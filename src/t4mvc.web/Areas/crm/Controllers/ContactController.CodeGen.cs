




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
    public partial class ContactController : ContactControllerBase
    {
        public ContactController(IContactViewModelService contactViewModelService, IServiceProvider serviceProvider) : base(contactViewModelService, serviceProvider)
        {
        }
    }

    public class ContactControllerBase : t4mvcController
    {
        protected readonly IContactViewModelService contactViewModelService;
        protected readonly IServiceProvider serviceProvider;

        // Contructor
        public ContactControllerBase(IContactViewModelService contactViewModelService, IServiceProvider serviceProvider)
        {
            this.contactViewModelService = contactViewModelService;
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

            var viewModel = contactViewModelService.Find(id.Value);

            return View("Details", viewModel);
        }

        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var viewModel = contactViewModelService.Find(id.Value);
            Current.EditMode = true;

            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(ContactViewModel contactViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                contactViewModelService.SaveContact(contactViewModel);
                Current.Saved("Contact saved");
                return Current.GetEditDestination(() => Url.Action("Details", new { id = contactViewModel.ContactId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				contactViewModelService.Hydrate(contactViewModel);
                Current.EditMode = true;
                return View("Details", contactViewModel);
            }
        }

        public virtual ActionResult Create(Guid? accountId)
        {
            Current.EditMode = true;
			var viewModel = new ContactViewModel(){ AccountId = accountId };
            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(ContactViewModel contactViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                contactViewModelService.CreateContact(contactViewModel);
                Current.Saved("Contact created");
                return Current.GetCreateDestination(() => Url.Action("Details", new { id = contactViewModel.ContactId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				contactViewModelService.Hydrate(contactViewModel);
                Current.EditMode = true;
                return View("Details", contactViewModel);
            }
        }

        protected virtual void ValidateViewModel()
        {
            // custom validation here
        }
    }
}