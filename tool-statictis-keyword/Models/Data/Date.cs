using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Models.Data
{
    public class Date : BaseModel
    {
        [Required]
        public DateTime DateTime { get; set; }
    }
}
