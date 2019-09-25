using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyImageManager.Models;
using System.IO;

namespace MyImageManager.Controllers
{
    public class DefaultController : Controller
    {
        private ImageManagerContext db = new ImageManagerContext();

        public ActionResult Index()
        {
            return View(db.MyImages.ToList());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }

            db.MyImages.Add(new Image
            {
                Name = Path.GetFileName(postedFile.FileName),
                ContentType = postedFile.ContentType,
                Data = bytes
            });

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public FileResult DownloadFile(int? fileId)
        {
            Image file = db.MyImages.ToList().Find(p => p.Id == fileId.Value);
            return File(file.Data, file.ContentType, file.Name);
        }
    }
}