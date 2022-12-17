using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.web.core.Models
{
    public record NoteDefinition
    {
        public Guid? Id { get; set; }
        public string KeyField { get; set; }
    }
}
