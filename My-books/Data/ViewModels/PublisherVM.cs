﻿namespace My_books.Data.ViewModels
{
    public class PublisherVM
    {
        public String Name { get; set; }
    }
    public class PublisherWithBooksAndAuthorsVM
    {
        public String Name { get; set; }
        public List<BookAuthorVm> BookAuthors { get; set; }
    }

    public class BookAuthorVm
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}
