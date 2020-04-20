using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TascUnity
{
    class StateLogger
    {
        List<string> stateList;

        public StateLogger()
        {
            stateList = new List<string>();
        }

        public void Store(string fileNamePrefix)
        {
            if (stateList.Count == 0)
                return;
            string timeNow = System.DateTime.Now.ToString("yyyyMMdd-HHmmss");
            string fileNameFull = fileNamePrefix + "_" + timeNow + ".csv";
            FileStream f = new FileStream(fileNameFull, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
            for (int i = 0; i < stateList.Count; i++)
            {
                writer.WriteLine(stateList[i]);
                Debug.Log(stateList[i]);
            }
                
            writer.Close();
            Debug.Log("Log file stored! - " + fileNameFull);
        }

        public void StoreALog(State state)
        {
            string timeNow = System.DateTime.Now.ToString("yyyy:MM:dd-HH:mm:ss:ffff");
            stateList.Add(timeNow + ", " + state.ToString() );
        }
    }
}
