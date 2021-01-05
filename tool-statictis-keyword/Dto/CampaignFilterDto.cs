using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Dto
{
    public class CampaignFilterDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
