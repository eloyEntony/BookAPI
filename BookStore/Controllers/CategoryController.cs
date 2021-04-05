using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Dto;
using BookStore.Models.Dto.ResultDto;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CategoryController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        public CollectionResultDto<CategoryDto> GetCategories()
        {
            var categories =_context.Categories.Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return new CollectionResultDto<CategoryDto>
            {
                IsSuccessful = true,
                Data = categories
            };
        }

        [HttpGet("{id}")]
        public CollectionResultDto<CategoryDto> GetCategory([FromRoute]int id)
        {
            var _category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (_category != null)
            {
                List<CategoryDto> tmp = new List<CategoryDto> { new CategoryDto() { Name = _category.Name, Id = _category.Id } };
                return new CollectionResultDto<CategoryDto>
                {
                    IsSuccessful = true,
                    Data = tmp
                };
            }
            else
                return new CollectionResultDto<CategoryDto> { IsSuccessful = false, Message = "no item" };
            
        }

        [HttpPatch]
        public ResultDto UpdateCategory([FromBody] CategoryDto dto)
        {
            try
            {
                if (dto != null)
                {
                    var tmp = _context.Categories.FirstOrDefault(x => x.Id == dto.Id);
                    tmp.Name = dto.Name;
                    _context.Categories.Update(tmp);
                    _context.SaveChanges();
                    return new ResultDto { IsSuccessful = true, Message = "success" };
                }
                else
                {
                    return new ResultDto { IsSuccessful = false, Message = "dto empty" };
                }
            }
            catch(Exception)
            {
                return new ResultDto { IsSuccessful = false, Message = "dto error" };
            }
        }


    

        [HttpDelete]
        public ResultDto DeleteCategory( int id)
        {
            try
            {
                if (id != null)
                {
                    var c = _context.Categories.Find(id);
                    _context.Categories.Remove(c);
                    _context.SaveChanges();
                    return new ResultDto
                    {
                        IsSuccessful = true,
                        Message = "Successfully deleted"
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccessful = false,
                        Message = "Id is not defined"
                    };
                }
            }
            catch(Exception)
            {
                return new ResultDto
                {
                    IsSuccessful = false,
                    Message = "Something goes wrong"
                };
            }

        }


        [HttpPost]
        public ResultDto AddCategory([FromBody]CategoryDto newCategory)
        {
            try
            {
                if (newCategory != null)
                {
                    Category category = new Category() { Name = newCategory.Name };
                    _context.Categories.Add(category);
                    _context.SaveChanges();
                    return new ResultDto { IsSuccessful = true, Message = "added" };
                }
                else
                {
                    return new ResultDto { IsSuccessful = false, Message = "empty model" };
                }
            }
            catch(Exception)
            {
                return new ResultDto { IsSuccessful = false, Message = "not added" };
            }            
        }

        [HttpGet("books/{id}")]
        public CollectionResultDto<BookDto> GetBooksFromCategory([FromRoute]int id)
        {
            var _category = _context.Categories.FirstOrDefault(x => x.Id == id).Name;

            if (_category != null)
            {
                List<Book> booksFromSelectCategory = _context.Books.Where(x => x.Category.Name == _category).ToList();

                List<BookDto> tmp = new List<BookDto>();
                foreach (var item in booksFromSelectCategory)
                {
                    tmp.Add(new BookDto { Name = item.Name, CategoryName = item.Category.Name, Id = item.Id });
                }               
                   
                
                return new CollectionResultDto<BookDto>
                {
                    IsSuccessful = true,
                    Data = tmp
                };
            }
            else
                return new CollectionResultDto<BookDto> { IsSuccessful = false, Message = "no item" };
        }
    }
}