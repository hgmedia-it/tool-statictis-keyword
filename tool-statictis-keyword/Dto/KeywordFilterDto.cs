﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Dto
{
    public class KeywordFilterDto
    {
        public string Name { get; set; }
        public int CampaignId { get; set; }
        public string Note { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
