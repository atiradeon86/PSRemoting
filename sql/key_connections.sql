alter table dbo.vm_data 
add constraint "FKEY_addomains"  
foreign key (ad_domain_id) references dbo.vm_addomains (id)

alter table dbo.vm_data 
add constraint "FKEY_domains"  
foreign key (domain_id) references dbo.vm_domains (id)

alter table dbo.vm_data 
add constraint "FKEY_hostnames"  
foreign key (hostname_id) references dbo.vm_hostnames (id)

alter table dbo.vm_data 
add constraint "FKEY_locations"  
foreign key (location_id) references dbo.vm_locations (id)

alter table dbo.vm_data 
add constraint "FKEY_pems"  
foreign key (pem_id) references dbo.vm_pems (id)

alter table dbo.vm_data 
add constraint "FKEY_resources"  
foreign key (resource_id) references dbo.vm_resources (id)
