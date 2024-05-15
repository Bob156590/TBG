using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using TBG;
bool game = true;
bool enemyAttakc = false;
Stopwatch playerSW = new Stopwatch();
Stopwatch enemySW = new Stopwatch();
List<Entity> entities = new List<Entity>();
Player player = new Player();
entities.Add(new Rat());
entities.Add(new Rat());
playerSW.Start();
enemySW.Start();
while (game)
{
    float lastAttack =0;
    lastAttack += enemySW.ElapsedMilliseconds;
    if(lastAttack > enemy.AttackSpeed){
        lastAttack = 0;
        // attack
    }
    if(playerSW.Elapsed.Seconds >= player.AttackSpeed) PlayerTurn();
    foreach(Entity entity in entities) if(enemySW.ElapsedMilliseconds % entity.AttackSpeed == 0 && enemyAttakc == false) EnemyTurn(entity);
}

void PlayerTurn(){
    if(entities.Count == 0) return;
    playerSW.Reset();
    enemySW.Stop();
    Console.WriteLine("Whats your next move:");
    Console.WriteLine("1. Attack\n2. Special\n3. Block\n4. Stats");
    try{PlayerMove(int.Parse(Console.ReadLine()));}
    catch
    {
        //Console.Clear();
        Console.WriteLine("Du måste skriva något");
        PlayerTurn();
    }
}

void PlayerMove(int chose)
{
    //Console.Clear();
    switch(chose){
        case 1:
            Console.WriteLine("Who you gonna attack?");
            int i = 0;
            foreach(Entity entity in entities)
            {
                i++;
                Console.Write($"{i}. ");
                entity.SkrivUt();
            }
            player.Attack(entities[Choose()-1]);
            break;
        case 2:
            player.Attack(entities[0], true);
            break;
        case 3:
            player.Block = true;
            break;
        case 4:
            Console.WriteLine($"Player\nHP: {player.Hp}\nSP: {player.SpecialPoints}\nDmg: {player.Dmg}\nAS: {player.AttackSpeed}");
            Console.ReadKey();
            //Console.Clear();
            PlayerTurn();
            break;
        default:
            Console.WriteLine("Pls chose only 1-4");
            PlayerTurn();
            break;
    }
    Check();
    enemySW.Start();
    playerSW.Start();
}

void EnemyTurn(Entity entity){
    enemyAttakc = true;
    enemySW.Stop();
    entity.Attack(player);
    enemySW.Start();
    Thread.Sleep(3);
    enemyAttakc = false;
}

int Choose()
{
    Console.WriteLine("Choose one of the enemies");
    int i = 10;
    while(i > entities.Count || i == null){
        i = int.Parse(Console.ReadLine());
    }
    return i;
}

void Check()
{
    for (int i = 0; i < entities.Count; i++)
    {
        if(entities[i].Hp <= 0) entities.RemoveAt(i);
    }
    if(player.Hp <= 0)
    {
        game = false;
        Console.WriteLine("You Have Died");
    }
}