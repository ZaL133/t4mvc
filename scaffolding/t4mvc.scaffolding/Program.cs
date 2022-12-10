using t4mvc.scaffolding;
using t4mvc.scaffolding.EntityDefinition;
using t4mvc.scaffolding.simpletemplates;
using t4mvc.scaffolding.templates;

Settings.RootPath               = @"..\..\..\..\..\src\";
Settings.ApplicationName        = "t4mvc";
Settings.DbContextName          = "t4DbContext";

var specFile = File.ReadAllText("schema.spec");
var entities = EntityParser.ParseSpecFile(specFile);

ScaffoldModel(entities);
ScaffoldDataServices(entities);

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