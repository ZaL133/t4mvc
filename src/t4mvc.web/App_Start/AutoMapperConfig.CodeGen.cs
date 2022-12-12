using AutoMapper;
using t4mvc.core;
using t4mvc.web.core.viewmodels;

namespace t4mvc.web
{
    public static partial class AutoMapperConfig
    {
	    public static void AddCodeGen(IMapperConfigurationExpression cfg)
        {
		    cfg.CreateMap<Account, AccountViewModel>().ReverseMap();
        }
    }

    public class t4mvcMappingProfile : Profile
    {
        public t4mvcMappingProfile()
        {
		    CreateMap<Account, AccountViewModel>().ReverseMap();
        }
    }
}