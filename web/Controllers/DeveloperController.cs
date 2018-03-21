using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;

namespace web.Controllers
{
  [Route("api/Developer")]
  public class DeveloperController : Controller
  {
    private readonly DeveloperService _service;

    public DeveloperController(DeveloperService service)
    {
      _service = service;
    }

    [HttpGet]
    [Route("")]
    public IActionResult GetAll()
    {
     // _service.AddDeveloper(new Developer());

      var cs = Program.Configuration["ConnectionStrings:MyDbContext"];
      using (IDbConnection db = new SqlConnection(cs))
      {
        var thing = db.Query<Developer>("Select * From Developers");
        return Json(thing);
      }
    }

    [HttpPost]
    [Route("Add")]
    public IActionResult CreateDeveloper([FromBody] Developer developer)
    {
      //do send a message to the queue where the developer will actually be created
      if (_service.AddDeveloper(developer))
      {
        return new OkResult();
      }
      return BadRequest();
    }

    [HttpPut]
    [Route("Update")]
    public IActionResult UpdateDeveloper([FromBody] Developer developer)
    {
      //TODO
      return null;
    }



    [HttpDelete]
    [Route("delete")]
    public IActionResult RemoveDeveloper()
    {
      //TODO
      return null;
    }


  }

  public class Developer
  {
    public int DeveloperId { get; set; }
    public string Name { get; set; }
    public string HackerName { get; set; }
    public string Location { get; set; }
    public string Bio { get; set; }
  }
}
