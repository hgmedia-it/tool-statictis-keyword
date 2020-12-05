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
    [Route("api/date")]
    [ApiController]
    public class DateController : ControllerBase
    {
        private ApplicationDbContext _context;
        public DateController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Add(Date dto)
        {
            var response = new ResponseDto<PagerDto<Date>>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (_context.Date.Any(t => t.DateTime.Equals(dto.DateTime)))
                {
                    response.ActionState = ActionState.Warning;
                    response.Message = $"Date time exist";
                    return Ok(response);
                }

                dto.UserId = _context.UserId;
                dto.DateTime = dto.DateTime;
                _context.Date.Add(dto);
                await _context.SaveChangesAsync();
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
                var dates = await _context.Date.Where(c => ids.Contains(c.Id)).ToListAsync();
                _context.Date.RemoveRange(dates);
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
