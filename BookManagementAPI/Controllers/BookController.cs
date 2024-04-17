using BAL.Models;
using BAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Sorted list of books by Publisher, Author (last, first), then title.
        /// </summary>
        /// <returns></returns>
        [HttpGet("SortedByPublisherAuthorTitle")]
        public async Task<IActionResult> GetBooksSortedByPublisherAuthorTitle()
        {
            var sortedBooks = await _bookService.GetBooksSortedByPublisherAuthorTitle();
            return Ok(sortedBooks);
        }

        /// <summary>
        /// Sorted list of books by Author (last, first) then title.
        /// </summary>
        /// <returns></returns>
        [HttpGet("SortedByAuthorTitle")]
        public async Task<IActionResult> GetBooksSortedByAuthorTitle()
        {
            var sortedBooks = await _bookService.GetBooksSortedByAuthorTitle();
            return Ok(sortedBooks);
        }

        /// <summary>
        /// Total price of all books
        /// </summary>
        /// <returns></returns>
        [HttpGet("Totalprice")]
        public async Task<ActionResult> GetTotalPriceOfAllBooks()
        {
            var sortedBooks = await _bookService.GetTotalPriceOfAllBooks();
            return Ok(sortedBooks);
        }

        /// <summary>
        /// Insert the bulk books
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     [
        ///         {
        ///            "publisher": "Subha",
        ///            "title": "Money Mangement",
        ///            "authorLastName": "Murugan",
        ///            "authorFirstName": "Varun",
        ///            "price" : 12.00,
        ///            "yearPublished" : 2022,
        ///            "cityPublished" : "Chennai"
        ///         }
        ///     ]
        /// </remarks>
        [HttpPost("SaveBooks")]
        public async Task<ActionResult> SaveBooks(List<Book> books)
        {
            await _bookService.SaveBooks(books);
            return Ok();
        }
    }
}
