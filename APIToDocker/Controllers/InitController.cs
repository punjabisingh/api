using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIToDocker.Domain;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIToDocker.Controllers
{
    [Route("api/[controller]")]
    public class InitController : Controller
    {
        [HttpGet]
        public string Get()
        {
            SQL _sql = new SQL();
            try{
                _sql.InitDB();
            }catch(Exception ex){
                return "Error : " + ex.Message.ToString();
            }

            return "Initilization of DB completed";
        }


    }
}
