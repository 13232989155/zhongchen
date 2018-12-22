using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zhongchen.Base;

namespace zhongchen.Controllers
{
    public class VideoController : BaseController
    {
        public IActionResult List()
        {
            return View();
        }
    }
}