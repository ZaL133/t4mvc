using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace t4mvc.core
{
	[Table(name: "Contact")]
	public partial class Contact
	{
        [Key]
        [Required]
        public Guid ContactId { get; set; }
        [Required]
        public Guid CreateUserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public Guid ModifyUserId { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }
        [Display(Description = "First Name")]
        public string FirstName { get; set; }
        [Display(Description = "Last Name")]
        public string LastName { get; set; }
        public Guid? AccountId { get; set; }
        [Display(Description = "Middle Name")]
        public string MiddleName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        [Display(Description = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Description = "Job Title")]
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool Active { get; set; }

	}
}