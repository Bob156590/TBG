using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using TBG;
using Newtonsoft.Json;
using System.Drawing;
using Newtonsoft.Json.Serialization;

bool game = true;//bool for if the game is still going
List<Entity> entities = new List<Entity>();
Player player = new Player();
entities.Add(new Rat());
Points points = new Points();
Random rnd = new Random();//Random for the enemy spawner
while (game)
{
    if(player.playerSW.Elapsed.Seconds >= player.AttackSpeed) PlayerTurn();
    foreach(Entity entity in entities) entity.CanAttack(player);
}
/// <summary>
/// Player chooses what to do
/// </summary>
void PlayerTurn(){
    Wait();
    if(entities.Count == 0) game = false;
    
    player.ClockManagment(2);
    Console.WriteLine("Whats your next move:");
    Console.WriteLine("1. Attack\n2. Special\n3. Block\n4. Stats\n5. test");
    try{PlayerMove(int.Parse(Console.ReadLine()));}
    catch
    {
        //Console.Clear();
        Console.WriteLine("Du måste skriva något");
        PlayerTurn();
    }
}

/// <summary>
/// Does what the player chose
/// </summary>
/// <param name="chose">What choose the player made</param>
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
        case 5:
            points.WriteOutTheScoreboard();
            Console.ReadKey();
            PlayerTurn();
            break;
        default:
            Console.WriteLine("Pls chose only 1-4");
            PlayerTurn();
            break;
    }
    Wait(false);
    Check();
    player.ClockManagment(1);
}

/// <summary>
/// Stops enemey attack timer when it's players turn.
/// </summary>
/// <param name="Stop">determines if enemy waits</param>
void Wait(bool stop = true){
    if (stop)
    {
        foreach(Entity entity in entities) entity.Wait();
    }
    else foreach(Entity entity in entities) entity.Wait(false);
}

/// <summary>
/// Choose which enemy to attack.
/// </summary>
int Choose()
{
    Console.WriteLine("Choose one of the enemies");
    int i = 10;
    while(i > entities.Count || i == null){
        i = int.Parse(Console.ReadLine());
    }
    return i;
}
/// <summary>
/// Checks if the player or the enemies are dead.
/// Spawns new enemies if all are dead. 
/// </summary>
void Check()
{
    for (int i = 0; i < entities.Count; i++)
    {
        if(entities[i].Hp <= 0)
        {
            points.Point += entities[i].Points;
            entities.RemoveAt(i);
        }

    }
    if(entities.Count == 0) Spawner();
    if(player.Hp <= 0)
    {
        game = false;
        Console.WriteLine("You Have Died");
    }
}
/// <summary>
/// Spawns random amount of diffrent enemie typs.
/// </summary>
void Spawner()
{
    for (int i = 0; i < rnd.Next(1, 2); i++)
    {
        entities.Add(new Rat());
        Console.WriteLine("U have encountered a Rat");
    }
    for (int i = 0; i < rnd.Next(0, 1); i++)
    {
        entities.Add(new Skeleton());
        Console.WriteLine("U have encountered a Skeleton");
    }
    Console.WriteLine("Get Ready for a fight.");
}


/// <summary>
/// Saves name and score when game ends.
/// </summary>
if(!game)
{
    string? name = null;
    Console.WriteLine("Pls tell us the name of the fallen hero so it may be saved in the hall of heros.");
    while (name == null) name = Console.ReadLine();
    points.Name = name; 
    points.SaveToScoreboard(points);
    Console.WriteLine($"Game Over\nPoints: {points.Point}");
}