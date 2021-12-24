using My_books.Data.Model;
using My_books.Data.ViewModels;

namespace My_books.Data.Services
{
    public class BookService
    {
        private readonly AppDbContext _context;
        public BookService( AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                isRead = book.isRead,
                DateRead = book.isRead ? book.DateRead.Value : null,
                Rate = book.isRead ? book.Rate.Value : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };
            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();

        public Book GetBookById(int bookId) => 
            _context.Books.FirstOrDefault(n => n.Id == bookId);
    }
}
