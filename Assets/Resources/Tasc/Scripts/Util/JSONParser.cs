using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TascUnity
{
    public class JSONParser
    {
        public static string[] Parse(string jsonString)
        {
            List<string> jsonObjList = new List<string>();

            string[] items = jsonString.Split('{');
            //Debug.Log(items);
            string stacked = "";
            for(int t=0; t< items.Length; t++)
            {
                if (items[t].Length == 0)
                    continue;

                var endBracket = items[t].IndexOf("}");
                var startListBracket = items[t].IndexOf("[");
                if (startListBracket >= 0 && startListBracket < endBracket)
                {

                }
                else if (endBracket >= 0)
                {   
                    jsonObjList.Add("{" + items[t].Substring(0,endBracket+1));
                }
                else
                {
                    stacked += items[t];
                }
            }

            return jsonObjList.ToArray();
        }

        public static string GetJsonObject(string jsonString, string handle)
        {
            string pattern = "\"" + handle + "\"\\s*:\\s*\\{";

            Regex regx = new Regex(pattern);

            Match match = regx.Match(jsonString);

            if (match.Success)
            {
                int bracketCount = 1;
                int i;
                int startOfObj = match.Index + match.Length;
                for (i = startOfObj; bracketCount > 0; i++)
                {
                    if (jsonString[i] == '{') bracketCount++;
                    else if (jsonString[i] == '}') bracketCount--;
                }
                return "{" + jsonString.Substring(startOfObj, i - startOfObj);
            }

            //no match, return null
            return null;
        }

        public static string[] GetJsonObjects(string jsonString, string handle)
        {
            string pattern = "\"" + handle + "\"\\s*:\\s*\\{";

            Regex regx = new Regex(pattern);

            //check if there's a match at all, return null if not
            if (!regx.IsMatch(jsonString)) return null;

            List<string> jsonObjList = new List<string>();

            //find each regex match
            foreach (Match match in regx.Matches(jsonString))
            {
                int bracketCount = 1;
                int i;
                int startOfObj = match.Index + match.Length;
                for (i = startOfObj; bracketCount > 0; i++)
                {
                    if (jsonString[i] == '{') bracketCount++;
                    else if (jsonString[i] == '}') bracketCount--;
                }
                jsonObjList.Add("{" + jsonString.Substring(startOfObj, i - startOfObj));
            }

            return jsonObjList.ToArray();
        }

        public static string[] GetJsonObjectArray(string jsonString, string handle)
        {
            string pattern = "\"" + handle + "\"\\s*:\\s*\\[\\s*{";

            Regex regx = new Regex(pattern);

            List<string> jsonObjList = new List<string>();

            Match match = regx.Match(jsonString);

            if (match.Success)
            {
                int squareBracketCount = 1;
                int curlyBracketCount = 1;
                int startOfObjArray = match.Index + match.Length;
                int i = startOfObjArray;
                while (true)
                {
                    if (jsonString[i] == '[') squareBracketCount++;
                    else if (jsonString[i] == ']') squareBracketCount--;

                    int startOfObj = i;
                    for (i = startOfObj; curlyBracketCount > 0; i++)
                    {
                        if (jsonString[i] == '{') curlyBracketCount++;
                        else if (jsonString[i] == '}') curlyBracketCount--;
                    }
                    jsonObjList.Add("{" + jsonString.Substring(startOfObj, i - startOfObj));

                    // continue with the next array element or return object array if there is no more left
                    while (jsonString[i] != '{')
                    {
                        if (jsonString[i] == ']' && squareBracketCount == 1)
                        {
                            return jsonObjList.ToArray();
                        }
                        i++;
                    }
                    curlyBracketCount = 1;
                    i++;
                }
            }

            //no match, return null
            return null;
        }
    }
}
