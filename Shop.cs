namespace TBG
{
    public class Shop
    {
        //Array of the items names.
        private string[] _itemNames = new string[] { "15hp Potion","Random Stat+","Fire Ball", "Frost Bolt"};
        //A 2D/grid Array of the items. It holds the items values. Which in order are: quantity, Sp cost, points cost and value of effect.
        private int[,] _items = new int[,] { { 0, 0, 10, 15},{ 0, 0, 30, 0}, { 0, 5, 60, 55}, {1, 7, 70, 66}};
        //Sends and returns values for the item.
        public int[] Fireball
        {
            get
            {
                int[] row = new int[_items.GetLength(1)];
                for (int i = 0; i < _items.GetLength(1); i++)
                {
                    row[i] = _items[2, i];
                }
                return row;
            }
            set
            {
                for (int i = 0; i < _items.GetLength(1); i++)
                {
                    _items[2, i] = value[i];
                }
            }
        }
        public int[] IceBolt
        {
            get
            {
                int[] row = new int[_items.GetLength(1)];
                for (int i = 0; i < _items.GetLength(1); i++)
                {
                    row[i] = _items[3, i];
                }
                return row;
            }
            set
            {
                for (int i = 0; i < _items.GetLength(1); i++)
                {
                    _items[3, i] = value[i];
                }
            }
        }
        /// <summary>
        /// Menue for buying the items
        /// </summary>
        /// <param name="points">Points you collected</param>
        /// <param name="player">Players object</param>
        /// <returns></returns>
        public int ShopMenu(int points, Player player)
        {
            int chose = 0;
            Console.Clear();
            while (true)
            {
                while (true)
                {
                    Console.WriteLine($"You have {points} points.\nWhat would you like to buy?\nType the number of the item in to buy it or 0 if you don't want anything.");
                    int i = 0;
                    foreach (string n in _itemNames)
                    {
                        string buffer = "";
                        while (n.Length + buffer.Length < 15)
                        {
                            buffer += " ";
                        }
                        Console.WriteLine($"{i+1}.{n}{buffer}Cost: {_items[i,2]}");
                        i++;
                    }
                    try
                    {
                        string input = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.Clear();
                            throw new Exception("Input cannot be empty. Please try again.");
                        }
                        chose = int.Parse(input);
                        if(chose != 0) Buy(chose, points, player);
                        break;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                if (chose == 0)
                {
                    break;
                }
            }
            return points;
        }
        /// <summary>
        /// Executs the order/chose that the player made in the ShopMenue
        /// </summary>
        /// <param name="chose">Chose of what to buy</param>
        /// <param name="points">Money/points</param>
        /// <param name="player">Player object</param>
        /// <returns></returns>
        public int Buy(int chose, int points, Player player)
        {
            if(points < _items[chose -1, 2])
            {
                Console.Clear();
                Console.WriteLine("You don't have enough points to buy this.");
                return points;
            }
            else
            {
                if(chose == 1 && player.Hp + 5 <= 100)
                {
                    player.Heal(_items[chose-1,3]);
                }
                if(chose == 2)
                {
                    player.StatsRandomizer();
                }
                _items[chose-1, 0]++;
                return points -= _items[chose-1, 2];
            }
        }
    }
}