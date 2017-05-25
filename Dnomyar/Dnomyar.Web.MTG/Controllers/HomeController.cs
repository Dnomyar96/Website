using Dnomyar.Web.MTG.Models.Home;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dnomyar.Web.MTG.Controllers
{
    public class HomeController : Controller
    {
        private bool connectionOpen;
        private MySqlConnection connection;

        public ActionResult Index()
        {
            GetConnection();

            var vm = new TestVM();

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM test WHERE Id = 1";

                MySqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    reader.Read();

                    if (reader.IsDBNull(0) == false)
                        vm.Id = reader.GetInt32(0);

                    if (reader.IsDBNull(1) == false)
                        vm.Name = reader.GetString(1);
                    else
                        vm.Name = "";

                    if (reader.IsDBNull(2) == false)
                        vm.IsTrue = reader.GetBoolean(2);

                    if (reader.IsDBNull(3) == false)
                        vm.Date = reader.GetDateTime(3);

                    reader.Close();

                }
                catch (MySqlException e)
                {
                    string MessageString = "Read error occurred  / entry not found loading the Column details: "
                        + e.ErrorCode + " - " + e.Message + "; \n\nPlease Continue";
                    //MessageBox.Show(MessageString, "SQL Read Error");
                    reader.Close();
                    vm.Name = MessageString;
                }
            }
            catch (MySqlException e)
            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;
                vm.Name = MessageString;
            }
            connection.Close();

            return View(vm);
        }

        public ActionResult Insert()
        {
            GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"INSERT INTO test (Naam, IsTrue, Date) VALUES ('InsertTest', 0, '{DateTime.Now}')";

                cmd.ExecuteReader();
            }
            catch (MySqlException e)
            {
                string MessageString = "The following error occurred loading the Column details: "
                    + e.ErrorCode + " - " + e.Message;
            }
            connection.Close();

            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void GetConnection()
        {
            connectionOpen = false;
            connection = new MySqlConnection();

            connection.ConnectionString = ConfigurationManager.ConnectionStrings["mtgDb"].ConnectionString;

            if (OpenLocalConnection())
            {
                connectionOpen = true;
            }
        }

        private bool OpenLocalConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}