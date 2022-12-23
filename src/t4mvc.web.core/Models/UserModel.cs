using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ProfilePic { get; set; }
    }
}
