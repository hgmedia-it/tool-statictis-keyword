using System;
using System.Collections.Generic;
using System.Globalization;
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
    [Route("api/campaign")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private ApplicationDbContext _context;
        public CampaignController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> Get(CampaignFilterDto dto)
        {
            var response = new ResponseDto<PagerDto<Campaign>>();
            try                                                             
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var queries = _context.Campaign.AsQueryable();

                // basic info
                if (!string.IsNullOrEmpty(dto.Name))
                {
                    queries = queries.Where(i => i.Name.Contains(dto.Name.Trim()));
                }
                if (!string.IsNullOrEmpty(dto.Description))
                {
                    queries = queries.Where(i => i.Description.Contains(dto.Description.Trim()));
                }

                if (dto.StartDate != null && !dto.StartDate.Equals(default(DateTime)))
                {

                    queries = queries.Where(i => DateTime.Compare(dto.StartDate.Date,i.StartDate.Date) <= 0);
                }
                if (dto.EndDate != null && !dto.StartDate.Equals(default(DateTime)))
                {
                    queries = queries.Where(i => DateTime.Compare(dto.EndDate.Date, i.EndDate.Date) >= 0);
                }
                if(dto.PageSize == 0)
                {
                    dto.PageSize = 50;
                }
                if(dto.PageNumber == 0)
                {
                    dto.PageNumber = 1;
                }
                dto.PageSize = dto.PageSize <= 0 ? 1 : dto.PageSize;
                var skipItems = (dto.PageNumber - 1) * dto.PageSize;
                var itemCount = await queries.CountAsync();
                var pageCount = (int)Math.Ceiling((double)itemCount / (double)dto.PageSize);
                var lstCampaign = await queries.OrderBy(i => i.Name.ToLower()).Skip(skipItems).Take(dto.PageSize).ToListAsync();

                response.Data = new PagerDto<Campaign>
                {
                    ItemCount = itemCount,
                    Items = lstCampaign,
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
        public async Task<IActionResult> Add(Campaign dto)
        {
            var response = new ResponseDto<PagerDto<Campaign>>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var queries = _context.Campaign.AsQueryable();
                if (_context.Campaign.Any(t => t.Name.Trim() == dto.Name.Trim()))
                {
                    response.ActionState = ActionState.Warning;
                    response.Message = $"Campaign name '{dto.Name}' exist";
                    return Ok(response);
                }
                dto.StartDate = DateTime.Parse(dto.StartDate.ToShortDateString());
                dto.EndDate = DateTime.Parse(dto.EndDate.ToShortDateString());
                DateTime dateNow = DateTime.Parse(DateTime.Now.ToShortDateString());
                if (DateTime.Compare(dto.EndDate, dateNow) < 0)
                {
                    response.ActionState = ActionState.Error;
                    response.Message = $"End date cannot ealier than date time now";
                    return Ok(response);
                }
                else if(DateTime.Compare(dto.StartDate, dto.EndDate) > 0)
                {
                    response.ActionState = ActionState.Error;
                    response.Message = $"End date cannot ealier than Start Date";
                    return Ok(response);

                }
                else if(DateTime.Compare(dto.StartDate, dateNow) < 0)
                {
                    response.ActionState = ActionState.Error;
                    response.Message = $"Start date cannot ealier than date time now";
                    return Ok(response);
                }
                dto.UserId = _context.UserId;
                _context.Campaign.Add(dto);
                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                response.ActionState = ActionState.Error;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(Campaign dto)
        {
            var response = new ResponseDto<PagerDto<Campaign>>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var queries = _context.Campaign.AsQueryable();
                var campaign = await _context.Campaign.FirstOrDefaultAsync(i => i.Id == dto.Id);
                if (campaign == null)
                {
                    response.ActionState = ActionState.Error;
                    response.Message = "Campaign does not exist";
                    return Ok(response);
                }
                if (_context.Campaign.Any(t => t.Name.Trim() == dto.Name.Trim()))
                {
                    response.ActionState = ActionState.Warning;
                    response.Message = $"Campaign name '{dto.Name}' exist";
                    return Ok(response);
                }
                if (DateTime.Compare(dto.EndDate, DateTime.Now) < 0 && DateTime.Compare(dto.StartDate, dto.EndDate) > 0 && DateTime.Compare(dto.StartDate, DateTime.Now) < 0)
                {
                    response.ActionState = ActionState.Warning;
                    response.Message = $"Invalid date time";
                    return Ok(response);
                }
                campaign.Name = dto.Name;
                campaign.Description = dto.Description;
                campaign.StartDate = dto.StartDate;
                campaign.EndDate = dto.EndDate;
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
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
                    var campaigns = await _context.Campaign.Where(i => ids.Contains(i.Id)).ToListAsync();
                    _context.Campaign.RemoveRange(campaigns);
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
