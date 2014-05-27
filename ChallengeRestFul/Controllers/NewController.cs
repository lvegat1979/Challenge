using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Challenge.BusinessLogic;
using System.Web.Script.Serialization;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ChampionshipRest.Controllers
{
    public class NewController : ApiController
    {
        
        /*
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "Text", "Text" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "Esta trabajando sobre new";
        }

        // GET api/values/5
        public string Get([FromUri]string value)
        {
            return "Obtiene Value From";
        }
        
        */
        // POST api/values

        public IEnumerable<string> Post([FromBody]object data)
        {

           JavaScriptSerializer js = new JavaScriptSerializer();

           try
           {
             //  object dict = js.Deserialize<Dictionary<string, string>>(data.ToString());
               String[][] list = js.Deserialize<string[][]>(data.ToString());
               return InfoData.ProcessData(list);
           }
           catch
           {
               try
               {
                   JContainer myContent = (((JContainer)(data)));
                   ProcessData(myContent);
               }
               catch 
               {
                   return null;
               }
           }

           return null;
           // object dict = js.Deserialize<Dictionary<string, string>>(data.ToString());

            /*
            if (myContent.Count == 2)
            {
                object dict = js.Deserialize<Dictionary<string, string>>(data.ToString());
                String[][] list = js.Deserialize<string[][]>(dict.ToString());
                return InfoData.ProcessData(list);
            }
            else
            {
                
            }

            return null;
            */
           //List<String> li = data as List<String>;

            
            //String[][][] list = js.Deserialize<string[][][]>(data.ToString());
         
           
          // return  InfoData.ProcessData(list);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myContent"></param>
        private void ProcessData(object data)
        {


            dynamic dynObj = JsonConvert.DeserializeObject(data.ToString());

            //JContainer is the base class
            var jObj = (JArray)dynObj;

            List<JArray> winner = new List<JArray>();

            JArray firstGamer;
            JArray secondGamer;

            foreach (JToken token in jObj.Children())
            {
               
                var otherArray = (JArray)token;

                for (int i = 0; i < otherArray.Count; i++)
                {
                    firstGamer =  otherArray[i][0] as JArray;
                    secondGamer = otherArray[i][1] as JArray;
                   
                    string strategyGame = ManagementLogic.EvaluteOptions(firstGamer[1].ToString(),secondGamer[1].ToString());

                    if (firstGamer[1].ToString() == strategyGame)
                    {
                        winner.Add (firstGamer);
                    }
                    else
                    {
                        winner.Add (secondGamer);
                    }
                }

            }

            if (winner.Count > 0)
            {
                GetFirtsSecond(winner);
            }
        }



        private void GetFirtsSecond(List<JArray> winner)
        {
            List<JArray> disWin = new List<JArray>();
            JArray firstGamer = new JArray();
            JArray secondGamer = new JArray();

            foreach (JArray item in winner)
            {
                //This code is for when firstGamer and seconGamer was process.
                if (firstGamer.Count > 0 && secondGamer.Count > 0)
                {
                    firstGamer = new JArray(); ;
                    secondGamer = new JArray(); ;
                }

                if (firstGamer.Count == 0)
                    firstGamer = item;
                else if (secondGamer.Count == 0)
                    secondGamer = item;

                if (firstGamer.Count > 0 && secondGamer.Count > 0)
                {
                    string strategyGame = ManagementLogic.EvaluteOptions(firstGamer[1].ToString(), secondGamer[1].ToString());

                    if (firstGamer[1].ToString() == strategyGame)
                    {
                        disWin.Add(firstGamer);
                    }
                    else
                    {
                        disWin.Add(secondGamer);
                    }
                }
            }
            if (disWin.Count > 2)
            {
                GetFirtsSecond(disWin);
            }
            else //Last Round
            {
                string strategyGame = ManagementLogic.EvaluteOptions(disWin[0][1].ToString(), disWin[1][1].ToString());
                string first = string.Empty;
                string second = string.Empty;

                if (firstGamer[1].ToString() == strategyGame)
                {
                    first = disWin[0][0].ToString();
                    second = disWin[1][0].ToString();
                }
                else
                {
                    first = disWin[1][0].ToString();
                    second = disWin[0][0].ToString();
                }
                InfoData.SetFirstSecond(first, second);
            }
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