using t4mvc.core;
using t4mvc.data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using t4mvc.web.core.Annotation;

namespace t4mvc.web.core.ViewModels
{
	public partial class NoteViewModel
	{
        [Required]
        public Guid NoteId { get; set; }
        [Required]
        public Guid ModifyUserId { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [UIHint("t4mvcHtmlTextArea")]
        public string NoteText { get; set; }
        [Select2("/api/select2/getaccounts", "AccountId", "Name", "Account")]
        public Guid? AccountId { get; set; }
        public string AccountIdName { get; set; }
        [Select2("/api/select2/getcontacts", "ContactId", "EmailAddress", "Contact")]
        public Guid? ContactId { get; set; }
        public string ContactIdEmailAddress { get; set; }

	}
}