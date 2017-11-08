using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using maintenance.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using NetCoreBBS.Entities;

namespace maintenance.Controllers
{
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment hostingEnv;
        private SignInManager<User> SignInManager;
        public HomeController(IHostingEnvironment env, ILogger<HomeController> logger, SignInManager<User> signInManager)
        {
            this.hostingEnv = env;
            this._logger = logger;
            this.SignInManager = signInManager;

        }
        public IActionResult Index()
        {
           if(SignInManager.IsSignedIn(User))
                return View();
           else
               return RedirectToAction("Login", "Account");


        }

        [HttpPost]
        public IActionResult New([FromServices]IHostingEnvironment env,  UserViewModel user)
        {

            //var fileName = Path.Combine("upload", DateTime.Now.ToString("MMddHHmmss") + ".jpg");
            //using (var stream = new FileStream(Path.Combine(env.WebRootPath, fileName), FileMode.CreateNew))
            //{
            //    user.IdCardImg.CopyTo(stream);
            //}

            //var users = dbContext.Set<User>();
            //var dbUser = new User()
            //{
            //    Name = user.Name,
            //    IdCardNum = user.IdNum,
            //    IdCardImgName = fileName
            //};
            //users.Add(dbUser);
            //dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
   public IActionResult UploadFiles(IList<IFormFile> files)
    {
        long size = 0;
        foreach(var file in files)
        {
            var filename = ContentDispositionHeaderValue
                            .Parse(file.ContentDisposition)
                            .FileName
                           .Trim('"');
           filename = hostingEnv.WebRootPath + $@"\{filename}";
           size += file.Length;
           using (FileStream fs = System.IO.File.Create(filename))
           {
              file.CopyTo(fs);
              fs.Flush();
           }
       }
    
       ViewBag.Message = $"{files.Count} file(s) / {size} bytes uploaded successfully!";
       return View();
   }
        [HttpPost]
    public IActionResult UploadFilesAjax()
    {
       long size = 0;
        var files = Request.Form.Files;
       foreach (var file in files)
       {
          var filename = ContentDispositionHeaderValue
                           .Parse(file.ContentDisposition)
                           .FileName
                           .Trim('"');
         filename = hostingEnv.WebRootPath + $@"\{filename}";
           size += file.Length;
           using (FileStream fs = System.IO.File.Create(filename))
        {
             file.CopyTo(fs);
              fs.Flush();
          }
      }
      string message = $"{files.Count} file(s) /{size} bytes uploaded successfully!";
      return Json(message);
 }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
