using t4mvc.core;
using t4mvc.data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using t4mvc.web.core.Annotation;

namespace t4mvc.web.core.ViewModels
{
	public partial class AccountViewModel
	{
        [Required]
        public Guid AccountId { get; set; }
        public Guid ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [UIHint("Phone")]
        public string Phone { get; set; }
        public string Fax { get; set; }
        [UIHint("Website")]
        public string Website { get; set; }
        [Display(Name = "Parent Account")]
        public Guid? ParentAccountId { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        [UIHint("t4mvcWysiwyg")]
        public string Description { get; set; }
        public bool? Active { get; set; }

        public List<ContactViewModel> Contacts { get; set; } = new List<ContactViewModel>();
        public List<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();
        public List<NoteViewModel> Notes { get; set; } = new List<NoteViewModel>();
		public List<AuditRecord> AuditHistory { get; set; } = new List<AuditRecord>();
	}
}