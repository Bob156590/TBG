using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TBG
{
    public class Points
    {
        private int _totalPoints;//Total points of the player
        private string _name;//Saved name
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Point
        {
            get { return _totalPoints; }
            set { _totalPoints = value; }
        }
        /// <summary>
        /// Saves the top 3 best player in a json file
        /// </summary>
        /// <param name="points">Object with the players points and name</param>
        public void SaveToScoreboard(Points points)
        {
            string jsonstring = File.ReadAllText("SaveFile.json");
            Points[] temp = JsonConvert.DeserializeObject<Points[]>(jsonstring);
            List<Points> sorter = temp.ToList();
            sorter.Add(points);
            sorter.Sort((x,y) => {
                if(x._totalPoints < y._totalPoints)
                    return 1;
                else if(x._totalPoints > y._totalPoints)
                    return -1;
                return 0;
            });
            if(sorter.Count > 3) sorter.RemoveAt(3);
            jsonstring = JsonConvert.SerializeObject(sorter);
            File.WriteAllText(@"SaveFile.json", jsonstring);
        }
        /// <summary>
        /// Writes out up to the top 3 best players scores. 
        /// </summary>
        public void WriteOutTheScoreboard()
        {
            string jsonstring = File.ReadAllText("SaveFile.json");
            Points[] temp = JsonConvert.DeserializeObject<Points[]>(jsonstring);
            foreach (Points point in temp) Console.WriteLine($"Name: {point.Name}\nPoints: {point.Point}");
        }
    }
}