using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Task_Codid.Models;
using MVC_Task_Codid.Repository.FileRepository;
using System.Security.Claims;

namespace MVC_Task_Codid.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        DriveDbContext dbContext = new DriveDbContext();
        IFileRepository fileRepository;

      public FilesController (IFileRepository _fileRepository)
        {
            fileRepository=_fileRepository;
        }


        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = Int32.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            List<Models.File> AllFilesModel = fileRepository.GetAllByUserId(userId);

            return View(AllFilesModel);
        }
        [HttpPost]
        public IActionResult Index(IFormFile files)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = Int32.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (files != null)
            {
                if (files.Length > 0)
                {
                    var fileName = Path.GetFileName(files.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                  

                    var objfiles = new Models.File()
                    {
                        DocumentId = 0,
                        Name = newFileName,
                        FileType = fileExtension,
                        CreatedOn = DateTime.Now,
                        UserId= userId
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                    }

                    dbContext.Files.Add(objfiles);
                    dbContext.SaveChanges();

                }
            }
               return RedirectToAction("Index", userId);
           }

        public IActionResult Download(int fileId)
        {
            var file = fileRepository.GetById(fileId);
            if (file == null)
            {
                return NotFound();
            }

            var stream = new MemoryStream(file.DataFiles);

            var contentType = GetContentType(file.FileType);

            return File(stream, contentType, file.Name);
        }


        [HttpPost]
        public IActionResult Delete(int fileId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userId = Int32.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
           
            Models.File file = fileRepository.GetById(fileId);
            if (file != null)
            {
                fileRepository.Delete(fileId);
                dbContext.SaveChanges();

                TempData["Message"] = "Course deleted successfully.";
            }

            return RedirectToAction("Index");

        }
        private string GetContentType(string fileExtension)
        {
            // You can add more file extensions and corresponding MIME types as needed
            switch (fileExtension.ToLower())
            {
                case ".txt":
                    return "text/plain";
                case ".pdf":
                    return "application/pdf";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                default:
                    return "application/octet-stream";
            }
        }
    }
}