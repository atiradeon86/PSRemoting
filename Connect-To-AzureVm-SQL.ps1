Function Connect-To-AzureVM-SQL {
  <#
  
  .SYNOPSIS
    Connect to Azure VM based on sql database
  .DESCRIPTION

  .PARAMETER mode
    p -> Open as PSSession
    r -> Open as RDP connection
    s -> Open as SSH Session

  .NOTES
    Version:        0.1
    Author:         Ati
    Creation Date:  2022.09.07
  .EXAMPLE
    Connect-To-AzureVM-SQL -mode p
  .EXAMPLE
    Connect-To-AzureVM-SQL -mode r
  .EXAMPLE
    Connect-To-AzureVM-SQL -mode s
  #>
  
  #-----------------------------------------------------------[Parameters]-----------------------------------------------------------
  
  [CmdletBinding()]
  param (
  
      [Parameter(Mandatory=$true,
      HelpMessage="r -rdp, p - PSSession, s = ssh")] 
      [string]$mode
  
  )
  
  #----------------------------------------------------------[Declarations]----------------------------------------------------------
  
  BEGIN { 

      #DB Credenetial

      $DB_credential = Import-CliXml -Path ".\db_credential.xml"

      $SqlServer = "localhost"
      $DB_Name = "vm"

      $DB_username =$DB_credential.username
      $DB_password = $DB_credential.GetNetworkCredential().password
      
  }
  
  #-----------------------------------------------------------[Process]------------------------------------------------------------
  
  PROCESS {

      #Find and try to import credential file
      $xml =    Get-ChildItem | Select-Object Name | ? name -notlike "db_credential.xml" | ? name -like "*.xml"

      #If we have a usable credential file
      if ($xml) {

        #Get xml file data
        $name = $xml.name
        $credential = Import-CliXml -Path ".\$name"

        #Reading passwrod, and username from XML file for the RDP connection
        $password = $credential.GetNetworkCredential().password
        $username = $credential.username
      }

      #If credential file does not exist 
      if (!$credential) {
       
          # Choise Prompt
          $title    = "`n[Warning] Your credential file not exist!"
          $question = "`nDo you want to Create a new XML file?"
          $choices  = '&Yes', '&No'

          $decision = $Host.UI.PromptForChoice($title, $question, $choices, 0)
          
          if ($decision -eq 0) {

            Write-Host "`nPlease provide the required data:`n"
            
            #Data request
            $username = Read-host "Username"
            $password = Read-host "Password" 

            Write-Host "`nThe new credential file created, and ready to use : $username.xml" -ForegroundColor Green 

            #Password Convert to SecureString
            $pw = ConvertTo-SecureString $password -AsPlainText -Force

            #Creating the credential xml file
            $cred = New-Object System.Management.Automation.PSCredential ($username, $pw)
            $cred | Export-CliXml -Path .\$username.xml

            #Reading the new xml file to for the authentication
            $credential = Import-CliXml -Path "$username.xml"

          } 
        
      }

      #Show Options Table

      $query= " 

      SELECT vm_data.id,hostname,location,resource,domain,addomain,vm_pems.pem FROM vm_data

      LEFT JOIN vm_hostnames On 
      vm_hostnames.id = vm_data.hostname_id

      LEFT JOIN vm_locations On  
      vm_data.location_id = vm_locations.id

      LEFT JOIN vm_resources ON 
      vm_data.resource_id = vm_resources.id

      LEFT JOIN vm_domains On  
      vm_domains.id = vm_data.domain_id 
      
      LEFT JOIN vm_addomains ON 
      vm_data.ad_domain_id = vm_addomains.id
      
      LEFT JOIN vm_pems ON 
      vm_data.pem_id =  vm_pems.id 

      ORDER BY vm_data.id
      "

      $data = Invoke-Sqlcmd -ServerInstance $SQLServer -Database $DB_Name -Query $query -Username $DB_username -Password $DB_password

      $data | Format-Table

      #Delete all PSSession
      Get-PSSession | Remove-PSSession

      #Read ID
      $choice = Read-host "Please enter ID"

      #Select domain based on ID, and retrieve the required data elements
      $request = "

      SELECT domain,addomain,pem FROM vm_data  

      LEFT JOIN vm_domains ON vm_data.domain_id = vm_domains.id 
      LEFT JOIN  vm_addomains ON vm_data.ad_domain_id = vm_addomains.id
	  LEFT JOIN  vm_pems ON vm_data.pem_id = vm_pems.id

      WHERE vm_data.id = $choice
      ";

      $request_data = Invoke-Sqlcmd -ServerInstance $SQLServer -Database $DB_Name -Query $request -Username $DB_username -Password $DB_password 

      $selected = $request_data.domain
      $pem = $request_data.pem
  
      #Create the required username if host is AD joined
      $ADdomain = $request_data.addomain

      #Invoke-Sqlcmd commandlet !!!cannot handle!!! [System.DBNull] , DB ->vm_addomains id 2 has fix value: -
      if ($ADdomain -ne "-") {  

            $ADuser = $ADdomain+= "\$username" 
            $username = $ADuser 

       }
      
      if ($mode -like "p") {

        #Skip Cert CACCheck
        $PSSessionOption = New-PSSessionOption -SkipCACheck 

        #Create new session
        New-PSSession  -ComputerName $selected -Credential $credential -UseSSL -SessionOption $PSSessionOption 

        #Get new session ID
        $session = Get-PSSession
        $id = $session.id

        #Enter PSSession
        Enter-PSSession $id

        }
        elseif ($mode -like "r")
        {
         
          $cred = cmdkey /generic:"$selected" /user:"$username" /pass:"$password"
          $rdp = mstsc /v: $selected 
           
        }
        elseif ($mode -like "s")
        {
          #Invoke-Sqlcmd commandlet !!!cannot handle!!! [System.DBNull] , DB ->vm_pems id 2 has fix value: -
          if ($pem -ne "-") {
            Write-Host "`nUsed PEM file: $pem`n"-ForegroundColor Green 
            Write-Host "`nPlease provide the required data:`n"
            $ssh_username = Read-host "Username"
        
            ssh -i "$pem" $ssh_username`@$selected
          }
        }

  }

#-----------------------------------------------------------[Finish up]------------------------------------------------------------

END {}

}