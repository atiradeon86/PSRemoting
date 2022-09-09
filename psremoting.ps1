#Enable PSRemoting
Enable-PSRemoting -Force

#Alow all hosts
Set-Item WSMan:\localhost\Client\TrustedHosts *

# Open Firewall port 5986
New-NetFirewallRule -DisplayName 'HTTP-Inbound' -Direction Inbound -Action Allow -Protocol TCP -LocalPort @('5986')

#Get-hostname
$hostname = hostname

#Get CN data from hostname
$fqdn = "$hostname.westeurope.cloudapp.azure.com"

#Create Cert
$cert = New-SelfSignedCertificate -Subject "CN=$fqdn" -TextExtension "2.5.29.37={text}1.3.6.1.5.5.7.3.1"
$thumbprint = $cert.Thumbprint

#WinRM Set to Listen HTTPS - with generated cert
winrm create winrm/config/Listener?Address=*+Transport=HTTPS '@{Hostname="' "$fqdn" '";CertificateThumbprint="' "$thumbprint" '" }'

<# Debug winrm get winrm/config #>


#Create a connect.ps1 file, and cat for easy connect

Write-host '$PSSessionOption' = New-PSSessionOption -SkipCACheck  *> connect.ps1
$connect = Write-host 'New-PSSession  -ComputerName '"$fqdn"' -Credential (Get-Credential) -UseSSL -SessionOption $PSSessionOption' 6>&1

#Add content to connect.ps1 file
Add-Content connect.ps1 -Value $connect

#Write out the connection script to the screen
cat connect.ps1