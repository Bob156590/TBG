namespace TBG
{
    public class Berserk : Entity
    {
        private int rage;//Special value that gives enemy more damage over time
        public int Rage{ get{return rage;}}
        public Berserk()
        {
            _hp = _rnd.Next(35, 50);
            _dmg = _rnd.Next(6, 9);
            _attackSpeed = _rnd.Next(2000, 4000);
            _name = $"Berserker";
            _points = 40;
        }
        public override void Attack(Player player)
        {
            player.TakeDamge(_dmg * rage* .7f, _name, false);
            rage++;
        }
        public override void TakeDamge(float dmg, int special)
        {
            base.TakeDamge(dmg, special);
            rage++;
        }
    }
}