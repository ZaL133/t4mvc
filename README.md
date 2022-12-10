# t4mvc
Scaffolding an .net core MVC application using a spec file and T4 templates

Often in a data access application, there is a TON of stuff that is done over and over. There's the model, the data access, the viewmodel and associated mapper, the html templates (list, details, edit), the navigation, dropdown lists, excel exports etc etc etc... 

During the process of building a large application like this, you start to develop standard ways of doing things, and then repeating those things over and over for each component. 

This project is meant to automate the all that repetitive boilerplate work, while still being flexible to any customizations, and encorporating best practices. It's all based on methods and opinions that I developed while working on a large web app. 

**Currently, the database is not part of this scaffolding project and is generated manually due to personal preference**