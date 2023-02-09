using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace t4mvc.core
{
	[Table(name: "Note")]
	public partial class Note
	{
        [Key]
        [Required]
        public Guid NoteId { get; set; }
        [Required]
        public Guid CreateUserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public Guid ModifyUserId { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }
        [Required]
        public string NoteText { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? ProjectLogId { get; set; }

	}
}