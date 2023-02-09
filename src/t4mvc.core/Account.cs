using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace t4mvc.core
{
	[Table(name: "Account")]
	public partial class Account
	{
        [Key]
        [Required]
        public Guid AccountId { get; set; }
        [Required]
        public Guid CreateUserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public Guid ModifyUserId { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public Guid? ParentAccountId { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

	}
}