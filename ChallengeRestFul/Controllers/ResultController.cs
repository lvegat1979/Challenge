using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Challenge.BusinessLogic;

namespace ChampionshipRest.Controllers
{
    public class ResultController : ApiController
    {
        /*
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "Jorge", "P" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // GET api/values/5
        public string Get([FromUri]string value)
        {
            return "Obtiene Value From";
        }
        */

        // POST api/values
        public string Post([FromUri]string first, string second)
        {

            InfoData.SetFirstSecond(first, second);
           
            return "status: 'success'";
        }

       /* // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }*/
    }
}