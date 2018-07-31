using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailServices _mailServices;
        private readonly IDutchRepository _repository;

        public AppController(IMailServices mailServices, IDutchRepository repository)
        {
            _mailServices = mailServices;
            _repository = repository;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send Email
                _mailServices.SendMessage("ssswagatss@gmail.com", model.Subject, model.Message);
                ViewBag.UserMessage = "Mail Sent";
            }
            ViewBag.Title = "Contact";
            return View();
        }


        public IActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        public IActionResult Shop()
        {
            return View(_repository.GetProducts().ToList());
        }
    }
}