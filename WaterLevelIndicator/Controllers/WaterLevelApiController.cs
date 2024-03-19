using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
//using System.Web.Mvc;
using System.Web.Routing;
using WaterLevelIndicator.Models.ViewModels;
using WaterLevelIndicator.Models;
using System.Web.Http;
using System.Net.Mail;


namespace WaterLevelIndicator.Controllers
{
    public class WaterLevelApiController : ApiController
    {
        private WaterLevelIndicationDBEntities1 _db = new WaterLevelIndicationDBEntities1();

        [HttpGet]
        [Route("api/waterlevel/{selectedLabel}")]
        public IHttpActionResult GetWaterLevelData(string selectedLabel)
        {
            var apiKey = Request.Headers.GetValues("X-API-Key").FirstOrDefault();

            if (apiKey != "water_level_indication_key_198765") 
            {
                return Unauthorized();
            }

            var waterLevelDataReplica = _db.Database.SqlQuery<WaterLevelViewModel>("exec GetAllWaterLevelDataReplicaforCurrentBox @Label", new SqlParameter("@Label", selectedLabel.ToString())).ToList();

            var lastRecord = waterLevelDataReplica.LastOrDefault();
            if (lastRecord != null && lastRecord.Measurement >= 5)
            {
                SendWarningEmail();
            }

            return Ok(waterLevelDataReplica);
        }

        //public IHttpActionResult GetWaterLevelData(string selectedLabel)
        //{
        //    var authHeader = Request.Headers.Authorization;
        //    if (authHeader == null || !authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return Unauthorized();
        //    }

        //    string encodedCredentials = authHeader.Parameter;
        //    string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
        //    string[] usernamePasswordArray = credentials.Split(':');
        //    string username = usernamePasswordArray[0];
        //    string password = usernamePasswordArray[1];

        //    // Perform authentication logic here, for example, check if username and password are valid
        //    if (!IsUserAuthenticated(username, password))
        //    {
        //        return Unauthorized();
        //    }

        //    var waterLevelDataReplica = _db.Database.SqlQuery<WaterLevelViewModel>("exec GetAllWaterLevelDataReplicaforCurrentBox @Label", new SqlParameter("@Label", selectedLabel.ToString())).ToList();

        //    var lastRecord = waterLevelDataReplica.LastOrDefault();
        //    if (lastRecord != null && lastRecord.Measurement >= 5)
        //    {
        //        SendWarningEmail();
        //    }

        //    return Ok(waterLevelDataReplica);
        //}

        //private bool IsUserAuthenticated(string username, string password)
        //{
        //    using (var db = new WaterLevelIndicationDBEntities1())
        //    {
        //        var result = db.Database.SqlQuery<int>("EXEC UserLogin @Username, @Password",
        //            new SqlParameter("@Username", username),
        //            new SqlParameter("@Password", password))
        //            .FirstOrDefault();

        //        return result == 1;
        //    }
        //}

        private void SendWarningEmail()
        {
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587; // Use 465 for SSL
            string smtpUsername = "shahrozbakht@gmail.com";
            string smtpPassword = "afot ulkj gvwl rqrs";
            const string subject = "Water Level Warning";
            const string body = "The water level has exceeded 5 meters. Please take necessary action.";

            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("shahrozbakht@gmail.com"),
                };

                mailMessage.To.Add("shahrozbakht@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                smtpClient.Send(mailMessage);
            }
        }
    }
}

