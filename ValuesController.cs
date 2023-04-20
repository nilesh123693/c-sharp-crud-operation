using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace crud_operation
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public ActionResult Get()
        {
            MySQLRepository mySQLRepository = new MySQLRepository();
            List<PersonModel> data = mySQLRepository.Read();
            return Ok(new { data });
        }
        [HttpPost]
        public ActionResult Post()
        {
            var status = false;
            var mode = Request.Form["mode"];
            var name = Request.Form["name"];
            var age = Request.Form["age"];
            var personId = Request.Form["personId"];

            MySQLRepository mySQLRepository = new();
            List<PersonModel> data = new();
            var code = "";
            switch (mode)
            {
                case "create":
                    try
                    {
                        mySQLRepository.Create(name, Convert.ToInt32(age));
                        code = ((int)ReturnCode.CREATE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;
                    }
                    break;
                case "read":
                    try
                    {
                        data = mySQLRepository.Read();
                        code = ((int)ReturnCode.READ_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;
                    }
                    break;
                case "update":
                    try
                    {
                        mySQLRepository.Update(name, Convert.ToInt32(age), Convert.ToInt32(personId));
                        code = ((int)ReturnCode.UPDATE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;
                    }
                    break;
                case "delete":
                    try
                    {
                        mySQLRepository.Delete(Convert.ToInt32(personId));
                        code = ((int)ReturnCode.DELETE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;
                    }
                    break;
                default:
                    code = ((int)ReturnCode.ACCESS_DENIED_NO_MODE).ToString();
                    break;

            }
            return Ok(new { status, code, data });
        }
    }
}

