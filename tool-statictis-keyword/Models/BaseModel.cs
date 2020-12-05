using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
