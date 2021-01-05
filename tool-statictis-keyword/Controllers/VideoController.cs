using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using tool_statictis_keyword.Data;
using tool_statictis_keyword.Dto;
using tool_statictis_keyword.Models.Data;

namespace tool_statictis_keyword.Controllers
{
    [Route("api/video")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private ApplicationDbContext _context;
        public VideoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(List<int> keywordId)
        {
            var response = new ResponseDto<string>();
            var tasks = new Dictionary<int,Task<List<Video>>>();           
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
                var keyword = _context.Keyword.Where(x => keywordId.Contains(x.Id)).ToList();
                foreach (var item in keyword)
                {
                    if(item.Videos.Count > 0)
                    {
                        response.ActionState = ActionState.Warning;
                        response.Message = $"Keyword {item} đã tồn tại top video";
                        return Ok(response);
                    }
                    else
                    {
                        tasks.Add(item.Id, GetTopPopularVideoByKeyword(item.Name));
                        await Task.WhenAll(tasks.Select(x => x.Value));
                        foreach (var task in tasks)
                        {
                            var value = await task.Value;
                            var key = _context.Keyword.FirstOrDefault(x => x.Id == task.Key);
                            key.Videos = new List<Keyword_Video>();
                            foreach (var video in value)
                            {
                                video.UserId = _context.UserId;
                                var keyword_video = new Keyword_Video
                                {
                                    Keyword = key,
                                    Video = video

                                };

                                key.Videos.Add(keyword_video);
                            }
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.ActionState = ActionState.Error;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        public static async Task<List<Video>> GetTopPopularVideoByKeyword(string keyword)
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("headless");
                IWebDriver driver = new ChromeDriver(options);
                keyword = HttpUtility.UrlEncode(keyword);
                driver.Navigate().GoToUrl($"https://www.youtube.com/results?search_query={keyword}");
                Actions action = new Actions(driver);
                var list = driver.FindElements(By.XPath("//ytd-video-renderer"));
                while (list.Count < 100)
                {
                    action.SendKeys(Keys.Control + Keys.End).Build().Perform();
                    Thread.Sleep(2000);

                    list = driver.FindElements(By.XPath("//ytd-video-renderer"));
                }
                List<Video> listVideo = new List<Video>();
                for (int i = 1; i <= 100; i++)
                {
                    var video = new Video();
                    string xPath = $"(//ytd-video-renderer)[{i}]//*[@id='dismissable']//*[@id='badges']//span[text()='LIVE NOW']";
                    if (!IsElementExist(driver, By.XPath(xPath)))
                    {
                        var videoId = list[i].FindElement(By.XPath(".//*[@id='dismissable']//a[@id='video-title']")).GetAttribute("href").ToString().Trim();
                        var channelId = list[i].FindElement(By.XPath(".//*[@id='dismissable']//div[@id='channel-info']/a")).GetAttribute("href").ToString().Trim();
                        var channelName = list[i].FindElement(By.XPath(".//*[@id='dismissable']//*[@id='channel-info']//*[@id='channel-name']//a")).Text.Trim();
                        video.VideoId = videoId;
                        video.ChannelId = channelId;
                        video.ChannelName = channelName;
                        listVideo.Add(video);
                    }
                    if (listVideo.Count == 5)
                    {
                        break;
                    }
                }
               driver.Quit();
               return listVideo;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public static bool IsElementExist(IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
