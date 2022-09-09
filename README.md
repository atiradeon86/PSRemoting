# PSRemoting

#Changelog 


2022.09.09 - Adding simple C# MSSQL database management app ManageDB v.0.1 (Datasource editor for Connect-To-AzureVm-SQL Module)

2022.09.07 - Adding MS SQL database support, with example database + db_credential_maker

2022.09.06 - Adding SSH support,Active Directory Domain support and implementing better credential logic

psremoting.ps1
-------------------------------------------------------------------------------------------------------------

-> Configure WinRM with self signed certificate, and create connection script (connect.ps1) <-

Usage:


1. -> Azure: Inbound security rule WinRM must be set. (Open port: 5986)

2. -> DNS name must to be set as the same way: 

Example:


Hostname: bryan

DNS: bryan.westeurope.cloudapp.azure.com


Connect-To-AzureVm-SQL.ps1 
-------------------------------------------------------------------------------------------------------------


->  Connect to Azure VM based on MS SQL database - with credenetial managament -<

Usage:


1. 
Import the provided SQL file (sql\vm.BACPAC) to your MS SQL Server, or create only the table structure (sql_tables.sql) and fill with your own data

SQL server in one minute, if you have s server with docker:  https://hub.docker.com/_/microsoft-mssql-server

```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

2.
Add your SQL Server data to the Connect-To-AzureVm-SQL.ps1 file

      $SqlServer = ""
      $DB_Name = ""

3. 
Create your DB credential file (You can use the provided: db_credential_maker.ps1 script)

4.
Import-Module .\Connect-To-AzureVm-SQL.ps1  -Force


Examples: 


Connect-To-AzureVm-SQL-mode r (RDP)

Connect-To-AzureVm-SQL -mode p (PSSession)

Connect-To-AzureVm-SQL -mode s (SSH)

-The script create your credential file, and you can use for the RDP authentication too ...


Connect-To-AzureVm.ps1 
-------------------------------------------------------------------------------------------------------------
->  Connect to Azure VM based on custom csv file -<

Import-Module .\Connect-To-AzureVm.ps1 -Force


Examples: 

Connect-To-AzureVM -mode r (RDP)

Connect-To-AzureVM -mode p (PSSession)

Connect-To-AzureVM -mode s (SSH)

-The script create your credential file, and you can use for the RDP authentication too ...
