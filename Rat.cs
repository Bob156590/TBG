namespace TBG
{
    public class Rat : Entity
    {
        public Rat()
        {
            _hp = _rnd.Next(12, 16);
            _dmg = _rnd.Next(5, 6);
            _attackSpeed = _rnd.Next(2000, 4000);
            _name = $"Rat";
            _points = 15;
            _bludgeon = false;
        }
        public override void Attack(Player player)
        {
            player.TakeDamge(_dmg, _name, false);
        }
    }
}