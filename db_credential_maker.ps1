#Create credential xml for the database connection

Write-Host "`nPlease provide the required data for db credemtial file:`n"
            
#Data request
$username = Read-host "Username"
$password = Read-host "Password" 

$pw = ConvertTo-SecureString $password -AsPlainText -Force
$cred = New-Object System.Management.Automation.PSCredential ($username, $pw)

$cred | Export-CliXml -Path ".\db_credential.xml"

Write-Host "`nThe new credential file created: db_credential.xml" -ForegroundColor Green