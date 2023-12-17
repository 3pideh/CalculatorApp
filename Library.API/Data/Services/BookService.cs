using Library.API.Data.Models;

namespace Library.API.Data.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = new List<Book>()
            {
                new Book()
                {
                    Id = new Guid("a483938e-9724-4aee-a006-6d16d19a585f"),
                    Title= "Inferno",
                    Description= "Description",
                    Author= "Peter",
                },
                new Book()
                {
                    Id = new Guid("f89bafc0-525b-41b0-8f78-38765d0d35b2"),
                    Title= "Title2",
                    Description= "Description2",
                    Author= "Jaco",
                },
                new Book()
                {
                    Id = new Guid("b06c5170-9cdf-4ebc-8482-29c409cb331b"),
                    Title= "Title3",
                    Description= "Description3",
                    Author= "Jo",
                },
            };
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }
        public Book Add(Book newBook)
        {
            _books.Add(newBook);
                return newBook;
        }


        public Book GetById(Guid id)
        {
            return _books.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Remove(Guid id)
        {

          _books.Remove(GetById(id));
        }
    }
}
