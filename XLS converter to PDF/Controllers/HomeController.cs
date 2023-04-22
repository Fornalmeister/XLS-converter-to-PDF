using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XLS_converter_to_PDF.Models;
using System;
using System.IO;
using SautinSoft;
using XLS_converter_to_PDF.Helpers.IHelpers;
using Microsoft.AspNetCore.Hosting.Server;
using XLS_converter_to_PDF.ViewModel;

namespace XLS_converter_to_PDF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChangeExtenstionHelper _changeExtenstionhelper;
        private readonly ICheckIsXLSHelper _checkIsXLSHelper;
        private readonly IChangeExtenstionHelper _helper;

        public HomeController(ILogger<HomeController> logger, IChangeExtenstionHelper changeExtenstionhelper, ICheckIsXLSHelper checkIsXLSHelper)
        {
            _logger = logger;
            _changeExtenstionhelper = changeExtenstionhelper;
            _checkIsXLSHelper = checkIsXLSHelper;
        }
        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost()]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            var size = files.Sum(x => x.Length);

            var filePaths = new List<string>();
            var fileNamePDFs = new List<string>();
            foreach (var formFile in files)
            {
                if (_checkIsXLSHelper.IsItXlsFile(formFile.FileName))
                {
                    if (formFile.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot\XLSFiles\", formFile.FileName);
                        filePaths.Add(filePath);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        string namePdf = await _changeExtenstionhelper.ChangeExtensionXLSToPDF(formFile.FileName);
                        string filePathPdf = Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot\PDFFiles\", namePdf);
                        ExcelToPdf x = new ExcelToPdf();
                        x.ConvertFile(filePath, filePathPdf);
                        fileNamePDFs.Add(namePdf);

                    }
                }
                else
                {
                    return RedirectToAction("BadFile");
                }
            }

            GeneratedPDF newPdfs = new GeneratedPDF
            {
                Count = files.Count,
                Size = size,
                FilePaths = filePaths,
                FileNamePDFs = fileNamePDFs
            };

            return RedirectToAction("GeneratedPdfs", "Home", newPdfs);
        }

        [HttpGet]
        public async Task<IActionResult> GeneratedPdfs(GeneratedPDF pdfs)
        {
            return View(pdfs);
        }

        [HttpGet]
        public async Task<IActionResult> BadFile()
        {
            return View();
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