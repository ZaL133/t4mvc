using t4mvc.core;
using t4mvc.data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using t4mvc.web.core.Annotation;

namespace t4mvc.web.core.ViewModels
{
	public partial class ContactViewModel
	{
        [Required]
        public Guid ContactId { get; set; }
        public Guid ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Account")]
        [Select2("/api/select2/getaccounts", "AccountId", "Name", "Account", area : "crm")]
        public Guid? AccountId { get; set; }
        public string AccountIdName { get; set; }
        [Display(Name = "Middle")]
        public string MiddleName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        [Display(Name = "Email Address")]
        [UIHint("Email")]
        public string EmailAddress { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [UIHint("Phone")]
        public string Phone { get; set; }
        public string Fax { get; set; }
        [UIHint("Phone")]
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool Active { get; set; }

        public List<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();
        public List<NoteViewModel> Notes { get; set; } = new List<NoteViewModel>();
	}
}