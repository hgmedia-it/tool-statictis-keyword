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
        [HttpGet]
        public async Task<IActionResult> Get(string name, int categoryId, int pageNumber = 1, int pageSize = 50)
        {
            var response = new ResponseDto<PagerDto<Keyword>>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var queries = _context.Keyword.AsQueryable();

                // basic info
                if (!string.IsNullOrEmpty(name))
                {
                    queries = queries.Where(i => i.Name.Contains(name.Trim()));
                }
                if (categoryId != 0)
                {
                    queries = queries.Where(i => i.CategoryId == categoryId);
                }
                pageSize = pageSize <= 0 ? 1 : pageSize;
                var skipItems = (pageNumber - 1) * pageSize;
                var itemCount = await queries.CountAsync();
                var pageCount = (int)Math.Ceiling((double)itemCount / (double)pageSize);
                var lstKeyword = await queries.OrderBy(i => i.Name.ToLower()).Skip(skipItems).Take(pageSize).ToListAsync();

                response.Data = new PagerDto<Keyword>
                {
                    ItemCount = itemCount,
                    Items = lstKeyword,
                    PageCount = pageCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch(Exception ex)
            {
                response.ActionState = ActionState.Error;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Keyword dto)
        {
            var response = new ResponseDto<PagerDto<Keyword>>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var list = dto.Name.Split(new string[] { "\t", "\r\n" , ","}, StringSplitOptions.None).ToList();
                foreach(var item in list)
                {
                    if (_context.Keyword.Any(t => t.Name.Trim() == dto.Name.Trim()))
                    {
                        response.ActionState = ActionState.Warning;
                        response.Message = $"Keyword name '{dto.Name}' exist";
                        return Ok(response);
                    }
                    if(_context.Category.Where(t=> t.Id == dto.CategoryId).ToList().Count == 0)
                    {
                        response.ActionState = ActionState.Warning;
                        response.Message = $"Category does not exist";
                        return Ok(response);
                    }
                    dto.UserId = _context.UserId;
                    dto.Name = dto.Name.Trim();
                    _context.Keyword.Add(dto);
                }
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
        public async Task<IActionResult> Edit(List<Keyword> listKeyword)
        {
            var response = new ResponseDto<PagerDto<Keyword>>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                foreach(var item in listKeyword)
                {
                    var keyword = await _context.Keyword.FirstOrDefaultAsync(i => i.Id == item.Id);
                    if (keyword == null)
                    {
                        response.ActionState = ActionState.Error;
                        response.Message = "Keyword does not exist";
                        return Ok(response);
                    }

                    keyword.Name = item.Name;
                    keyword.CategoryId = item.Id;
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
        [HttpDelete]
        public async Task<IActionResult> Delete(List<int> ids)
        {
            var response = new ResponseDto<int>();

            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var keywords = await _context.Keyword.Where(c => ids.Contains(c.Id)).ToListAsync();
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
