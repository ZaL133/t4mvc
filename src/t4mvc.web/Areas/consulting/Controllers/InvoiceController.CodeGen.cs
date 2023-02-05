




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
    public partial class InvoiceController : InvoiceControllerBase
    {
        public InvoiceController(IInvoiceViewModelService invoiceViewModelService, IServiceProvider serviceProvider) : base(invoiceViewModelService, serviceProvider)
        {
        }
    }

    public class InvoiceControllerBase : t4mvcController
    {
        protected readonly IInvoiceViewModelService invoiceViewModelService;
        protected readonly IServiceProvider serviceProvider;

        // Contructor
        public InvoiceControllerBase(IInvoiceViewModelService invoiceViewModelService, IServiceProvider serviceProvider)
        {
            this.invoiceViewModelService = invoiceViewModelService;
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

            var viewModel = invoiceViewModelService.Find(id.Value);

            return View("Details", viewModel);
        }

        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var viewModel = invoiceViewModelService.Find(id.Value);
            Current.EditMode = true;

            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(InvoiceViewModel invoiceViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                invoiceViewModelService.SaveInvoice(invoiceViewModel);
                Current.Saved("Invoice saved");
                return Current.GetEditDestination(() => Url.Action("Details", new { id = invoiceViewModel.InvoiceId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				invoiceViewModelService.Hydrate(invoiceViewModel);
                Current.EditMode = true;
                return View("Details", invoiceViewModel);
            }
        }

        public virtual ActionResult Create(Guid projectId)
        {
            Current.EditMode = true;
			var viewModel = new InvoiceViewModel(){ ProjectId = projectId };
            return View("Details", viewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(InvoiceViewModel invoiceViewModel)
        {
            ValidateViewModel();

            if (ModelState.IsValid)
            {
                invoiceViewModelService.CreateInvoice(invoiceViewModel);
                Current.Saved("Invoice created");
                return Current.GetCreateDestination(() => Url.Action("Details", new { id = invoiceViewModel.InvoiceId }),
                                                  () => Url.Action("Index"));
            }
            else
            {
				invoiceViewModelService.Hydrate(invoiceViewModel);
                Current.EditMode = true;
                return View("Details", invoiceViewModel);
            }
        }

        protected virtual void ValidateViewModel()
        {
            // custom validation here
        }
    }
}