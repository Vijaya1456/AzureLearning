using AzureBlobProject.Models;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using AzureBlobProject.Services;

namespace AzureBlobProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContainer _container;
        public HomeController(IContainer container)
        {
                _container = container;
        }



        public async Task<IActionResult> Index()
        {
            return View(await _container.GetAllContainerAndBlobs());
        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
