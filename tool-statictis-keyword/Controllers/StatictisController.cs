using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace tool_statictis_keyword.Controllers
{
    [Route("api/statictis")]
    [ApiController]
    public class StatictisController : ControllerBase
    {
       public async Task<int> GetVideoPublishCount(string keyword)
       {
            try
            {
                keyword = HttpUtility.UrlEncode(keyword);
                string url = $"https://www.youtube.com/results?search_query={keyword}&sp=EgIIAg%253D%253D";
                var client = new WebClient();
                string htmlStr = await client.DownloadStringTaskAsync(url);
                var initDataStr = htmlStr.Substring(htmlStr.IndexOf(@"{""responseContext"""));
                initDataStr = initDataStr.Substring(0, initDataStr.IndexOf("};") + 1);

                var initDataJson = JsonConvert.DeserializeObject<dynamic>(initDataStr);
                var videoJsons = initDataJson.contents.twoColumnSearchResultsRenderer.primaryContents.sectionListRenderer.contents[0]
                    .itemSectionRenderer.contents;
                return 0;
            }
            catch(Exception ex)
            {
                return 0;
            }
       }
    }
    
}
