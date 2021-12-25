namespace My_books.Data.Model
{
    public class Publisher
    {
        public int Id { get; set; }
        public String Name { get; set; }

        //Navigation Properties
        public List<Book> Books { get; set; }
    }
}
