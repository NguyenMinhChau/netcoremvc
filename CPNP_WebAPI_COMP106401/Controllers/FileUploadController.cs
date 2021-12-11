using CPNP_WebAPI_COMP106401.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public FileUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload objfile)
        {
            if(objfile.files.Length > 0)
            {
                try
                {
                    if(!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + objfile.files.FileName))
                    {
                        objfile.files.CopyTo(filestream);
                        filestream.Flush();
                        return "\\uploads\\" + objfile.files.FileName;
                    }
                }
                catch(Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Unsuccessful";
            }
        }
    }
}
