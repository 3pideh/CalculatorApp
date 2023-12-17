
using LibraryMVC.Models.Models;
using LibraryMVCApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryMVCApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _service;

        public BooksController(ILogger<BooksController> logger, IBookService service)
        {
            _logger = logger;
            _service = service;
        }

        public ActionResult Index()
        {
            var result = _service.GetAll();
            return View(result);
        }

        public ActionResult Details(Guid id)
        {
            var result = _service.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book item )
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                _service.Add(item);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View();
            }
        }

        public ActionResult Delete(Guid id) {
            var result = _service.GetById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Delete(Guid id,IFormCollection collection)
        {
            try
            {
                _service.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch 
            {

                return View();
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}