using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Models.Data
{
    public class Video : BaseModel
    {       
        public string VideoId { get; set; }
        public string ChannelName { get; set; }
        public int ViewCount { get; set; }
        public int ChannelId { get; set; }
    }
}
