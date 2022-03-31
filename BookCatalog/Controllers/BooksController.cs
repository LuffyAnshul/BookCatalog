using BookCatalog.Dtos;
using BookCatalog.Modals;
using BookCatalog.Modals.Repo;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController: ControllerBase
    {
        private IBook _BookRepo;
        public BooksController(IBook bookRepo)
        {
            _BookRepo = bookRepo;
            // _BookRepo = new InMemBookRepo();
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetBook()
        {
            return _BookRepo.GetBooks()
                .Select(x => new BookDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                })
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<BookDTO> GetBook(Guid id)
        {
            var book = _BookRepo.GetBook(id);

            if(book == null)
            {
                return NotFound();
            }

            var bookDTO = new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price
            };
            return bookDTO;
        }

        [HttpPost]
        public ActionResult CreateBook(CreateOrUpdateBook book)
        {
            var myBook = new Book();
            myBook.Id = Guid.NewGuid();
            myBook.Title = book.Title;  
            myBook.Price = book.Price;

            _BookRepo.CreateBook(myBook);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<IBook> UpdateBook(Guid id, CreateOrUpdateBook book)
        {
            var myBook = _BookRepo.GetBook(id);
            myBook.Title = book.Title;
            myBook.Price = book.Price;

            if(myBook == null)
            {
                return NotFound();
            }

            _BookRepo.UpdateBook(id, myBook);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBook(Guid id)
        {
            var book = _BookRepo.GetBook(id);
            if (book == null) return NotFound();

            _BookRepo.DeleteBook(id);

            return Ok();
        }
    }
}
