using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace t4mvc.core
{
	[Table(name: "Invoice")]
	public partial class Invoice
	{
        [Key]
        [Required]
        public Guid InvoiceId { get; set; }
        [Required]
        public Guid CreateUserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public Guid ModifyUserId { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
        [Required]
        [Display(Description = "Invoice Name")]
        public string InvoiceName { get; set; }
        [Required]
        [Display(Description = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }
        [Required]
        [Display(Description = "Invoice Amount")]
        public decimal InvoiceAmount { get; set; }
        [Required]
        public string Status { get; set; }

	}
}