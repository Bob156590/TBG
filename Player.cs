using System.Diagnostics;

namespace TBG
{
    public class Player : IStats
    {
        public Stopwatch playerSW = new Stopwatch();
        private float _hp = 100; //Player health point
        private int _dmg = 10; //Player damage
        private bool _block; //If Player is blocking or not
        private int _attackSpeed = 4; //How fast the Player attacks
        private int _specialPoint = 10;//Player can exchange SP to use Special Move™
        
        public float Hp
        {
            get{ return _hp; }
        }
        public int Dmg 
        {
            get{ return _dmg; }
        }
        public int AttackSpeed 
        {
            get{ return _attackSpeed; }
        }
        public int SpecialPoints
        {
            get{ return _specialPoint; }
        }
        public bool Block
        {
            get{ return _block; }
            set{ _block = value; }
        }
        /// <summary>
        /// Player attacks an enemy
        /// </summary>
        /// <param name="entity">The enemies object</param>
        /// <param name="sp">If it's a reguler att attack or a special attack</param>
        public void Attack(Entity entity, bool sp = false)
        {
            if(!sp)entity.Hp -= _dmg;
            if(sp)
            {
                entity.TakeDamge(_dmg*1.5f);
                _specialPoint -= 10;
                return;
            }
        }
        /// <summary>
        /// Player takes damage from enemy
        /// </summary>
        /// <param name="dmg">How much damage the player should take</param>
        /// <param name="name">The name of the enemy that attacked</param>
        /// <param name="bludgeon">If the attack can stun the player</param>
        public virtual void TakeDamge(float dmg, string name, bool bludgeon)
        {
            Random rnd = new Random();
            if(!bludgeon)
            {
                _hp -= dmg;
                Console.WriteLine($"The {name} delt {dmg} to you");
            }
            else
            {
                _hp = dmg;
                if(rnd.Next(10) == 0)
                {
                    playerSW.Restart();
                    Console.WriteLine("You were hit on the head a little to hard.");
                }
            }
        }
        /// <summary>
        /// Manages when the player attacks and stops.
        /// </summary>
        /// <param name="state">State of the timer</param>
        public void AttackManager(int state)
        {
            if(state == 0) playerSW.Stop();
            else if(state == 1) playerSW.Start();
            else if(state == 2) playerSW.Restart();
        }
    }
}