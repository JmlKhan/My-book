﻿using My_books.Data.Model;
using My_books.Data.Paging;
using My_books.Data.ViewModels;
using My_books.Exceptions;
using System.Text.RegularExpressions;

namespace My_books.Data.Services
{
    public class PublisherService
    {
        private readonly AppDbContext _context;
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public List<Publisher> GetAllPublishers(string? sortBy, string? searchString, int? pageNumber)
        {
            var allPublishers = _context.Publishers
                                .OrderBy(n => n.Name).ToList();

            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers
                            .OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString)).ToList();
            }

            //paging
            int pageSize = 5;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);


            return allPublishers;
        }  
        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartWithNumber(publisher.Name)) 
                    throw new PublisherNameException("Name starts with number", publisher.Name);
            
            
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPublisherById(int id) => _context
            .Publishers.FirstOrDefault(n => n.Id == id);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVm()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName)
                        .ToList()
                    }).ToList()
                }).FirstOrDefault();
            return _publisherData; 
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);

            if(_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            } else
            {
                throw new Exception($"The publisher with id {id} does not exist!");
            }
        }

        //check publisher name starts with number
        private bool StringStartWithNumber(string name) 
                                   => Regex.IsMatch(name, @"^\d");
    
        
    }
}
