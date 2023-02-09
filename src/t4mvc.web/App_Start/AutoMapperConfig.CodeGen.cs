using AutoMapper;
using t4mvc.core;
using t4mvc.web.core.ViewModels;

namespace t4mvc.web
{
    public static partial class AutoMapperConfig
    {
	    public static void AddCodeGen(IMapperConfigurationExpression cfg)
        {
		    cfg.CreateMap<Account, AccountViewModel>().ReverseMap();
		    cfg.CreateMap<Contact, ContactViewModel>().ReverseMap();
		    cfg.CreateMap<Invoice, InvoiceViewModel>().ReverseMap();
		    cfg.CreateMap<Note, NoteViewModel>().ReverseMap();
		    cfg.CreateMap<Project, ProjectViewModel>().ReverseMap();
		    cfg.CreateMap<ProjectLog, ProjectLogViewModel>().ReverseMap();

        }
    }

    public class t4mvcMappingProfile : Profile
    {
        public t4mvcMappingProfile()
        {
		    CreateMap<Account, AccountViewModel>().ReverseMap();
		    CreateMap<Contact, ContactViewModel>().ReverseMap();
		    CreateMap<Invoice, InvoiceViewModel>().ReverseMap();
		    CreateMap<Note, NoteViewModel>().ReverseMap();
		    CreateMap<Project, ProjectViewModel>().ReverseMap();
		    CreateMap<ProjectLog, ProjectLogViewModel>().ReverseMap();

        }
    }
}