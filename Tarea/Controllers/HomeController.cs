using System;
using Microsoft.AspNetCore.Mvc;

namespace Tarea.Controllers
{
    [Controller]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public ActionResult HomePage()
        {
            return Ok(new {hola = "Amiguitos"});
        }
    }
}

