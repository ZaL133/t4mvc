# t4mvc
Scaffolding an .net core MVC application using a spec file and T4 templates

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

## Licenses 

I rely heavily on the wonderful project EPPlus for excel functionality. 
Please see EPPlus for more licensing info. If you are using this project for commercial use - you need to obtain a commercial license for EPPlus
https://www.epplussoftware.com/en/LicenseOverview