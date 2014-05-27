using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;

namespace MvcAppWebSite.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file == null)
                return RedirectToAction("Index");

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);

                BinaryReader b = new BinaryReader(file.InputStream);
                byte[] binData = b.ReadBytes(Convert.ToInt32( file.InputStream.Length));


                SendMessage("http://localhost/challenge/api/championship/new", binData);
                //string result = System.Text.Encoding.UTF8.GetString(binData);

                //JavaScriptSerializer js = new JavaScriptSerializer();
               // String[][] list = js.Deserialize<string[][]>(result);

                //return InfoData.ProcessData(list);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            
            string msg = GetMessage("http://localhost/challenge/api/championship/top?count=1");

            JavaScriptSerializer js = new JavaScriptSerializer();
            String[] list = js.Deserialize<string[]>(msg);

            StringBuilder build = new StringBuilder();
            int line = 1;
          
            foreach (string item in list)
            {

                build.AppendLine( item.ToString() );
                       
            }
           

            ViewBag.Message = build.ToString();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Learn About Me.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Me.";

            return View();
        }

        public ActionResult Documentation()
        {
            ViewBag.Message = "About Rock-Paper-Scissors";

            return View();
        }


        public string GetMessage(string endPoint)
        {
            HttpWebRequest request = CreateWebRequest(endPoint);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("POST failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseValue = reader.ReadToEnd();
                    }
                }
                return responseValue;
            }
        }

        private HttpWebRequest CreateWebRequest(string endPoint)
        {
            var request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = "GET";
            request.ContentLength = 0;
            //request.ContentType = "text/xml";
            request.ContentType = "application/json";
            return request;
        }



        public string SendMessage(string endPoint, byte[] data)
        {
            HttpWebRequest request = CreateWebRequestPost (endPoint);

            request.ContentLength = data.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(data, 0, data.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            return string.Empty;
        }

        private HttpWebRequest CreateWebRequestPost(string endPoint)
        {
            var request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = "POST";
            request.ContentLength = 0;
            request.ContentType = "application/json";

            return request;
        }
    }
}
