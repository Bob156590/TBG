namespace TBG
{
    public class Skeleton : Entity
    {
        private bool weapon;
        /// <summary>
        /// All the Skeletons stats
        /// </summary>
        public Skeleton()
        { 
            Random rnd = new Random();
            weapon = rnd.Next(2) == 1;
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
            if(weapon)player.TakeDamge(_dmg, _name, weapon);
            else player.TakeDamge(_dmg*.75f, _name, weapon);
        }
        
    }
}