using System.Diagnostics;

namespace TBG
{
    public class Player : IStats
    {
        private float _hp = 100; //Player health point
        private int _dmg = 10; //Player damage
        private bool _block; //If Player is blocking or not
        private int _attackSpeed = 4; //How fast the Player attacks
        private int _specialPoint = 10;//Player can exchange SP to use Special Move™
        
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
        public int SpecialPoints
        {
            get{ return _specialPoint; }
        }
        public bool Block
        {
            get{ return _block; }
            set{ _block = value; }
        }
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
        public virtual void TakeDamge(float dmg, string name)
        {
            _hp -= dmg;
            Console.WriteLine($"The {name} delt {dmg} to you");
        }
    }
}