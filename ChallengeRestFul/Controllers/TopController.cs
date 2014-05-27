using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Challenge.BusinessLogic;

namespace ChampionshipRest.Controllers
{
    /// <summary>
    /// Returns the top of winners by greater points
    /// </summary>
    public class TopController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            //return new  string[] { "Jorge", "Luis","Kimberlin" };
            return InfoData.GetTop(0);
        }

        // GET api/values/5
        public IEnumerable<string> Get([FromUri]int count)
        {
            return InfoData.GetTop(count);
        }
    }
}