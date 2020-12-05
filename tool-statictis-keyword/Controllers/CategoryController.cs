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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string name, string note, int pageNumber = 1, int pageSize = 50)
        {
            var response = new ResponseDto<PagerDto<Category>>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var queries = _context.Category.AsQueryable();

                // basic info
                if (!string.IsNullOrEmpty(name))
                {
                    queries = queries.Where(i => i.CategoryName.Contains(name.Trim()));
                }
                if (!string.IsNullOrEmpty(note))
                {
                    queries = queries.Where(i => i.Note.Contains(note.Trim()));
                }
                pageSize = pageSize <= 0 ? 1 : pageSize;
                var skipItems = (pageNumber - 1) * pageSize;
                var itemCount = await queries.CountAsync();
                var pageCount = (int)Math.Ceiling((double)itemCount / (double)pageSize);
                var lstCategory = await queries.OrderBy(i => i.CategoryName.ToLower()).Skip(skipItems).Take(pageSize).ToListAsync();

                response.Data = new PagerDto<Category>
                {
                    ItemCount = itemCount,
                    Items = lstCategory,
                    PageCount = pageCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
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
        public async Task<IActionResult> Add(Category dto)
        {
            var response = new ResponseDto<PagerDto<Category>>();
            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (_context.Category.Any(t => t.CategoryName.Trim() == dto.CategoryName.Trim()))
                {
                    response.ActionState = ActionState.Warning;
                    response.Message = $"Audio tag name '{dto.CategoryName}' exist";
                    return Ok(response);
                }

                dto.UserId = _context.UserId;
                dto.CategoryName = dto.CategoryName.Trim();
                _context.Category.Add(dto);
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
                var categories = await _context.Category.Where(c => ids.Contains(c.Id)).ToListAsync();
                _context.Category.RemoveRange(categories);
                response.Data = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.ActionState = ActionState.Error;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(Category dto)
        {
            var response = new ResponseDto<string>();

            try
            {
                _context.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var category = await _context.Category.FirstOrDefaultAsync(i => i.Id == dto.Id);
                if (category == null)
                {
                    response.ActionState = ActionState.Error;
                    response.Message = "Category does not exist";
                    return Ok(response);
                }

                category.CategoryName = dto.CategoryName;
                category.Note = dto.Note;
                await _context.SaveChangesAsync();
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
