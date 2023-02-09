using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace t4mvc.core
{
	[Table(name: "Project")]
	public partial class Project
	{
        [Key]
        [Required]
        public Guid ProjectId { get; set; }
        [Required]
        public Guid CreateUserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public Guid ModifyUserId { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }
        [Required]
        [Display(Description = "Project Name")]
        public string ProjectName { get; set; }
        [Display(Description = "Start Date")]
        public DateTime? StartDate { get; set; }
        [Display(Description = "Due Date")]
        public DateTime? DueDate { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? PrimaryContactId { get; set; }
        public string Description { get; set; }
        public decimal? EstimatedIncome { get; set; }

	}
}