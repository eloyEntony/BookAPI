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
    public class BookController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BookController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        public CollectionResultDto<BookDto> GetBooks()
        {
             //var tmpCategory = _context.Categories.FirstOrDefault(x=>x.Id == )
            var books = _context.Books.Select(c => new BookDto()
            {
                Id = c.Id,
                Name = c.Name,
                CategoryName = c.Category.Name

            }).ToList();
            return new CollectionResultDto<BookDto>
            {
                IsSuccessful = true,
                Data = books
            };
        }

        [HttpGet("{id}")]
        public CollectionResultDto<BookDto> GetBook([FromRoute] int id)
        {
            var _book = _context.Books.FirstOrDefault(x => x.Id == id);

            if (_book != null)
            {
               
                List<BookDto> tmp = new List<BookDto> { new BookDto() { Name = _book.Name, Id = _book.Id } };
                return new CollectionResultDto<BookDto>
                {
                    IsSuccessful = true,
                    Data = tmp
                };
            }
            else
                return new CollectionResultDto<BookDto> { IsSuccessful = false, Message = "no item" };

        }

        [HttpPatch]
        public ResultDto UpdateBook([FromBody] BookDto dto)
        {
            try
            {
                if (dto != null)
                {
                    var tmp = _context.Books.FirstOrDefault(x => x.Id == dto.Id);
                    tmp.Name = dto.Name;
                    _context.Books.Update(tmp);
                    _context.SaveChanges();
                    return new ResultDto { IsSuccessful = true, Message = "success" };
                }
                else
                {
                    return new ResultDto { IsSuccessful = false, Message = "dto empty" };
                }
            }
            catch (Exception)
            {
                return new ResultDto { IsSuccessful = false, Message = "dto error" };
            }
        }




        [HttpDelete]
        public ResultDto DeleteBook(int id)
        {
            try
            {
                if (id != null)
                {
                    var c = _context.Books.Find(id);
                    _context.Books.Remove(c);
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
            catch (Exception)
            {
                return new ResultDto
                {
                    IsSuccessful = false,
                    Message = "Something goes wrong"
                };
            }

        }


        [HttpPost]
        public ResultDto AddBook([FromBody] BookDto newBook)
        {
            try
            {
                var tmpCategory = _context.Categories.FirstOrDefault(x => x.Name == newBook.CategoryName);
                if (newBook != null && tmpCategory !=null)
                {
                    Book book = new Book() { Name = newBook.Name, Category = tmpCategory};
                    _context.Books.Add(book);
                    _context.SaveChanges();
                    return new ResultDto { IsSuccessful = true, Message = "added book" };
                }
                else
                {
                    return new ResultDto { IsSuccessful = false, Message = "empty model" };
                }
            }
            catch (Exception)
            {
                return new ResultDto { IsSuccessful = false, Message = "not added" };
            }
        }

    }
}