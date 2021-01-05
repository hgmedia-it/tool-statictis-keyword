using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Models.Data
{
    public class Keyword : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string Note { get; set; }
        public int CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }
        public virtual ICollection<Keyword_Video> Videos { get; set; }
    }
    public class Keyword_Video
    {
        public int KeywordId { get; set; }
        public virtual Keyword Keyword { get; set; }
        public int VideoId { get; set; }
        public virtual Video Video { get; set; }
    }
}
