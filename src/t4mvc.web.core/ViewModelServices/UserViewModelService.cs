using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.core;
using t4mvc.data.Services;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.Models;
using t4mvc.web.core.ViewModels;

namespace t4mvc.web.core.ViewModelServices
{
    public partial interface IUserViewModelService
    {
        IQueryable<UserViewModel> GetAllUsers();
        UserViewModel Find(Guid UserId);
        void SaveUser(UserViewModel UserViewModel);
        void DeleteUser(UserViewModel UserViewModel);
        void Hydrate(UserViewModel UserViewModel);
        IEnumerable<UserModel> GetAllUsersWithProfilePics();
    }
    public partial class UserViewModelService : IUserViewModelService
    {

        private readonly IContextHelper contextHelper;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IUserService UserService;


        public IQueryable<UserViewModel> GetAllUsers()
        {
            var query = (from User in UserService.GetAllUsers()

                         select new UserViewModel
                         {

                         });
            return query;
        }

        public UserViewModelService(IUserService UserService, IContextHelper contextHelper, IWebHostEnvironment hostEnvironment)
        {
            this.UserService = UserService;
            this.contextHelper = contextHelper;
            this.hostEnvironment = hostEnvironment;
        }


        public UserViewModel Find(Guid UserId)
        {
            var User = UserService.Find(UserId).Map<UserViewModel>();

            Hydrate(User);
            return User;
        }

        public void SaveUser(UserViewModel UserViewModel)
        {
            var User = UserViewModel.Map<t4mvcUser>();

            var ignore = FieldMappingInspector<t4mvcUser>.GetAllReadonlyFields(UserViewModel);

            UserService.UpdateUser(User, ignore);

            contextHelper.SaveChanges();
        }

        public void DeleteUser(UserViewModel UserViewModel)
        {
            var User = UserViewModel.Map<t4mvcUser>();

            UserService.DeleteUser(User);

            contextHelper.SaveChanges();
        }


        public void Hydrate(UserViewModel UserViewModel)
        {
            var id = UserViewModel.Id;
        }

        const string PROFILEPICTUREDIRECTORY = "/img/profiles",
                         NOPIC = "/img/profiles/no-pic.gif";



        public IEnumerable<UserModel> GetAllUsersWithProfilePics()
        {

            var profileFiles = this.hostEnvironment.WebRootFileProvider.GetDirectoryContents(PROFILEPICTUREDIRECTORY);

            var allProfilePics = profileFiles.ToDictionary(key => key.Name.Split('.')[0],
                                                           val => val.Name,
                                                           StringComparer.CurrentCultureIgnoreCase);

            var users = UserService.GetAllUsers().ToList()
                                                 .Select(x => new UserModel
                                                 {
                                                     UserId     = x.Id,
                                                     Email      = x.Email,
                                                     FullName   = x.UserName,
                                                     ProfilePic = allProfilePics.ContainsKey(x.Id.ToString())
                                                                    ? $"{PROFILEPICTUREDIRECTORY}/{allProfilePics[x.Id.ToString()]}"
                                                                    : NOPIC
                                                 });

            return users;
        }
    }
}