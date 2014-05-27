using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace ChallengeRestFul.Controllers
{
    public static class JSonUtil
    {

        public static JToken Merge(
        this JToken left, JToken right)
        {
            if (left.Type != JTokenType.Object)
                return right.DeepClone();

            var leftClone = (JContainer)left.DeepClone();

            MergeInto(leftClone, right);

            return leftClone;
        }


        public static void MergeInto(
                                this JContainer left, JToken right)
        {
            foreach (var rightChild in right.Children<JProperty>())
            {
                var rightChildProperty = rightChild;
                var leftProperty = left.SelectToken(rightChildProperty.Name);

                if (leftProperty == null)
                {
                    // no matching property, just add 
                    left.Add(rightChild);
                }
                else
                {
                    var leftObject = leftProperty as JObject;
                    if (leftObject == null)
                    {
                        // replace value
                        var leftParent = (JProperty)leftProperty.Parent;
                        leftParent.Value = rightChildProperty.Value;
                    }

                    else
                        // recurse object
                        MergeInto(leftObject, rightChildProperty.Value);
                }
            }
        }
    }
}