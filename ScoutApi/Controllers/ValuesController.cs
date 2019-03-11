using MySql.Data.MySqlClient;
using ScoutApi.Models;
using System;
using System.Web.Http;

namespace ScoutApi.Controllers
{
    [RoutePrefix("api/")]
    public class ValuesController : ApiController
    {
        public ValuesController()
        {
            
        }

        [HttpPost]
        public string SendGeoData([FromUri] tracking_locations data)
        {
            data.TeamID = 9999;
            data.teamname = "Owens team";
            data.latitude = "51.4771822";
            data.longitude = "-3.1546003";
            data.lasttracked = "1549376549";
            data.staff = 0;
            data.onsite = false;
            data.score = null;
            data.members = null;

            string connStr = "server=38fea24f2847fa7519001be390c98ae0acafe387;user=joem;database=tracking_locations;port=3306;password=wP5sdnRFPF9Cl6HT";
            var conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();


                string sql = 
                    $"INSERT INTO " +
                    $"  tracking_locations " +
                    $"  (TeamID, teamname, latitude, longitude, lasttracked, staff, onsite, score, members)" +
                    $"VALUES" +
                    $" ({data.TeamID}, " +
                    $"  {data.teamname}," +
                    $"  {data.latitude}," +
                    $"  {data.longitude}," +
                    $"  {data.lasttracked}," +
                    $"  {data.staff}, " +
                    $"  {data.onsite}, " +
                    $"  {data.score}," +
                    $"  {data.members})";

                MySqlCommand sqlCommand = new MySqlCommand(sql, conn);
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Console.WriteLine(sqlDataReader[0] + " -- " + sqlDataReader[1]);
                }

                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");

            return "";
        }
    }
}
