using System.Diagnostics;

namespace TBG
{
    public abstract class Entity : IStats
    {
        protected int _points;//Enemies point worth
        protected Stopwatch enemySW = new Stopwatch();//Timer for attackspeed
        protected string _name;//Name
        protected float _hp;//Enemies Health points
        protected int _dmg;//Enemies Damage
        protected int _attackSpeed;//How fast they attack
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
        /// <summary>
        /// Enemy takes damge from player.
        /// </summary>
        /// <param name="dmg">How much damage the player does to the ememy</param>
        public abstract void TakeDamge(float dmg);
        /// <summary>
        /// When the enemy should attack
        /// </summary>
        /// <param name="player">The player/object the enemy attacks</param>
        public void CanAttack(Player player)
        {   
            enemySW.Start();
            if (enemySW.ElapsedMilliseconds >= AttackSpeed)
            {
                Attack(player);
                enemySW.Restart();
                enemySW.Stop();
            }
            //Stopwatch in every class insted of the program class
        }
        /// <summary>
        /// Enemy Deals damage to the enemy
        /// </summary>
        /// <param name="player">Players object</param>
        public abstract void Attack(Player player);
        public int Check()
        {
            return _points;
        }
        /// <summary>
        /// Stops the enemies time and starts it too
        /// </summary>
        /// <param name="stop"></param>
        public void Wait(bool stop = true)
        {
            if(stop) enemySW.Stop();
            if(!stop) enemySW.Start();
        }
    }
}