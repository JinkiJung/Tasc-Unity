using UnityEngine;

namespace TascUnity
{
    public class JSONTest : MonoBehaviour
    {
        public int lives;
        public float health;

        public static JSONTest CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<JSONTest>(jsonString);
        }

        private void Start()
        {
            //var jd = JsonUtility.FromJson<JSONTest>("{\"lives\":3,\"health\":0.8}");

            //JsonUtility.FromJsonOverwrite("{\"name\":\"Test name\",\"description\":0.8}", t2);
            var fullString = "{\"name\":\"Test name\",\"description\":\"0.8\",\"test\":[{\"name\":\"Test name\",\"description\":\"0.8\",\"test\":[{\"name\":\"Test name\",\"description\":\"0.8\"},{\"name\":\"Test name\",\"description\":\"0.2\"}]} ],\"test2\":[{\"name\":\"Test name\",\"description\":\"0.8\",\"test\":[{\"name\":\"Test name\",\"description\":\"0.8\"}]} ]}"; // "{\"name\":\"Test name\",\"description\":\"0.8\",\"test\":[{\"name\":\"Test name\",\"description\":\"0.8\",\"test\":[{\"name\":\"Test name\",\"description\":\"0.8\"}]},{\"name\":\"Test name2\",\"description\":\"0.82\"}],\"test2\":[{\"name\":\"Test name\",\"description\":\"0.8\"}]}";
            string text = System.IO.File.ReadAllText(@".\Assets\Resources\Tasc\Json\tasc-schema.json");
            var parsedString = JSONParser.Parse(text, fullString);
            //for(int t=0; t< parsedString.Length; t++)
                //Debug.Log(parsedString[t]);

            //Debug.Log(JsonUtility.FromJson<Scenario>());// \"scenario\":[{\"name\":\"Test name\",\"description\":\"0.8\"},{\"name\":\"Test name\",\"description\":\"0.8\"}]}"));
        }

        // Given JSON input:
        // {"name":"Dr Charles","lives":3,"health":0.8}
        // this example will return a PlayerInfo object with
        // name == "Dr Charles", lives == 3, and health == 0.8f.
    }
}
