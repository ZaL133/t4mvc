




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

namespace t4mvc.web.Areas.consulting.Controllers
{
    [Area("consulting")]
    public partial class ProjectController : ProjectControllerBase
    {
        public ProjectController(IProjectViewModelService projectViewModelService, IServiceProvider serviceProvider) : base(projectViewModelService, serviceProvider)
        {
        }
    }

    public class ProjectControllerBase : t4mvcController
    {
        protected readonly IProjectViewModelService projectViewModelService;
        protected readonly IServiceProvider serviceProvider;

        // Contructor
        public ProjectControllerBase(IProjectViewModelService projectViewModelService, IServiceProvider serviceProvider)
        {
            this.projectViewModelService = projectViewModelService;
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

            var viewModel = projectViewModelService.Find(id.Value);

            return View("Details", viewModel);
        }

        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var viewModel = projectViewModelService.Find(id.Value);
            Current.EditMode = true;

            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(ProjectViewModel projectViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                projectViewModelService.SaveProject(projectViewModel);
                Current.Saved("Project saved");
                return Current.GetEditDestination(() => Url.Action("Details", new { id = projectViewModel.ProjectId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				projectViewModelService.Hydrate(projectViewModel);
                Current.EditMode = true;
                return View("Details", projectViewModel);
            }
        }

        public virtual ActionResult Create(Guid? accountId, Guid? primaryContactId)
        {
            Current.EditMode = true;
			var viewModel = new ProjectViewModel(){ AccountId = accountId, PrimaryContactId = primaryContactId };
            projectViewModelService.Hydrate(viewModel);
            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(ProjectViewModel projectViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                projectViewModelService.CreateProject(projectViewModel);
                Current.Saved("Project created");
                return Current.GetCreateDestination(() => Url.Action("Details", new { id = projectViewModel.ProjectId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				projectViewModelService.Hydrate(projectViewModel);
                Current.EditMode = true;
                return View("Details", projectViewModel);
            }
        }

        protected virtual void ValidateViewModel()
        {
            // custom validation here
        }
    }
}