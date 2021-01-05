using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Dto
{
    public class EditKeywordRangeDto
    {
        public List<int> Ids { get; set; }
        public int CampaignId { get; set; }
        public string Note { get; set; }
    }
}
