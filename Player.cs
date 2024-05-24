using System.Diagnostics;

namespace TBG
{
    public class Player : IStats
    {
        private Random rnd = new Random();
        public Stopwatch playerSW = new Stopwatch();
        private float _hpMax = 100;
        private float _hp = 100; //Player health point
        private int _dmg = 15; //Player damage
        private int _attackSpeed = 4000; //How fast the Player attacks
        private int _specialPointMax = 500;
        private int _specialPoint = 500;//Player can exchange SP to use Special Move™
        
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
        /// <summary>
        /// Player attacks an enemy
        /// </summary>
        /// <param name="entity">The enemies object</param>
        /// <param name="sp">If it's a reguler att attack or a special attack</param>
        public void Attack(Entity entity)
        {
            entity.Hp -= _dmg;
        }
        /// <summary>
        /// Method for using SpecialAttacks that are bought from the shop.
        /// </summary>
        /// <param name="entity">object of enemy to use the item/SP on.</param>
        /// <param name="items">Object to get accest to the usable items.</param>
        public void SpecialAttack(Entity entity, Shop items)
        {
            string use = "";
            Console.WriteLine("What would you like to use?");
            Console.WriteLine($"1.Fire ball {items.Fireball[0]}x    Cost:{items.Fireball[1]}");
            Console.WriteLine($"2.Ice bolt {items.IceBolt[0]}x    Cost:{items.IceBolt[1]}");
            bool specialMove = true;
            while(specialMove)
            {
                try{use = Console.ReadLine();}
                catch
                {
                    Console.Clear();
                    Console.WriteLine("You must write something");
                    SpecialAttack(entity, items);
                }
                switch(use)
                {
                    case "1":
                        if(items.Fireball[0] > 0 && _specialPoint > items.Fireball[1])
                        {
                            entity.TakeDamge(items.Fireball[3], 1);
                            _specialPoint -= items.Fireball[1];
                        }
                        else Console.WriteLine("You don't have resurse for this.");
                        specialMove = false;
                        break;
                    case "2":
                        if(items.IceBolt[0] > 0 && _specialPoint > items.IceBolt[1])
                        {
                            entity.TakeDamge(items.IceBolt[3], 2);
                            _specialPoint -= items.IceBolt[1];
                        }
                        else Console.WriteLine("You don't have resurse for this.");
                        specialMove = false;
                        break;
                    default:
                        Console.WriteLine("You have to write between 1 and 2");
                        break;
                }
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
            if(!bludgeon && dmg > 0)
            {
                _hp -= dmg;
                Console.WriteLine($"The {name} cuts you for {dmg}");
            }
            else if(bludgeon && dmg > 0)
            {
                _hp -= dmg;
                if(rnd.Next(10) == 0)
                {
                    playerSW.Restart();
                    Console.WriteLine($"You were hit on the head a little to hard.{dmg}");
                }
                else Console.WriteLine($"The {name} hits you for {dmg}");
            }
            else Console.WriteLine($"{name} missed you");
        }
        /// <summary>
        /// Heals the player
        /// </summary>
        /// <param name="health">How much it should heal</param>
        public void Heal(int health)
        {
            if(health + _hp < _hpMax) _hp =+health;
            else _hp = 100;
            Console.WriteLine(Hp);
        }
        /// <summary>
        /// Gives the player a random uppgrade to one of their stats
        /// </summary>
        public void  StatsRandomizer()
        {
            int stat = rnd.Next(1, 5);
            if(stat == 1)
            {
                _hpMax += 12;
                Heal(20);
                Console.WriteLine("Your max health increased by 12 and you were heald for 20.");
            }
            if(stat == 2)
            {
                _dmg += 5;
                Console.WriteLine("Your dmg increased by 5.");
            }
            if(stat == 3)
            {
                _attackSpeed -= 100;
                Console.WriteLine("Your attackspeed decreased by 100 ms.");
            }
            if(stat == 4)
            {
                _specialPointMax += 10;
                _specialPoint += 8;
                Console.WriteLine("Your Max Sp increased by 10 and regend 8");
            }
        }
        /// <summary>
        /// A method for recharging Sp
        /// </summary>
        public void Rest()
        {
            if(_specialPoint + 5 <= _specialPointMax)
            {
                _specialPoint += 5;
                Console.WriteLine($"Sp recharged to {_specialPoint}");
            }
            else
            {
                _specialPoint = _specialPointMax;
                Console.WriteLine($"Max Sp reached: {_specialPointMax}");
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