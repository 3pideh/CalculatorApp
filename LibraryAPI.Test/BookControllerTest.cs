using Library.API.Controllers;
using Library.API.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Library.API.Data.Models;

namespace LibraryAPI.Test
{
    public class BookControllerTest
    {
        BooksController _controller;
        IBookService _service;

        public BookControllerTest()
        {
            _service = new BookService();
            _controller = new BooksController(_service);
        }

        [Fact]
        public void GetAllTest()
        {
            var result = _controller.Get();
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(list.Value);

            var listBooks = list.Value as List<Book>;
            Assert.Equal(3, listBooks.Count);
        }

        [Theory]
        [InlineData("a483938e-9724-4aee-a006-6d16d19a585f", "a483938e-9724-4aee-a006-6d16d19a5222")]
        public void GetByIdTest(string id1 , string id2)
        {
            //arrange
            var validGuid = new Guid(id1);
            var invalidGuid = new Guid(id2);

            //act
            var notfoundresult = _controller.Get(invalidGuid);
            var okresult = _controller.Get(validGuid);

            //assert
            Assert.IsType<NotFoundResult>(notfoundresult.Result);
            
          

            //assert
            Assert.IsType<OkObjectResult>(okresult.Result);
            var item = okresult.Result as OkObjectResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.NotNull(bookItem);
            Assert.Equal(validGuid, bookItem.Id);
            Assert.Equal("Inferno", bookItem.Title);

        }

        [Fact]
        public void AddBookTest()
        {
            //arrange
            var completeBook = new Book()
            {
                Title = "Inferno",
                Description = "Description",
                Author = "Peter"
            };

            //act
            var createdResponse = _controller.Post(completeBook);

            //assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);

            var item = createdResponse as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(completeBook.Author , completeBook.Author);
            Assert.Equal(completeBook.Title, completeBook.Title);
            Assert.Equal(completeBook.Description, completeBook.Description);

            //arrange
            var incompleteBook = new Book()
            {
                Title = "Inferno",
                Description = "Description",

            };

            //act
            _controller.ModelState.AddModelError("Author", "Autor is Required");
            var badResponse = _controller.Post(incompleteBook);

            //assert
            Assert.IsType<BadRequestObjectResult>(badResponse);

            

        }

        [Theory]
        [InlineData("a483938e-9724-4aee-a006-6d16d19a585f", "a483938e-9724-4aee-a006-6d16d19a5555")]
        public void RemoveByIdTest(string id1, string id2)
        {
            //arrange
            var validGuid = new Guid(id1);
            var invalidGuid = new Guid(id2);

            //act
            var notfoundresult = _controller.Remove(invalidGuid);
           
            //assert
            Assert.IsType<NotFoundResult>(notfoundresult);
            Assert.Equal(3 , _service.GetAllBooks().Count());

            var okresult = _controller.Remove(validGuid);

            Assert.IsType<OkResult>(okresult);
            Assert.Equal(2, _service.GetAllBooks().Count());

        }

    }
}