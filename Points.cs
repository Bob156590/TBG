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
            string jsonstring = File.ReadAllText("SaveFile.json");
            Points[] temp = JsonConvert.DeserializeObject<Points[]>(jsonstring);
            foreach (Points point in temp) Console.WriteLine($"Name: {point.Name}\nPoints: {point.Point}");
        }

        public void SaveToScoreboard(Points points)
        {
            string jsonstring = File.ReadAllText("SaveFile.json");
            Points[] temp = JsonConvert.DeserializeObject<Points[]>(jsonstring);
            List<Points> sorter = temp.ToList();
            sorter.Add(points);
            sorter.Sort((x,y) => {
                if(x._points < y._points)
                    return 1;
                else if(x._points > y._points)
                    return -1;
                return 0;
            });
            if(sorter.Count > 3) sorter.RemoveAt(3);
            jsonstring = JsonConvert.SerializeObject(sorter);
            File.WriteAllText(@"SaveFile.json", jsonstring);
        }
    }
}