using My_books.Data.Model;

namespace My_books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                //if(!context.Books.Any())
                //{
                //    context.Books.AddRange(
                //    new Book()
                //    {
                //        Title = "1st Book Title",
                //        Description = "1st book description",
                //        isRead = true,
                //        DateAdded = DateTime.Now.AddDays(10),
                //        Rate =  4,
                //        Genre = "Biography",
                //        Author = "First Author",
                //        CoverUrl = "https.....",
                //        DateRead = DateTime.Now
                //    },
                //    new Book()
                //    {
                //        Title = "2st Book Title",
                //        Description = "2st book description",
                //        isRead = false,
                //        DateAdded = DateTime.Now.AddDays(-10),
                //        Rate = 5,
                //        Genre = "Fiction",
                //        Author = "Second Author",
                //        CoverUrl = "https.....",
                //        DateRead = DateTime.Now.AddDays(-5)
                //    });

                //    context.SaveChanges();
                //}
            }
        }
    }
}
