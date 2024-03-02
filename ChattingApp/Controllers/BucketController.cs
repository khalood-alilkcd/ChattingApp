using Imagekit.Sdk;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;

namespace ChattingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BucketController : ControllerBase
    {
        private readonly ImagekitClient _imagekitClient;

        public BucketController(ImagekitClient imagekitClient)
        {
            _imagekitClient=imagekitClient;
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download([FromQuery] string filePath)
        {
            try
            {
                string imageUrl = _imagekitClient.Url(new Transformation()).Path(filePath).Generate();
                return StatusCode(200, imageUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"an error uccered : {ex.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                if (formCollection == null)
                    return BadRequest("Unable to read form data"); // Log: Unable to read form data

                var file = formCollection.Files.FirstOrDefault();
                if (file == null)
                    return BadRequest("No file or empty file uploaded"); // Log: No file or empty file uploaded

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)?.FileName?.Trim('"');

                // Convert IFormFile to byte[]
                using (var momeryStream = new MemoryStream())
                {
                    await file.CopyToAsync(momeryStream);
                    byte[] fileBytes =  momeryStream.ToArray();

                    FileCreateRequest request = new FileCreateRequest()
                    {
                        file = fileBytes,
                        fileName = fileName
                    };

                    Result result = _imagekitClient.Upload(request);

                    // Log: File successfully uploaded
                    string jsonResponse = JsonConvert.SerializeObject(new {message = "Image Uploaded", result = result.name});
                    return Ok(jsonResponse); 
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
