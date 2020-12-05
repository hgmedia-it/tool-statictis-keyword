using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Models.Data
{
    public class Statictis : BaseModel
    {
        public int KeywordId { get; set; }
        public virtual Keyword Keyword { get; set; }
        public int DateId { get; set; }
        public virtual Date Date { get; set; }
        public int CategoryId { get; set; }
        public int SearchByDayResultId { get; set; }
        public virtual SearchByDayResult SearchByDayResult { get; set; }
        public virtual ICollection<Video> TopVideoPopular { get; set; }
    }

    public class SearchByDayResult
    {
        public int Id { get; set; }
        public int VideoPublishCount { get; set; }
        public int VideoLiveCount { get; set; }
        public int VideoMostPopularId { get; set; }
        public virtual Video VideoMostPopular { get; set; }
    }
}
