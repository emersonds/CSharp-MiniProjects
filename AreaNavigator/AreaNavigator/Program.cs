using System.Runtime.CompilerServices;

namespace AreaNavigator
{
    enum Directions { NORTH, EAST, SOUTH, WEST }

    class Program
    {
        private static void Main(string[] args)
        {
            Dictionary<string, Area> map = CreateMap();
            AssignNeighbors(map);

            Area currArea = map["Entrance"];

            Console.WriteLine("Welcome to the AreaNavigator!");

            GameLoop(map, currArea);
        }

        /// <summary>
        /// Creates Areas with names.
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, Area> CreateMap() =>
            new()
            {
                { "Entrance", new("Entrance") },
                { "Middle0", new("Middle0") },
                { "Left0", new("Left0") },
                { "Right0", new("Right0") },
                { "Middle1", new("Middle1") },
            };

        /// <summary>
        /// Adds directional neighbors to areas.
        /// </summary>
        /// <param name="map"></param>
        private static void AssignNeighbors(Dictionary<string, Area> map)
        {
            map["Entrance"].SetNeighbors(map["Middle0"], null, null, null);
            map["Middle0"].SetNeighbors(map["Middle1"], map["Right0"], map["Entrance"], map["Left0"]);
            map["Left0"].SetNeighbors(null, map["Middle0"], null, null);
            map["Right0"].SetNeighbors(null, null, null, map["Middle0"]);
            map["Middle1"].SetNeighbors(null, null, map["Middle0"], null);
        }


        private static void DisplayPassableAreas(Area area)
        {
            if (area == null)
            {
                Console.WriteLine("Invalid Area.\n");
                return;
            }

            if (area.NorthArea != null)
                Console.WriteLine("North Area: " + area.NorthArea.Name);
            if (area.EastArea != null)
                Console.WriteLine("East Area: " + area.EastArea.Name);
            if (area.SouthArea != null)
                Console.WriteLine("South Area: " + area.SouthArea.Name);
            if (area.WestArea != null)
                Console.WriteLine("West Area: " + area.WestArea.Name);

            Console.WriteLine();
        }


        private static void GameLoop(Dictionary<string, Area> map, Area currArea)
        {
            bool navigating = true;

            while (navigating)
            {
                Console.WriteLine($"Your current location is {currArea.Name} and this is where you can travel:\n");

                DisplayPassableAreas(currArea);

                string input = GetDirectionInput();

                if (input == "QUIT")
                {
                    Console.WriteLine("Thanks for navigating! Goodbye.");
                    navigating = false;
                    break;
                }

                Enum.TryParse<Directions>(input, out Directions dir);

                if (currArea.GetNeighbor(dir) == null)
                {
                    Console.WriteLine("This path is impassable. Please try again.\n");
                    continue;
                }
                else
                {
                    currArea = currArea.GetNeighbor(dir);
                }
            }
        }


        private static string GetDirectionInput()
        {
            Console.WriteLine("Please choose a direction to travel: NORTH EAST SOUTH WEST or QUIT\n");
            string input = Console.ReadLine().ToUpper();
            Console.WriteLine();

            if (Enum.IsDefined(typeof(Directions), input) || input == "QUIT")
            {
                return input;
            }
            else
            {
                Console.WriteLine("Invalid Direction.\n");
            }

            return GetDirectionInput();
        }

        /// <summary>
        /// Displays every area in the map as well as its neighbors.
        /// </summary>
        /// <param name="map"></param>
        private static void DisplayAllAreasAndNeighbors(Dictionary<string, Area> map)
        {
            foreach (var area in map)
            {
                Console.WriteLine(
                    $"Area: {area.Key}\n" +
                    $"North Area: {(area.Value.NorthArea != null ? area.Value.NorthArea.Name : "No North Area")}\n" +
                    $"East Area: {(area.Value.EastArea != null ? area.Value.EastArea.Name : "No East Area")}\n" +
                    $"South Area: {(area.Value.SouthArea != null ? area.Value.SouthArea.Name : "No South Area")}\n" +
                    $"West Area: {(area.Value.WestArea != null ? area.Value.WestArea.Name : "No West Area")}\n");
            }
        }
    }
}