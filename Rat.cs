namespace TBG
{
    public class Rat : Entity
    {
        public Rat()
        {
            Random rnd = new Random();
            _hp = rnd.Next(1, 10);
            _dmg = rnd.Next(1, 4);
            _attackSpeed = rnd.Next(2000, 4000);
            _name = $"Rat";
            points = 10;
        }
    }
}