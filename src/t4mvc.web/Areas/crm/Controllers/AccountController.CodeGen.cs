




using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using t4mvc.web;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.ViewModels;
using t4mvc.web.core.ViewModelServices;

namespace t4mvc.web.Areas.crm.Controllers
{
    [Area("crm")]
    [Authorize(Roles = "crm")]
    public partial class AccountController : AccountControllerBase
    {
        public AccountController(IAccountViewModelService accountViewModelService, IServiceProvider serviceProvider) : base(accountViewModelService, serviceProvider)
        {
        }
    }

    public class AccountControllerBase : t4mvcController
    {
        protected readonly IAccountViewModelService accountViewModelService;
        protected readonly IServiceProvider serviceProvider;

        // Contructor
        public AccountControllerBase(IAccountViewModelService accountViewModelService, IServiceProvider serviceProvider)
        {
            this.accountViewModelService = accountViewModelService;
            this.serviceProvider        = serviceProvider;
        }

        // GET: Admin/Manufacturer
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var viewModel = accountViewModelService.Find(id.Value);

            return View("Details", viewModel);
        }

        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var viewModel = accountViewModelService.Find(id.Value);
            Current.EditMode = true;

            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AccountViewModel accountViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                accountViewModelService.SaveAccount(accountViewModel);
                Current.Saved("Account saved");
                return Current.GetEditDestination(() => Url.Action("Details", new { id = accountViewModel.AccountId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				accountViewModelService.Hydrate(accountViewModel);
                Current.EditMode = true;
                return View("Details", accountViewModel);
            }
        }

        public virtual ActionResult Create()
        {
            Current.EditMode = true;
			var viewModel = new AccountViewModel();
            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(AccountViewModel accountViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                accountViewModelService.CreateAccount(accountViewModel);
                Current.Saved("Account created");
                return Current.GetCreateDestination(() => Url.Action("Details", new { id = accountViewModel.AccountId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				accountViewModelService.Hydrate(accountViewModel);
                Current.EditMode = true;
                return View("Details", accountViewModel);
            }
        }

        protected virtual void ValidateViewModel()
        {
            // custom validation here
        }
    }
}