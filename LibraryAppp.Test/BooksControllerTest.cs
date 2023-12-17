using LibraryApp.Controllers;
using LibraryApp.Data.MockData;
using LibraryAppp.Data.Models;
using LibraryAppp.Data.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryAppp.Test
{
    public class BooksControllerTest
    {
        [Fact]
        public void IndexUnitTest()
        {
            //arrange
            var mockRepo = new Mock<IBookService>();//access all methods of service
            mockRepo.Setup(n => n.GetAll()).Returns(MockData.GetTestBookItems());
            var controller = new BooksController(mockRepo.Object);

            //act
            var result = controller.Index();

            //assert            
            var viewResult =  Assert.IsType<ViewResult>(result);
            var viewResultsBook = Assert.IsAssignableFrom<List<Book>>(viewResult.ViewData.Model);
            Assert.Equal(5 , viewResultsBook.Count());
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "cd2bd817-98cd-4cf3-a80a-53ea0cd9c200")]

        public void DetailsUnitTest(string validGuid , string invalidGuid)
        {
            var mockRepo = new Mock<IBookService>();

            //arrange
            Guid validItemGuid = new Guid(validGuid);
            mockRepo.Setup(x => x.GetById(new Guid(validGuid))).Returns(MockData.GetTestBookItems().FirstOrDefault(z => z.Id == validItemGuid));
            var controller = new BooksController(mockRepo.Object);

            //act
            var result = controller.Details(validItemGuid);

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultValue = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal(validItemGuid, viewResultValue.Id);
            Assert.Equal("Managing Oneself", viewResultValue.Title);
            Assert.Equal("Peter Drucker", viewResultValue.Author);

            //arrange
            Guid invalidItemGuid = new Guid(invalidGuid);
            mockRepo.Setup(x => x.GetById(new Guid(invalidGuid))).Returns(MockData.GetTestBookItems().FirstOrDefault(z => z.Id == invalidItemGuid));

            //act
            var notfoundresult = controller.Details(invalidItemGuid);

            //assert
            Assert.IsType<NotFoundResult>(notfoundresult);
        }

        [Fact]
        public void CreateTest()
        {
            //arrange
            var mockRepo = new Mock<IBookService>();//access all methods of service
            var controller = new BooksController(mockRepo.Object);
            var newValidItem = new Book()
            {
                Author = "Author",
                Title = "Title",
                Description = "Description"
            };

            //act
            var result = controller.Create(newValidItem);

            //assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Null(redirectToActionResult.ControllerName);


            //arrange
            var newInvalidItem = new Book()
            {
                Title = "Title",
                Description = "Description"
            };
            controller.ModelState.AddModelError("Author", "The Author Value Is Required");

            //act
            var resultInvalid = controller.Create(newInvalidItem);
            //assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultInvalid);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200")]
        public void RemoveTest(string validGuid)
        {
            //arrange
            var mockRepo = new Mock<IBookService>();//access all methods of service
            mockRepo.Setup(n => n.GetAll()).Returns(MockData.GetTestBookItems());
            var controller = new BooksController(mockRepo.Object);
            var item = new Guid(validGuid);

            //act
            var result = controller.Delete(item,null); //check get action

            //assert
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Null( actionResult.ControllerName);

        }
    }
}