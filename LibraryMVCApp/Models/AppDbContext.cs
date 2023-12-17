
using LibraryMVC.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVCApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //_books = new List<Book>()
        //{
        //    new Book()
        //    {
        //        Id = new Guid("a483938e-9724-4aee-a006-6d16d19a585f"),
        //        Title= "Inferno",
        //        Description= "Description",
        //        Author= "Peter",
        //    },
        //    new Book()
        //    {
        //        Id = new Guid("f89bafc0-525b-41b0-8f78-38765d0d35b2"),
        //        Title= "Title2",
        //        Description= "Description2",
        //        Author= "Jaco",
        //    },
        //    new Book()
        //    {
        //        Id = new Guid("b06c5170-9cdf-4ebc-8482-29c409cb331b"),
        //        Title= "Title3",
        //        Description= "Description3",
        //        Author= "Jo",
        //    },
        //};

        public DbSet<Book> books { get; set; }
    }
}
