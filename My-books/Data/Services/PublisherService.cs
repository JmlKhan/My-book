using My_books.Data.Model;
using My_books.Data.ViewModels;

namespace My_books.Data.Services
{
    public class PublisherService
    {
        private readonly AppDbContext _context;
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();
        }
    }
}
