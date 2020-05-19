using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TascUnity
{
    public class JSONParser
    {


        private static List<string> UnwrapJsonString(string targetString)
        {
            List<string> jsonObjList = new List<string>();
            while (true)
            {
                Regex regx = new Regex(@"\{[^\{]+?\}");

                //Regex regx = new Regex(@"""[^""\\]*(?:\\.[^""\\]*)*""\s*:\s*\{");
                //Regex regx = new Regex("\"[^\"\\\\]*(?:\\\\.[^\"\\\\]*)*\"\\s*:\\s*\\[");

                Match match = regx.Match(targetString);
                if (!match.Success)
                    break;
                jsonObjList.Add(match.Value);
                targetString = regx.Replace(targetString, ("\"?Obj" + (jsonObjList.Count - 1).ToString() + "?\""), 1);
            }
            return jsonObjList;
        }

        private static string ProcessJsonElement(string typeName, string targetString)
        {
            //var type = Type.GetType(typeName);
            //var myObject = (MyAbstractClass)Activator.CreateInstance(type);

            
            Regex regxForVariable = new Regex(@"(?:""(\w|-|\$)*"":[^\[])([^\},\{]*)");
            MatchCollection matchVariables = regxForVariable.Matches(targetString);

            for(int i=0; i< matchVariables.Count; i++)
            {
                ProcessLine(matchVariables[i].Value);
            }

            return "";
        }

        private static string ProcessLine(string lineString)
        {
            /*
            Regex regxForList = new Regex(@"(?:""(\w|-)*"")");
            MatchCollection matchVariables = regxForList.Matches(lineString);

            if (matchVariables.Count == 2)
                Debug.Log(matchVariables[0].Value + ": " + matchVariables[1].Value);
            else
                Debug.Log(matchVariables[0].Value + ": ");
                */

            string[] lines = lineString.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );
            for(int i=0; i<lines.Length; i++)
            {
                string[] splited = lines[i].Split(':');
                if (splited.Length == 2)
                    Debug.Log(splited[0] + ": " + splited[1].Trim());
            }
            

            return "";
        }

        private static void ProcessSchema(string text)
        {

            // Display the file contents to the console. Variable text is a string.
            List<string> elements = UnwrapJsonString(text);
            for(int i=0; i< elements.Count; i++)
            {
                Debug.Log(elements[i]);
                ProcessJsonElement("tt",elements[i]);
            }
        }

        public static string[] Parse(string jsonSchemaString, string jsonString)
        {
            ProcessSchema(jsonSchemaString);
            List<string> jsonObjList = new List<string>();

            //Regex regx = new Regex("(?:\"\\s *\\w + \"\\s*:\\s*\")(.*?)(?:\")");

            //jsonObjList = UnwrapJsonObject(jsonString);

            for(int i= jsonObjList.Count-1; i>=0; i--)
            {
                
            }
            
            /*
            MatchCollection matches = regx.Matches(jsonString);

            int num = 0;
            foreach (Match match in matches)
            {
                num++;
                GroupCollection groups = match.Groups;
                jsonObjList.Add(match.Value);
            }
            jsonObjList.Add(num.ToString());
            */

            /*
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
            */
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
