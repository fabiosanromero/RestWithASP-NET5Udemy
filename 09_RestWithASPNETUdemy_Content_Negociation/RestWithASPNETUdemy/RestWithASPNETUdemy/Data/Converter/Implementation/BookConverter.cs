using RestWithASPNETUdemy.Data.Converter.Contract;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Data.Converter.Implementation
{
    public class BookConverter : IParser<BookDTO, Book>, IParser<Book, BookDTO>
    {
        public Book Parse(BookDTO origin)
        {
            if (origin == null) return null;
            return new Book
            {
                id = origin.id,
                Author=origin.Author,
                Launch_Date=origin.Launch_Date,
                Price=origin.Price,
                Title=origin.Title
            };
        }

        public BookDTO Parse(Book origin)
        {
            if (origin == null) return null;
            return new BookDTO
            {
                id = origin.id,
                Author = origin.Author,
                Launch_Date = origin.Launch_Date,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public List<Book> Parse(List<BookDTO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<BookDTO> Parse(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
