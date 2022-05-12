using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers.MyAcion
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        public readonly IMyService _IMyService;

        public MyController(IMyService iMyService)
        {
            _IMyService = iMyService;

        }

        [HttpGet]
        public int getid()
        {
            //var a = 1238;
            var a = _IMyService.Getid();
            return a;
        
        } 

    }
}
