using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using ProfileSample.DAL;
using ProfileSample.Models;

namespace ProfileSample.Controllers
{
    public class HomeOptimizedController : Controller
    {
        private const string ImagesDirectoryPath = "../../../MP.ProfilingOptimization.Task2/Content/Img";
        private const string ImagesFileExtension = "*.jpg";

        private readonly ProfileSampleEntities _context;

        public HomeOptimizedController()
        {
            _context = new ProfileSampleEntities();
        }

        public ActionResult Index()
        {
            using (_context)
            {
                var sources = _context.ImgSources.Take(20);

                var model = sources.Select(img => new ImageModel
                {
                    Name = img.Name,
                    Data = img.Data
                }).ToList();

                return View(model);
            }
        }

        public ActionResult Convert()
        {
            var files = Directory.GetFiles(ImagesDirectoryPath, ImagesFileExtension); // for unit test
            //var files = Directory.GetFiles(Server.MapPath("~/Content/Img"), ImagesFileExtension); // for run test

            using (_context)
            {
                _context.ImgSources.ForEach(item => _context.ImgSources.Remove(item));

                _context.ImgSources.AddRange(files.Select(file => new ImgSource
                {
                    Name = Path.GetFileName(file),
                    Data = DecreaseImageResolution(System.IO.File.ReadAllBytes(file), 20)
                }));

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Private methods

        private byte[] DecreaseImageResolution(byte[] originBytes, int jpegQuality)
        {
            using (var inputStream = new MemoryStream(originBytes))
            {
                var image = Image.FromStream(inputStream);

                var jpegEncoder = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);

                var encoderParameters = new EncoderParameters(1)
                {
                    Param = { [0] = new EncoderParameter(Encoder.Quality, jpegQuality) }
                };

                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, jpegEncoder, encoderParameters);
                    return outputStream.ToArray();
                }
            }
        }
        #endregion
    }
}