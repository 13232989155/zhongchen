using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zhongchen.Base;

namespace zhongchen.Controllers
{
    public class ProductController : BaseController
    {

        public IActionResult List(int? productTypeId)
        {

            return View(productTypeId ?? -1);
        }


        public IActionResult Detail(int productId)
        {

            return View(productId);
        }
    }
}