using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using AzureBlobProject.Models;
using IContainer = AzureBlobProject.Services.IContainer;
using Container = AzureBlobProject.Models.Container;

namespace AzureBlobProject.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainer _contianerservice;

        public ContainerController(IContainer containerservice)
        {
            _contianerservice = containerservice;
        }
        public async Task<IActionResult> Index()
        {
            var allcontainer=await _contianerservice.GetAllContainer();
            return View(allcontainer);
        }
        public async Task<IActionResult> Delete(string containerName)
        {
            await _contianerservice.DeleteContainer(containerName);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Create()
        {
            
            return View(new Container());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Container container)
        {
            await _contianerservice.CreateContainer(container.Name);
            return RedirectToAction("Index");
        }


    }
}
