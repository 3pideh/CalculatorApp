using Library.API.Data.Models;

namespace Library.API.Data.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book Add( Book newBook );  
        Book GetById(Guid id );
        void Remove(Guid id );
    }
}
