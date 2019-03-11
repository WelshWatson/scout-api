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

            string server = "localhost";
            string database = "ETS";
            string port = "3306";
            string uid = "joem";
            string password = "wP5sdnRFPF9Cl6HT";

            string connStr = $"SERVER={server};DATABASE={database};PORT={port};USER={uid};PASSWORD={password};";

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

                //Execute command
                sqlCommand.ExecuteNonQuery();
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
