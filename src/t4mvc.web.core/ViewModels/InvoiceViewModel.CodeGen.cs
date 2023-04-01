using t4mvc.core;
using t4mvc.data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using t4mvc.web.core.Annotation;

namespace t4mvc.web.core.ViewModels
{
	public partial class InvoiceViewModel
	{
        [Required]
        public Guid InvoiceId { get; set; }
        public Guid ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        [Required]
        [Display(Name = "Project")]
        [Select2("/api/select2/getprojects", "ProjectId", "ProjectName", "Project", area : "consulting")]
        public Guid ProjectId { get; set; }

        public string ProjectIdProjectName { get; set; }
        [Required]
        [Display(Name = "Invoice Name")]
        public string InvoiceName { get; set; }
        [Required]
        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        [Required]
        [Display(Name = "Invoice Amount")]
        [UIHint("Money")]
        public decimal InvoiceAmount { get; set; }
        public InvoiceStatus? Status { get; set; }

		public List<AuditRecord> AuditHistory { get; set; } = new List<AuditRecord>();

	}
}