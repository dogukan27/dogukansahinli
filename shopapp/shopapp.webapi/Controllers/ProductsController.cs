using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace shopapp.webapi.Controllers
{
    [ApiController]
    //localhost:4200/api/products (api olan yer zorunlu değil)
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        
    }
}