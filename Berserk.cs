namespace TBG
{
    public class Berserk : Entity
    {
        private int rage;//Spcial value that gives enemy more damage over time
        public int Rage{ get{return rage;}}
        public Berserk()
        {
            Random rnd = new Random();
            _hp = rnd.Next(1, 10);
            _dmg = rnd.Next(1, 4);
            _attackSpeed = rnd.Next(2000, 4000);
            _name = $"Berserker";
            _points = 5;
        }
        public override void Attack(Player player)
        {
            player.TakeDamge(_dmg * rage* .7f, _name, false);
            rage++;
        }
        public override void TakeDamge(float dmg)
        {
            _hp -= dmg;
            Console.WriteLine($"You hit {_name} for {_dmg}");
            rage++;
        }
    }
}