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
        public string ChannelId { get; set; }
        public DateTime? PublishDate { get; set; }
        public virtual ICollection<Keyword_Video> Keywords { get; set; }
    }
    public class ViewsCountByDay : BaseModel
    {
        public float ViewsCount { get; set; }
        public int VideoId { get; set; }
        public virtual Video Video { get; set; }
        public DateTime Date { get; set; }

    }
}
