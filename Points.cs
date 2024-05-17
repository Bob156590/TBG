using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TBG
{
    public class Points
    {
        private int _points;
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Point
        {
            get { return _points; }
            set { _points = value; }
        }

        public void WriteOutTheScoreboard()
        {
            string line;
            using (StreamReader file = new StreamReader("SaveFile.json"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    Points points = JsonConvert.DeserializeObject<Points>(line);
                    Console.WriteLine($"Name: {points._name},  Points: {points._points}");
                }
            }

        }

        public void SaveToScoreboard(Points points)
        {
            string jsonstring = File.ReadAllText("SaveFile.json");
            List<Points> sorter = JsonConvert.DeserializeObject<List<Points>>(jsonstring);
            sorter.Add(points);
            sorter.Sort((x,y) => {
                if(x._points < y._points)
                    return 1;
                else if(x._points > y._points)
                    return -1;
                return 0;
            });
            sorter.RemoveAt(3);
            jsonstring = JsonConvert.SerializeObject(sorter);
            File.WriteAllText(@"SaveFile.json", jsonstring);
        }
    }
}