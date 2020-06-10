using JsonToPdf.net.Lib;
using JsonToPdf.net.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace JsonToPdf.net.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IAwsS3 _awsS3;
        IHtmlCompiler _compiler;
        IConverter _converter;

        public ValuesController(IAwsS3 awsS3, IHtmlCompiler compiler, IConverter converter)
        {
            _awsS3 = awsS3;
            _compiler = compiler;
            _converter = converter;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] Request value)
        {
            var fileName = $"{Guid.NewGuid().ToString()}-{Guid.NewGuid().ToString()}-{Guid.NewGuid().ToString()}.pdf";
            var html = _compiler.Compile(JsonConvert.DeserializeObject(value.Data), value.Template);
            var pdf = _converter.Convert(html, true);
            var result = await _awsS3.UploadFileAsync(pdf, fileName);

            if (result.Success)
                return Ok(result.Message);
            else
                return BadRequest($"Something went wrong - {result.Message}");
        }

    }
}
