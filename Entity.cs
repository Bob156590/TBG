using System.Diagnostics;

namespace TBG
{
    public class Entity : IStats
    {
        public Entity()
        {
        }
        Stopwatch enemySW = new Stopwatch();
        protected new string _name;
        protected float _hp;
        protected int _dmg;
        protected int _attackSpeed;
        public float Hp
        {
            get{ return _hp; }
            set{ _hp = value; }
        }
        public int Dmg 
        {
            get{ return _dmg; }
        }
        public int AttackSpeed 
        {
            get{ return _attackSpeed; }
        }
        public void SkrivUt()
        {
            Console.WriteLine($"{_name}: {_hp}");
        }
        public virtual void TakeDamge(float dmg)
        {
            _hp -= dmg;
            Console.Write($"You hit {_name} for {_dmg}");
        }
        public void CanAttack()
        {
            //Stopwatch in every class insted of the program class
        }
        public virtual void Attack(Player player)
        {
            player.TakeDamge(_dmg, _name);
        }
    }
}