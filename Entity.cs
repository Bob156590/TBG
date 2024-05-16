using System.Diagnostics;

namespace TBG
{
    public class Entity : IStats
    {
        public Entity()
        {
        }
        protected int points;
        protected Stopwatch enemySW = new Stopwatch();
        protected string _name;
        protected float lastAttack;
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
            Console.WriteLine($"You hit {_name} for {_dmg}");
        }
        public void CanAttack(Player player)
        {   
            enemySW.Start();
            lastAttack = enemySW.ElapsedMilliseconds;
            if (lastAttack >= AttackSpeed)
            {
                lastAttack = 0;
                Attack(player);
                enemySW.Restart();
                enemySW.Stop();
            }
            //Stopwatch in every class insted of the program class
        }
        public virtual void Attack(Player player)
        {
            player.TakeDamge(_dmg, _name);
        }
        public int Check()
        {
            return points;
        }
        public void Wait(bool stop = true)
        {
            if(stop) enemySW.Stop();
            if(!stop) enemySW.Start();
        }
    }
}