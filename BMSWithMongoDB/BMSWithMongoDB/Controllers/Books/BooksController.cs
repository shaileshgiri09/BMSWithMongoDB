using BMS.Domain.Model;
using BMS.Infrastructure.IService;
using BMS.Infrastructure.Service;
using BMSWithMongoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMSWithMongoDB.Controllers.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _bookService;
        private readonly ICategoryService _categoryService;

        public BooksController(IBooksService booksService, ICategoryService categoryService)
        {
            _bookService = booksService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Book> books =  await _bookService.GetAsync();
            /*foreach (var book in books)
            {
                var category = await _categoryService.GetAsync(book.Category);
                if (category is null)
                {
                    book.Category = null!;
                }
                book.Category = category.Name;
            }*/
            return Ok(books);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            Book books = await _bookService.GetAsync(id);

            if(books is null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDTO newBook)
        {
            Book book = new Book
            {
                BookName = newBook.BookName,
                Price = newBook.Price,
                Category = newBook.Category,
                Author = newBook.Author
            };
            await _bookService.CreateBook(book);

            return CreatedAtAction(nameof(Get), new { id = book.Id }, newBook);
        }

        [HttpPut]
        public async Task<IActionResult> UdpateBook(Book updateBook)
        {
            var book =  await _bookService.GetAsync(updateBook.Id);

            if(book is null)
            {
                return NotFound();
            }

            await _bookService.UpdateBook(updateBook);

            return NoContent();
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var book = await _bookService.GetAsync(id);
            if(book is null)
            {
                return NotFound();
            }

            await _bookService.DeleteBook(id);

            return NoContent();
        }






    }
}
