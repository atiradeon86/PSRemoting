USE [Azure]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[vm_data](
	[id] int NOT NULL IDENTITY(1,1) primary key, 
	[hostname_id] int NOT NULL,
	[location_id] [int] NOT NULL,
	[resource_id] [int] NOT NULL,
	[domain_id] [int] NOT NULL,
	[ad_domain_id] [int] NOT NULL,
	[pem_id] [int]
) ON [PRIMARY]

CREATE TABLE [dbo].[vm_hostnames](
	[id] int NOT NULL IDENTITY(1,1) primary key,
	[hostname] [nvarchar](50) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[vm_locations](
	[id] int NOT NULL IDENTITY(1,1) primary key,
	[location] [nvarchar](50) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[vm_domains](
	[id] int NOT NULL IDENTITY(1,1) primary key,
	[domain] [nvarchar](50) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[vm_addomains](
	[id] int NOT NULL IDENTITY(1,1) primary key,
	[addomain] [nvarchar](50) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[vm_resources](
	[id] int NOT NULL IDENTITY(1,1) primary key,
	[id_vm_location] [int] NOT NULL,
	[resource] [nvarchar](50) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[vm_pems](
	[id] int NOT NULL IDENTITY(1,1) primary key,
	[pem] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO


