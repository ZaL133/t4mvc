using t4mvc.core;
using t4mvc.data.services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace t4mvc.web.core.viewmodels
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