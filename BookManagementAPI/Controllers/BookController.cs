using BAL.Models;
using BAL.Services.Interface;
using BookManagementAPI.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(ExceptionFilter))]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("SortedByPublisherAuthorTitle")]
        public async Task<IActionResult> GetBooksSortedByPublisherAuthorTitle()
        {
            var sortedBooks = await _bookService.GetBooksSortedByPublisherAuthorTitle();
            return Ok(sortedBooks);
        }

        [HttpGet("SortedByAuthorTitle")]
        public async Task<IActionResult> GetBooksSortedByAuthorTitle()
        {
            var sortedBooks = await _bookService.GetBooksSortedByAuthorTitle();
            return Ok(sortedBooks);
        }

        [HttpGet("Totalprice")]
        public async Task<ActionResult> GetTotalPriceOfAllBooks()
        {
            var sortedBooks = await _bookService.GetTotalPriceOfAllBooks();
            return Ok(sortedBooks);
        }

        [HttpPost("SaveBooks")]
        public async Task<ActionResult> SaveBooks(List<Book> books)
        {
            await _bookService.SaveBooks(books);
            return Ok();
        }
    }
}
