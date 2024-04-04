using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace AzureBlobProject.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlob _blobservice;
        public BlobController(IBlob blobservice)
        {
            _blobservice = blobservice;     
        }
        [HttpGet]
        public async Task<IActionResult> Manage(string containerName)
        {
            var blobsObj= await _blobservice.GetAllBlobs(containerName);
            return View(blobsObj);
        }
        [HttpGet]
        public async Task<IActionResult> AddFile(string containerName)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(string containerName,IFormFile file)
        {
            if(file==null || file.Length<1)
            return View();

            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
            var result = await _blobservice.UploadBlob(fileName, file, containerName);
            if(result)
            
                return RedirectToAction("Index","Container");

            
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ViewFile(string name,string containerName)
        {
            return Redirect(await _blobservice.GetBlob(name,containerName));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteFile(string name, string containerName)
        {
            await _blobservice.DeleteBlob(name, containerName);
            return RedirectToAction("Index", "Home"); 
        }
    }
}
