using MySql.Data.MySqlClient;
using ScoutApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;

namespace ScoutApi.Controllers
{
    [RoutePrefix("api")]
    public class LocationController : ApiController
    {
        public static readonly string server = "139.162.239.235";
        public static readonly string database = "ETS";
        public static readonly string port = "3306";
        public static readonly string uid = "app";
        public static readonly string password = "AMR2019!";
        public static readonly MySqlConnection conn = new MySqlConnection($"SERVER={server};DATABASE={database};PORT={port};USER={uid};PASSWORD={password};");

        [HttpPost]
        [Route("send-coords")]
        public bool SendGeoData([FromUri] TrackingData data)
        {
            if (data == null)
            {
                throw new NullReferenceException();
            }

            try
            {
                conn.Open();

                var sqlQuery = string.Empty;

                sqlQuery =
                    $"SELECT * FROM " +
                    $" tracking_locations" +
                    $" WHERE TeamID = {data.TeamId}";

                var dbData = Convert.ToString(new MySqlCommand(sqlQuery, conn).ExecuteScalar());

                if (!string.IsNullOrEmpty(dbData))
                {
                    sqlQuery =
                $" UPDATE tracking_locations" +
                    $" SET" +
                    $" longitude = '{data.Longitude}', latitude = '{data.Latitude}'" +
                    $" WHERE TeamID = {data.TeamId}";
                }
                else
                {
                    sqlQuery =
                    $"INSERT INTO " +
                    $" tracking_locations (TeamID, longitude, latitude, lasttracked)" +
                    $" VALUES ('{data.TeamId}', '{data.Longitude}', '{data.Latitude}',  '{DateTime.Now.ToString()}')";
                }
                
                new MySqlCommand(sqlQuery, conn).ExecuteNonQuery();
            }
            catch
            {
                return false;
            }

            conn.Close();

            return true;
        }

        [HttpGet]
        [Route("teams")]
        public List<Team> GetTeams()
        {
            return ReadTeamsFromDb();
        }

        private static List<Team> ReadTeamsFromDb()
        {
            try
            {
                List<Team> response = new List<Team>();
                conn.Open();

                var sqlQuery = string.Empty;

                sqlQuery = $"SELECT * FROM Teams";

                MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();

                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);

                foreach (DataRow row in dTable.Rows)
                {
                    string id = row["TeamID"].ToString();
                    string name = row["TeamName"].ToString();

                    response.Add(new Team { TeamID = id, TeamName = name });
                }

                conn.Close();

                return response;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
    }

    public class Team
    {
        public string TeamID { get; set; }
        public string TeamName { get; set; }
    }
}
