




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
    public partial class ProjectLogController : ProjectLogControllerBase
    {
        public ProjectLogController(IProjectLogViewModelService projectLogViewModelService, IServiceProvider serviceProvider) : base(projectLogViewModelService, serviceProvider)
        {
        }
    }

    public class ProjectLogControllerBase : t4mvcController
    {
        protected readonly IProjectLogViewModelService projectLogViewModelService;
        protected readonly IServiceProvider serviceProvider;

        // Contructor
        public ProjectLogControllerBase(IProjectLogViewModelService projectLogViewModelService, IServiceProvider serviceProvider)
        {
            this.projectLogViewModelService = projectLogViewModelService;
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

            var viewModel = projectLogViewModelService.Find(id.Value);

            return View("Details", viewModel);
        }

        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var viewModel = projectLogViewModelService.Find(id.Value);
            Current.EditMode = true;

            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(ProjectLogViewModel projectLogViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                projectLogViewModelService.SaveProjectLog(projectLogViewModel);
                Current.Saved("ProjectLog saved");
                return Current.GetEditDestination(() => Url.Action("Details", new { id = projectLogViewModel.ProjectLogId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				projectLogViewModelService.Hydrate(projectLogViewModel);
                Current.EditMode = true;
                return View("Details", projectLogViewModel);
            }
        }

        public virtual ActionResult Create(Guid projectId)
        {
            Current.EditMode = true;
			var viewModel = new ProjectLogViewModel(){ ProjectId = projectId };
            projectLogViewModelService.Hydrate(viewModel);
            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(ProjectLogViewModel projectLogViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                projectLogViewModelService.CreateProjectLog(projectLogViewModel);
                Current.Saved("ProjectLog created");
                return Current.GetCreateDestination(() => Url.Action("Details", new { id = projectLogViewModel.ProjectLogId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				projectLogViewModelService.Hydrate(projectLogViewModel);
                Current.EditMode = true;
                return View("Details", projectLogViewModel);
            }
        }

        protected virtual void ValidateViewModel()
        {
            // custom validation here
        }
    }
}