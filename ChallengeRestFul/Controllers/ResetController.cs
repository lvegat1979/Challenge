using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Challenge.BusinessLogic;
using System.Web.Script.Serialization;
using System.Collections;

namespace ChampionshipRest.Controllers
{
    public class ResetController : ApiController
    {
       
        // DELETE api/values/5
        public String Delete()
        {
            InfoData.ResetDataBase();

            return "status: 'success'";
        }

        public String Delete(int id)
        {
            InfoData.ResetDataBase();

            return "status: 'success'";
        }


        public string Get()
        {
            InfoData.ResetDataBase();

            return "Delete: Success";
        }


    }
}