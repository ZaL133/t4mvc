using t4mvc.web.core.Rendering;

namespace t4mvc.web.App_Start
{
    public class SidebarConfig
    {
        public static void Configure()
        {
            t4mvcHtmlHelper.AddCodeGenFunc = (helper, sidebarModel) =>
            {
                // add anything custom here 

                // Then call the codegen function 
                // t4mvcHtmlHelper.AddCodeGen(helper, sidebarModel);
            };
        }
    }
}
