using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using knowledgenetwork.Models;
using knowledgenetwork.Repositories.Interfaces;
using knowledgenetwork.Repositories;
using knowledgenetwork.Controllers.Dtos;

namespace knowledgenetwork.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlogPost()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult BlogPage(int? id)
        {
            return View();
        }

        public IActionResult GetComments()
        {
            return View();
        }
    }
}
