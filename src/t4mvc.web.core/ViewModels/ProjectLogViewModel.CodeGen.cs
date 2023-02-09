using t4mvc.core;
using t4mvc.data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using t4mvc.web.core.Annotation;

namespace t4mvc.web.core.ViewModels
{
	public partial class ProjectLogViewModel
	{
        [Required]
        public Guid ProjectLogId { get; set; }
        public Guid ModifyUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        [Required]
        [Display(Name = "Project")]
        [Select2("/api/select2/getprojects", "ProjectId", "ProjectName", "Project", area : "consulting")]
        public Guid ProjectId { get; set; }

        public string ProjectIdProjectName { get; set; }
        [Required]
        [Display(Name = "Entry Name")]
        public string EntryName { get; set; }
        [Required]
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
        [Required]
        public decimal Hours { get; set; }

        public List<NoteViewModel> Notes { get; set; } = new List<NoteViewModel>();

		public List<AuditRecord> AuditHistory { get; set; } = new List<AuditRecord>();

	}
}