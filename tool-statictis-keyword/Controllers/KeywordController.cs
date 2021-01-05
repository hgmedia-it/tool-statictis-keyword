using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tool_statictis_keyword.Data;
using tool_statictis_keyword.Dto;
using tool_statictis_keyword.Models.Data;

namespace tool_statictis_keyword.Controllers
{
    [Route("api/keyword")]
    [ApiController]
    public class KeywordController : ControllerBase
    {
        private ApplicationDbContext _context;
        public KeywordController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> Get(KeywordFilterDto dto)
        {
            var response = new ResponseDto<PagerDto<Keyword>>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var queries = _context.Keyword.AsQueryable();

                // basic info
                if (!string.IsNullOrEmpty(dto.Name))
                {
                    queries = queries.Where(i => i.Name.Contains(dto.Name.Trim()));
                }
                if (dto.CampaignId != 0)
                {
                    queries = queries.Where(i => i.CampaignId == dto.CampaignId);
                }
                if (dto.PageSize == 0)
                {
                    dto.PageSize = 50;
                }
                if (dto.PageNumber == 0)
                {
                    dto.PageNumber = 1;
                }
                dto.PageSize = dto.PageSize <= 0 ? 1 : dto.PageSize;
                var skipItems = (dto.PageNumber - 1) * dto.PageSize;
                var itemCount = await queries.CountAsync();
                var pageCount = (int)Math.Ceiling((double)itemCount / (double)dto.PageSize);
                var lstKeyword = await queries.OrderBy(i => i.Name.ToLower()).Skip(skipItems).Take(dto.PageSize).ToListAsync();

                response.Data = new PagerDto<Keyword>
                {
                    ItemCount = itemCount,
                    Items = lstKeyword,
                    PageCount = pageCount,
                    PageNumber = dto.PageNumber,
                    PageSize = dto.PageSize
                };
            }
            catch (Exception ex)
            {
                response.ActionState = ActionState.Error;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Keyword dto)
        {
            var response = new ResponseDto<string>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var list = dto.Name.Split(new string[] { "\t", "\r\n", ",", "\n" }, StringSplitOptions.None).ToList();
                foreach (var item in list)
                {
                    if (_context.Keyword.Any(t => t.Name.Trim().Equals(item.Trim())))
                    {
                        response.ActionState = ActionState.Warning;
                        response.Message = $"Keyword name '{item}' exist before";
                        return Ok(response);
                    }
                }
                foreach(var item in list)
                {
                    if (_context.Campaign.Where(t => t.Id == dto.CampaignId).ToList().Count == 0)
                    {
                        response.ActionState = ActionState.Warning;
                        response.Message = $"Campaign does not exist";
                        return Ok(response);
                    }
                    dto.UserId = _context.UserId;
                    dto.CampaignId = dto.CampaignId;
                    dto.Name = item.Trim();
                    _context.Keyword.Add(dto);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                response.ActionState = ActionState.Error;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(Keyword dto)
        {
            var response = new ResponseDto<string>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var keyword = await _context.Keyword.FirstOrDefaultAsync(i => i.Id == dto.Id);
                if (keyword == null)
                {
                    response.ActionState = ActionState.Error;
                    response.Message = "Keyword does not exist";
                    return Ok(response);
                }

                keyword.Name = dto.Name;
                keyword.CampaignId = dto.CampaignId;
                keyword.Note = dto.Note;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.ActionState = ActionState.Error;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPut]
        [Route("edit-range")]
        public async Task<IActionResult> EditRange(EditKeywordRangeDto dto)
        {
            _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = new ResponseDto<int>();
            try
            {
                var keywords = await _context.Keyword.Where(c => dto.Ids.Contains(c.Id)).ToListAsync();
                foreach (var item in keywords)
                {
                    item.CampaignId = dto.CampaignId;
                    item.Note = dto.Note;
                }

                response.Data = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.ActionState = ActionState.Error;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            var response = new ResponseDto<int>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var keywords = await _context.Keyword.Where(i => ids.Contains(i.Id)).ToListAsync();
                _context.Keyword.RemoveRange(keywords);
                response.Data = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.ActionState = ActionState.Error;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
