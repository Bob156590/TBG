using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using TBG;
using Newtonsoft.Json;
using System.Drawing;
using Newtonsoft.Json.Serialization;

bool game = true;//Bool for if the game is still going
List<Entity> entities = new List<Entity>();//List of enemy objects
//Objects of classes
Shop shop = new Shop();
Player player = new Player();//Player object
Points points = new Points();//point object
Random _rnd = new Random();//Random for the enemy spawner
int floor = 0;//Starts timer for player
int floorModifier = 0;//Make the game harder over time
//Game keeps going untill u lose
while (game)
{
    Check();
    if(player.playerSW.ElapsedMilliseconds >= player.AttackSpeed) PlayerTurn();
    foreach(Entity entity in entities) entity.CanAttack(player);
}
/// <summary>
/// Player chooses what to do
/// </summary>
void PlayerTurn(){
    Wait();
    player.AttackManager(2);
    player.AttackManager(0);
    Console.WriteLine("Whats your next move:");
    Console.WriteLine("1. Attack\n2. Special\n3. Rest\n4. Stats\n5. Scoreboard");
    try{PlayerMove(int.Parse(Console.ReadLine()));}
    catch
    {
        Console.Clear();
        Console.WriteLine("You must write something");
        PlayerTurn();
    }
}

/// <summary>
/// Proceeds to do what the user chose at PlayerTurn
/// </summary>
/// <param name="chose">What choose the player made</param>
void PlayerMove(int chose)
{

    Console.Clear();
    switch(chose){
        case 1:
            Console.WriteLine("Who you gonna attack?");
            
            player.Attack(entities[Choose()-1]);
            break;
        case 2:
            player.SpecialAttack(entities[Choose()-1], shop);
            PlayerTurn();
            break;
        case 3:
            player.Rest();
            break;
        case 4:
            Console.WriteLine($"Player\nHP: {player.Hp}\nSP: {player.SpecialPoints}\nDmg: {player.Dmg}\nAS: {player.AttackSpeed}");
            Console.WriteLine("Press any button to continue.");
            Console.ReadKey();
            Console.Clear();
            PlayerTurn();
            break;
        case 5:
            points.WriteOutTheScoreboard();
            Console.WriteLine("Press any button to continue.");
            Console.ReadKey();
            PlayerTurn();
            break;
        default:
            Console.WriteLine("Pls chose only 1-4");
            PlayerTurn();
            break;
    }
    Wait(false);
    player.AttackManager(1);
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
    int i = 0;
    foreach(Entity entity in entities)
    {
        i++;
        Console.Write($"{i}. ");
        entity.SkrivUt();
    }
    Console.WriteLine("Choose one of the enemies");
    int j = 10;
    while(j > entities.Count || j == null){
        j = int.Parse(Console.ReadLine());
    }
    return j;
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
    if(entities.Count == 0)
    {
        if(floor > 0)
        {
            player.AttackManager(2);
            player.AttackManager(0);
            Console.WriteLine("Would you like to go to the shop?\nWrite 1 if yes and write anything else if no.");
            string? goShop = Console.ReadLine();
            if(goShop == "1") points.Point = shop.ShopMenu(points.Point, player);
        }
        Spawner();
    }
    if(player.Hp <= 0)
    {
        game = false;
        Console.WriteLine("You Have Died");
    }
}
/// <summary>
/// Spawns random amount of diffrent enemie typs. And keeps track of the floor/round
/// </summary>
void Spawner()
{
    player.AttackManager(1);
    for (int i = 0; i < _rnd.Next(4 + floorModifier); i++)
    {
        entities.Add(new Rat());
        Console.WriteLine("U have encountered a Rat");
    }
    for (int i = 0; i < _rnd.Next(3 + floorModifier); i++)
    {
        entities.Add(new Skeleton());
        Console.WriteLine("U have encountered a Skeleton");
    }
    for (int i = 0; i < _rnd.Next(2 + floorModifier); i++)
    {
        entities.Add(new Berserk());
        Console.WriteLine("U have encountered a Berserker");
    }
    if(entities.Count > 0)
    {
        floor++;
        Console.WriteLine("Get Ready for a fight.");
    }
    if(floor % 3 == 0) floorModifier++;
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
    points.Floor = floor;
    points.SaveToScoreboard(points);
    Console.WriteLine($"Game Over\nPoints: {points.Point}");
}