﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    
    public class HomeController :BaseApiController
    {
        [HttpGet]
        public string Index()
        
        {
            return " Api is Running Successfully !!";
        }
    }
}
