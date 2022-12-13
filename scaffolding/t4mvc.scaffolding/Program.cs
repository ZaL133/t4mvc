﻿using t4mvc.scaffolding;
using t4mvc.scaffolding.EntityDefinition;
using t4mvc.scaffolding.simpletemplates;
using t4mvc.scaffolding.templates;
using t4mvc.scaffolding.templates.viewtemplates;

Settings.RootPath               = @"..\..\..\..\..\src\";
Settings.ApplicationName        = "t4mvc";
Settings.DbContextName          = "t4DbContext";

var specFile        = File.ReadAllText("schema.spec");
Settings.Entities   = EntityParser.ParseSpecFile(specFile).ToList();
var entities = Settings.Entities;

ScaffoldModel(entities);
ScaffoldDataServices(entities);
ScaffoldViewModels(entities);
ScaffoldViewModelServices(entities);
ScaffoldAutoMapper(entities);
ScaffoldUnity(entities);
// This is a big one
ScaffoldAdminAreas(entities.Where(x => !x.DontScaffold));

// Scaffolds out the model files 
static void ScaffoldModel(IEnumerable<Entity> entities)
{
    var directory = Settings.CreateAndMapPath($"{Settings.ApplicationName}.core");

    // Scaffold the user 
    var userFileName    = $"{Settings.ApplicationUserTypeName}.CodeGen.cs";
    var fullUserFileName = Path.Combine(directory, userFileName);
    var userText        = new app_identityuser().TransformText();
    File.WriteAllText(fullUserFileName, userText);

    // Scaffold each model item
    foreach(var entity in entities)
    {
        var ec          = new entityclass(entity);
        var ecText      = ec.TransformText();
        var fileName    = $"{entity.Name.ToSchemaName()}.cs";
        var fullFileName = Path.Combine(directory, fileName);
        
        // Write
        File.WriteAllText(fullFileName, ecText);
    }
}

// Scaffolds out the data access services and the data context
static void ScaffoldDataServices(IEnumerable<Entity> entities)
{
    // Create the data context 
    var ctxFolder   = Settings.CreateAndMapPath($"{Settings.ApplicationName}.Data");
    var ctxFileName = $"{Settings.DbContextName}.CodeGen.cs";
    var ctx         = new dbcontext(entities);
    var ctxText     = ctx.TransformText();

    File.WriteAllText(Path.Combine(ctxFolder, ctxFileName), ctxText);

    // Write each data service file
    var directory = Settings.CreateAndMapPath($"{Settings.ApplicationName}.data\\Services");
    foreach(var entity in entities)
    {
        var dataService     = new dataservice(entity);
        var dsText          = dataService.TransformText();
        var fileName        = $"{entity.Name.ToSchemaName()}Service.cs";
        var fullFileName    = Path.Combine(directory, fileName);

        // Write 
        File.WriteAllText(fullFileName, dsText);
    }
}

static void ScaffoldViewModels(IEnumerable<Entity> entities)
{
    var folderName  = Path.Combine($"{Settings.ApplicationName}.web.core", "ViewModels");
    var folder      = Settings.CreateAndMapPath(folderName);

    foreach (var entity in entities)
    {
        var vmFileName      = entity.Name.ToSchemaName() + "ViewModel.CodeGen.cs";
        var vmFullFileName  = Path.Combine(folder, vmFileName);
        var vm              = new entityviewmodel(entity);
        var vmText          = vm.TransformText();

        // Write the poco classes
        File.WriteAllText(vmFullFileName, vmText);
    }
}

static void ScaffoldViewModelServices(IEnumerable<Entity> entities)
{
    var viewModelServiceDirectory = Settings.CreateAndMapPath($"{Settings.ApplicationName}.Web.Core\\ViewModelServices");
    Directory.CreateDirectory(viewModelServiceDirectory);

    foreach (var entity in entities)
    {
        var fileName        = $"{entity.Name.ToSchemaName()}ViewModelService.CodeGen.cs";
        var fullFileName    = Path.Combine(viewModelServiceDirectory, fileName);
        var vms             = new viewmodelservice(entity);
        var vmsText         = vms.TransformText();
        // Write the poco classes
        File.WriteAllText(fullFileName, vmsText);
    }

}

static void ScaffoldAutoMapper(IEnumerable<Entity> entities)
{
    var appStart = Settings.CreateAndMapPath($"{Settings.ApplicationName}.Web\\App_Start");
    Directory.CreateDirectory(appStart);

    var amFileName      = $"AutoMapperConfig.CodeGen.cs";
    var amFullFileName  = Path.Combine(appStart, amFileName);
    var am              = new automapper(entities);
    var amText          = am.TransformText();

    File.WriteAllText(amFullFileName, amText);
}

static void ScaffoldUnity(IEnumerable<Entity> entities)
{
    var appStart = Settings.CreateAndMapPath($"{Settings.ApplicationName}.Web\\App_Start");
    Directory.CreateDirectory(appStart);

    var amFileName      = $"ServiceConfig.CodeGen.cs";
    var amFullFileName  = Path.Combine(appStart, amFileName);
    var am              = new unity(entities);
    var amText          = am.TransformText();

    File.WriteAllText(amFullFileName, amText);
}

static void ScaffoldAdminAreas(IEnumerable<Entity> entities)
{
    foreach(var areaGroup in entities.GroupBy(x => x.Area))
    {
        var areaKey = areaGroup.Key;
        string areaPath;
        if (areaKey == null)
        {
            areaPath = Path.Combine(Settings.RootPath, Settings.ApplicationName + ".web");
        }
        else
        {
            var folder = Path.Combine(Settings.RootPath, Settings.ApplicationName + ".web", "Areas", areaKey);
            areaPath = Settings.CreateAndMapPath(folder);
        }

        // Write Nav
        var navBar = "<nav>\n\t<ul>\n\t\t<li>" + string.Join("</li>\n\t\t<li>",
                                                       entities.Select(x => "<a href='~/" + areaKey + "/" + x.Name.ToSchemaName() + "'>" + x.Name + "</a>")
                                                               .ToArray())
                                         + "</li>\n\t</ul>\n</nav>";

        Directory.CreateDirectory(areaPath + "\\Views\\Shared");
        Directory.CreateDirectory(areaPath + "\\Controllers");
        Directory.CreateDirectory(areaPath + "\\Views");

        if (areaKey != null)
        {
            File.WriteAllText(areaPath + "\\Views\\web.config", File.ReadAllText("CustomTemplates\\Web.Config.txt").Replace("{AppName}", Settings.ApplicationName));
            File.WriteAllText(areaPath + "\\Views\\_ViewStart.cshtml", @"@{
    Layout = Settings.ViewStart;
}");
            File.WriteAllText(areaPath + "\\Views\\Shared\\_Nav.cshtml", navBar);
            File.WriteAllText(areaPath + "\\" + areaKey + "HostingStartup.cs", $@"
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof({Settings.ApplicationName}.Web.Areas.{areaKey}.{areaKey}HostingStartup))]
namespace {Settings.ApplicationName}.Web.Areas.{areaKey}
{{
    public class {areaKey}HostingStartup : IHostingStartup
    {{
        public void Configure(IWebHostBuilder builder)
        {{
            builder.ConfigureServices((context, services) => {{}});
        }}
    }}
}}");

            Directory.CreateDirectory($"{Settings.RootPath}\\{Settings.ApplicationName}.web.core\\Rendering");
            File.WriteAllText($"{Settings.RootPath}\\{Settings.ApplicationName}.Web.Core\\Rendering\\{Settings.ApplicationName}HtmlHelper.CodeGen.cs",
                              new htmlhelper(entities).TransformText());
        }

        foreach (var entity in areaGroup)
        {
            // Write Controller
            File.WriteAllText(areaPath + "\\Controllers\\" + entity.Name.ToSchemaName() + "Controller.CodeGen.cs",
                          new admincontroller(areaKey, entity).TransformText());

            string viewDirectory;
            if (areaKey == null)
            {
                viewDirectory = $"{Settings.RootPath}\\{Settings.ApplicationName}.Web\\Views\\" + entity.Name.ToSchemaName() + "\\CodeGen\\";
            }
            else
            {
                viewDirectory = $"{Settings.RootPath}\\{Settings.ApplicationName}.Web\\Areas\\" + areaKey + "\\Views\\" + entity.Name.ToSchemaName() + "\\CodeGen\\";
            }

            Directory.CreateDirectory(viewDirectory);
            // Write Index View
            File.WriteAllText(viewDirectory + "Index.cshtml",
                          new adminindexview(areaKey, entity).TransformText());

            // Write Index View
            File.WriteAllText(viewDirectory + "_IndexPartial.cshtml",
                          new partialindexview(areaKey, entity).TransformText());

            // Write Table View
            File.WriteAllText(viewDirectory + "_TablePartial.cshtml",
                          new partialtableview(areaKey, entity).TransformText());

            //// Write Edit View
            //File.WriteAllText(viewDirectory + "Details.cshtml",
            //              new admindetails(applicationName, areaKey, entity).TransformText());

            //// Write Edit View
            //if (entity.Layout != null)
            //{
            //    File.WriteAllText(viewDirectory + "_DetailsPartial.cshtml",
            //              new partialdetails_layout(applicationName, areaKey, entity).TransformText());
            //}
            //else
            //{
            //    File.WriteAllText(viewDirectory + "_DetailsPartial.cshtml",
            //              new partialdetails_nolayout(applicationName, areaKey, entity).TransformText());
            //}
        }
    }
}