Function Connect-To-AzureVM {
  <#
  
  .SYNOPSIS
    Connect to Azure VM based on custom csv file
  .DESCRIPTION

  .PARAMETER mode
    p -> Open as PSSession
    r -> Open as RDP connection
    s -> Open as SSH Session

  .NOTES
    Version:        0.5
    Author:         Ati
    Creation Date:  2022.09.06
  .EXAMPLE
    Connect-To-AzureVM -mode p
  .EXAMPLE
    Connect-To-AzureVM -mode r
  .EXAMPLE
    Connect-To-AzureVM -mode s
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

      #Import CSV
      $domains = import-csv ".\domains.csv"
        
  }
  
  #-----------------------------------------------------------[Process]------------------------------------------------------------
  
  PROCESS {

      #Find and try to import credential file
      $xml =   Get-ChildItem | Select-Object Name | ? name -like "*.xml"

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

      #Show Options
      Write-Output $domains | Format-Table

      #Delete all PSSession
      Get-PSSession | Remove-PSSession

      #Read ID

      $choice = Read-host "Please enter ID"

      #Select domain based on ID
      $selected=  $domains[$choice].Domain

      #Create the required username if host is AD joined
      $ADdomain = $domains[$choice].ADDomain
    
      if ($ADdomain) {

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

          $pem = $domains[$choice].PEM
          $domain = $domains[$choice].domain
          
          Write-Host "`nUsed PEM file: $pem`n"-ForegroundColor Green 
          Write-Host "`nPlease provide the required data:`n"
          $ssh_username = Read-host "Username"
      
          ssh -i "$pem" $ssh_username`@$domain
          
        }

  }

#-----------------------------------------------------------[Finish up]------------------------------------------------------------

END {}

}