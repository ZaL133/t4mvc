# t4mvc
Scaffolding an .net core MVC application using a spec file and T4 templates

See here for a functional example! 

<a href="https://t4mvc.azurewebsites.net/">t4mvc</a>
<pre>
User: admin@gmail.com
Pass: t4mvcP@$$word
</pre>

Often in a data access application, there is a TON of stuff that is done over and over. There's the model, the data access, the viewmodel and associated mapper, the html templates (list, details, edit), the navigation, dropdown lists, excel exports etc etc etc... 

During the process of building a large application like this, you start to develop standard ways of doing things, and then repeating those things over and over for each component. 

This project is meant to automate the all that repetitive boilerplate work, while still being flexible to any customizations, and encorporating best practices. It's all based on methods and opinions that I developed while working on a large web app. 

Boilterplate code that is automated

* Model
* Entity Framework context
* Configuration for automapper, Dependency Injection
* Security based on users and roles
* ViewModels with read only properties, Create and Modify fields for Date and User on all generated records
* Creationg of MVC Controllers, Views, and Areas
* Custom layout specification file for views
* Child record tables
* Select2 Lookups of Foreign key references
* Datatables.net index pages with server side sorting, filtering etc. 
	* Includes export to excel endpoint 
	* Includes report - simpile page with results from the datatable
* Icons used everywhere
* Global search
* Auditing
* Database Schema

The theme is based on the [admin bootstrap example](https://getbootstrap.com/docs/5.0/examples/dashboard/)

## Steps to build the project

1. Clone the repo
2. Reinstall nuget packages
3. Set connection string and run the EF migrations
4. Run the project to ensure it builds and runs and you can register
5. Open the `t4mvc/scaffolding` solution
6. Add your model and layout to the `schema.spec` and `layout.spec` files respectively. 
    * These include documentation
7. Run the scaffolding project to deploy any changes
8. Add scaffolding migrations at the dotnet cli
9. Deploy scaffolding migrations
10. Run! 

## Open source projects

* Automapper

## How to publish

If publishing to IIS, you need to set the maxQueryString and maxUrl fields to >= 6000. This is because the datatables.net url's get very long. If you have an error on the datatables grid, it's because this needs to be set. 

Here's a sample web.config 

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
<location path="." inheritInChildApplications="false">
	<system.webServer>
	<handlers>
		<add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
	</handlers>
	<aspNetCore processPath="dotnet" arguments=".\t4mvc.web.dll" stdoutLogEnabled="false" stdoutLogFile="\\?\%home%\LogFiles\stdout" hostingModel="inprocess" />
	<security>
		<requestFiltering allowDoubleEscaping="false">
			<requestLimits maxUrl="6000" maxQueryString="6000" />
		</requestFiltering>
	</security>
	</system.webServer>
</location>
</configuration>
<!--ProjectGuid: 2d6db2c8-e536-403d-9b11-8a10e80d9ef7-->
```

## Licenses 

I rely heavily on the wonderful project EPPlus for excel functionality. 
Please see EPPlus for more licensing info. If you are using this project for commercial use - you need to obtain a commercial license for EPPlus
https://www.epplussoftware.com/en/LicenseOverview
