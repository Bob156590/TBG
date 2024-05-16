using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using TBG;
using Newtonsoft.Json;
using System.Drawing;
using Newtonsoft.Json.Serialization;

bool game = true;
Stopwatch playerSW = new Stopwatch();
List<Entity> entities = new List<Entity>();
Player player = new Player();
entities.Add(new Rat());
playerSW.Start(); 
Points points = new Points();
//string jsonstring = JsonConvert
while (game)
{
    if(playerSW.Elapsed.Seconds >= player.AttackSpeed) PlayerTurn();
    foreach(Entity entity in entities) entity.CanAttack(player);
}

void PlayerTurn(){
    Wait();
    if(entities.Count == 0) game = false;
    playerSW.Reset();
    //enemySW.Stop();
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
            break;
        default:
            Console.WriteLine("Pls chose only 1-4");
            PlayerTurn();
            break;
    }
    Wait(false);
    Check();
    playerSW.Start();
}

void Wait(bool stop = true){
    if (stop)
    {
        foreach(Entity entity in entities) entity.Wait();
    }
    else foreach(Entity entity in entities) entity.Wait(false);
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
        if(entities[i].Hp <= 0)
        {
            entities.RemoveAt(i);
            points.Point += 10;
        }

    }
    if(entities.Count == 0) game = false;
    if(player.Hp <= 0)
    {
        game = false;
        Console.WriteLine("You Have Died");
    }
}

if(!game)
{
    string name = null;
    while (name == null) name = Console.ReadLine();
    points.Name = name; 
    points.SaveToScoreboard(points);
    Console.WriteLine($"Game Over\nPoints: {points.Point}");
}