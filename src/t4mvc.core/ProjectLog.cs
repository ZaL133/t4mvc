using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace t4mvc.core
{
	[Table(name: "ProjectLog")]
	public partial class ProjectLog
	{
        [Key]
        [Required]
        public Guid ProjectLogId { get; set; }
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
        [Display(Description = "Entry Name")]
        public string EntryName { get; set; }
        [Required]
        [Display(Description = "Entry Date")]
        public DateTime EntryDate { get; set; }
        [Required]
        public decimal Hours { get; set; }

	}
}