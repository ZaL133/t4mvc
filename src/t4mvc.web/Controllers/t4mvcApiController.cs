using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using t4mvc.web.core.Models;
using t4mvc.web.core.ViewModels;
using t4mvc.web.core.ViewModelServices;
using Ganss.Xss;

namespace t4mvc.web.Controllers
{
    [Authorize]
    public partial class t4mvcApiController
    {
        [Route("all-users")]
        [HttpGet]
        public IEnumerable<UserModel> GetAllUsers()
        {
            var userViewModelService    = serviceProvider.GetService<IUserViewModelService>() as IUserViewModelService;
            var allUsers                = userViewModelService.GetAllUsersWithProfilePics();
            var allUserList             = allUsers.ToList();

            return allUserList;
        }

        [Route("new-note")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool NewNote([FromForm] NoteViewModel noteViewModel)
        {
            var sanitizer           = new HtmlSanitizer();
            noteViewModel.NoteText  = sanitizer.Sanitize(noteViewModel.NoteText);

            noteViewModelService.CreateNote(noteViewModel);

            return true;
        }
    }
}
