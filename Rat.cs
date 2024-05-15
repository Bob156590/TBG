namespace TBG
{
    public class Rat : Entity
    {
        public Rat()
        {
            Random rnd = new Random();
            this._hp = rnd.Next(1, 10);
            this._dmg = rnd.Next(1, 4);
            this._attackSpeed = rnd.Next(4000, 6000);
            this._name = $"Rat";
        }
    }
}