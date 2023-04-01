using t4mvc.core;
using t4mvc.data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using t4mvc.web.core.Annotation;

namespace t4mvc.web.core.ViewModels
{
	public partial class ProjectViewModel
	{
        [Required]
        public Guid ProjectId { get; set; }
        public Guid ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
        [Display(Name = "Account")]
        [Select2("/api/select2/getaccounts", "AccountId", "Name", "Account", area : "consulting")]
        public Guid? AccountId { get; set; }

        public string AccountIdName { get; set; }
        [Display(Name = "Primary Contact")]
        [Select2("/api/select2/getcontacts", "ContactId", "EmailAddress", "Contact", area : "consulting")]
        public Guid? PrimaryContactId { get; set; }

        public string PrimaryContactIdEmailAddress { get; set; }
        [UIHint("t4mvcWysiwyg")]
        public string Description { get; set; }
        [Display(Name = "Estimated Income")]
        [UIHint("Money")]
        public decimal? EstimatedIncome { get; set; }

        public List<ProjectLogViewModel> ProjectLogs { get; set; } = new List<ProjectLogViewModel>();

        public List<InvoiceViewModel> Invoices { get; set; } = new List<InvoiceViewModel>();

        public List<NoteViewModel> Notes { get; set; } = new List<NoteViewModel>();

		public List<AuditRecord> AuditHistory { get; set; } = new List<AuditRecord>();

	}
}