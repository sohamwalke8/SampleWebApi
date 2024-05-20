using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Application.Interface;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyDataController : ControllerBase
    {

        private readonly IGenericInterface _genericInterface;

        public MyDataController(IGenericInterface genericInterface)
        {
            _genericInterface = genericInterface;
        }

        [HttpGet("GetString")]
        public string Get()
        {
            var str = _genericInterface.GetValueFromEnvironment();
            return str;

        }

        [HttpGet("Exception")]
        public IActionResult Getdata()
        {
            var valOne = 11;
            var valTwo = 0;
            return Ok(valOne / valTwo);
        }

    }
}
