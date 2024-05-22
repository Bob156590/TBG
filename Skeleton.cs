namespace TBG
{
    public class Skeleton : Entity
    {
        public Skeleton()
        {
            Random rnd = new Random();
            _hp = rnd.Next(7, 12);
            _dmg = rnd.Next(5, 7);
            _attackSpeed = rnd.Next(3000, 5000);
            _name = $"Skeleton";
            _points = 15;
        }
        public override void TakeDamge(float dmg)
        {
            _hp -= dmg;
            Console.WriteLine($"You hit {_name} for {_dmg}");
        }
        public override void Attack(Player player)
        {
            player.TakeDamge(_dmg, _name);
        }
        
    }
}