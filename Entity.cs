using System.Diagnostics;

namespace TBG
{
    public abstract class Entity : IStats
    {
        protected bool _bludgeon;//If the enemies attack can stun or not
        protected bool _burn;//Bool for when an enemy is burning
        protected int _burnTimer;//How long the enemy is going to get burned
        protected Random _rnd = new Random();//Random to use in diffrent places
        protected int _points;//Enemies points/money worth
        protected Stopwatch _enemySW = new Stopwatch();//Timer for attackspeed
        protected string _name;//Name
        protected float _hp;//Enemies Health points
        protected int _dmg;//Enemies Damage
        protected int _attackSpeed;//How fast they attack
        public int Points
        {
            get{ return _points; }
        }
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
        /// Enemy takes damge from player and gets a status effect if it's a Sp attakc.
        /// </summary>
        /// <param name="dmg">How much damage the player does to the ememy</param>
        /// <param name="special">Tells what effect the attack did</param>
        public virtual void TakeDamge(float dmg, int special)
        {
            _hp -= dmg;
            Console.WriteLine($"You hit {_name} for {_dmg}");
            StatusCheck(special);
        }
        /// <summary>
        /// A status checker to check if the enemy is inflicted by the statues
        /// </summary>
        /// <param name="check"></param>
        public void StatusCheck(int check)
        {
            if(_rnd.Next(101) <= 45 && check == 1)
            {
                Console.WriteLine("Enemy got burned");
                _burn = true;
                _burnTimer += 4;
            }
            if(_rnd.Next(101) <= 55 && check == 2)
            {
                Console.WriteLine("Enemy got stuned");
                _enemySW.Restart();
            }
        }
        /// <summary>
        /// When the enemy should attack
        /// </summary>
        /// <param name="player">The player/object the enemy attacks</param>
        public void CanAttack(Player player)
        {   
            _enemySW.Start();
            if (_enemySW.ElapsedMilliseconds >= AttackSpeed)
            {
                if(_burn && _burnTimer > 0)
                {
                    Console.WriteLine($"{_name} got burned for 5 dmg");
                    _hp -= 5;
                    _burnTimer--;
                }
                Attack(player);
                _enemySW.Restart();
                _enemySW.Stop();
            }
            //Stopwatch in every class insted of the program class
        }
        /// <summary>
        /// Enemy Deals damage to the enemy
        /// </summary>
        /// <param name="player">Players object</param>
        public abstract void Attack(Player player);
        /// <summary>
        /// Stops the enemies time and starts it too
        /// </summary>
        /// <param name="stop">Decides if clock should start or stop</param>
        public void Wait(bool stop = true)
        {
            if(stop) _enemySW.Stop();
            if(!stop) _enemySW.Start();
        }
    }
}