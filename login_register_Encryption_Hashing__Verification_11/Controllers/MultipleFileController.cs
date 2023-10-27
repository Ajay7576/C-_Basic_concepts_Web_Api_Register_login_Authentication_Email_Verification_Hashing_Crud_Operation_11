using login_register_Encryption_Hashing__Verification_11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace login_register_Encryption_Hashing__Verification_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleFileController : Controller
    {
        public string ErrorMessage { get; set; }
        public IConfiguration _configuration;

        public MultipleFileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("FileUpload")]
        public IActionResult PostMultipleFile([FromForm] FileUploadModel fileData)
        {

            if (fileData == null)
            {
                return BadRequest("Please select file");
            }

            string getFile = Path.Combine(Directory.GetCurrentDirectory() + "/Files/");
            string[] Files = Directory.GetFiles(getFile).ToArray();

            var fileWithPath = string.Empty;
            foreach (var file in Files)
            {
                string fileName = Path.GetFileName(file);

              
                foreach (var f1 in fileData.Files)
                {
                    

                    string name = f1.FileName;

                    var stream = f1.OpenReadStream();
                    BinaryReader br = new System.IO.BinaryReader(stream);
                    Byte[] bytes = br.ReadBytes((Int32)stream.Length);
                 

                    var extension = Path.GetExtension(name);
                    var nameWithoutExtension = Path.GetFileNameWithoutExtension(name);
                    var i = DateTime.Now.Ticks;


                    if (fileName == name)
                    {

                        fileName = nameWithoutExtension.Trim() + " (" + i + ")" + extension;

                        PathSaveInDrive(fileData,fileName);

                    }
              

                    PathSaveInDrive(fileData, fileName);
                }

            }

            return Ok("Successfully Loaded files");
        }

        private void PathSaveInDrive(FileUploadModel fileData ,string fileName)
        {
            var path = _configuration.GetSection("DrivePath").Value;
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), path);


            foreach (var f in fileData.Files)
            {

                var setDrivePath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(setDrivePath, FileMode.Create))
                {
                    f.CopyTo(stream);
                }

            }
        }
    }
}



