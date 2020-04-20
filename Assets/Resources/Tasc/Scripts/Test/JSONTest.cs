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

            GameObject obj2 = new GameObject();
            obj2.name = "TestMono 02";
            var t2 = obj2.AddComponent<JSONTest>();
            //JsonUtility.FromJsonOverwrite("{\"name\":\"Test name\",\"description\":0.8}", t2);
            var fullString = "{\"name\":\"Test name\",\"description\":\"0.8\",\"test\":[{\"name\":\"Test name\",\"description\":\"0.8\"},{\"name\":\"Test name2\",\"description\":\"0.82\"}]}";
            var parsedString = JSONParser.Parse(fullString);
            for(int t=0; t< parsedString.Length; t++)
                Debug.Log(parsedString[t]);

            //Debug.Log(JsonUtility.FromJson<Scenario>());// \"scenario\":[{\"name\":\"Test name\",\"description\":\"0.8\"},{\"name\":\"Test name\",\"description\":\"0.8\"}]}"));
        }

        // Given JSON input:
        // {"name":"Dr Charles","lives":3,"health":0.8}
        // this example will return a PlayerInfo object with
        // name == "Dr Charles", lives == 3, and health == 0.8f.
    }
}
