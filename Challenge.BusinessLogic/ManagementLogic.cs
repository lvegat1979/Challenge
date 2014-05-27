using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Challenge.BusinessLogic
{
    /// <summary>
    /// Management all logic with business rules.
    /// </summary>
    public class ManagementLogic
    {


        /// <summary>
        /// Evalue how is the win o lose
        /// When is iquals return I
        /// When win Paper, return P
        /// when win Rock, return R
        /// when win scissors, return S
        /// </summary>
        /// <param name="strategy1"></param>
        /// <param name="strategy2"></param>
        public static string EvaluteOptions(string strategy1, string strategy2)
        {
            if (strategy1.Equals(strategy2))
            {
                return "I";
            }
            else
            {
                if (strategy1.Equals("R"))
                {
                    if (strategy2.Equals("S"))
                    {
                        return "R"; 
                    }
                    else
                    {
                        return "P"; ;
                    }
                }

                if (strategy1.Equals("P"))
                {
                    if (strategy2.Equals("R"))
                    {
                        return "P" ;
                    }
                    else
                    {
                        return "S";
                    }
                }

                if (strategy1.Equals("S"))
                {
                    if (strategy2.Equals("P"))
                    {
                        return "S";
                    }
                    else
                    {
                        return "P";
                    }

                }
            }
            return string.Empty;
        }
    }
}
