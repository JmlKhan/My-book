namespace My_books.Data.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //Navigation Properties
        public List<Book_Author> Book_Authors { get; set; }
    }
}
