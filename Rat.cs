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
            _points = 5;
        }
        public override void TakeDamge(float dmg)
        {
            _hp -= dmg;
            Console.WriteLine($"You hit {_name} for {_dmg}");
        }
        public override void Attack(Player player)
        {
            player.TakeDamge(_dmg, _name, false);
        }
    }
}