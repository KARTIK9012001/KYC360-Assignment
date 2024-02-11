using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using Project_KYC360_.Models;
using System.Data;
namespace Project_KYC360_.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeInformation : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public EmployeeInformation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllEmployees")]
        public string GetEmployees()
        {
            Microsoft.Data.SqlClient.SqlConnection con = new SqlConnection(_configuration.GetConnectionString("").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * FROM Name", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Name> namelist = new List<Name>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    Name name = new Name();
                    name.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    name.EntityId = Convert.ToInt32(dt.Rows[i]["EntityId"]);
                    name.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    name.MiddleName = Convert.ToString(dt.Rows[i]["MiddleName"]);
                    name.Surname = Convert.ToString(dt.Rows[i]["Surname"]);
                    

                }
                


            }
            if(namelist.Count > 0)
            {
               return JsonConvert.SerializeObject(namelist);

            }
            else
            {
                response.StatusCode = 100;
                response.Error = "No data found";
                return JsonConvert.SerializeObject(response);

            }
        }

    }
}
