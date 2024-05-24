namespace TBG
{
    public class Skeleton : Entity
    {
        /// <summary>
        /// All the Skeletons stats
        /// </summary>
        public Skeleton()
        { 
            _bludgeon = _rnd.Next(2) == 1;
            _hp = _rnd.Next(20, 30);
            _dmg = _rnd.Next(5, 7);
            _attackSpeed = _rnd.Next(3000, 5000);
            _name = $"Skeleton";
            _points = 25;
        }
        public override void Attack(Player player)
        {
            if(!_bludgeon)player.TakeDamge(_dmg, _name, _bludgeon);
            else player.TakeDamge(_dmg*.75f, _name, _bludgeon);
        }
        
    }
}