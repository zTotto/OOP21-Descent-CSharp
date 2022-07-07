using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LorenzoTodisco;

namespace JonathanLupini
{
    public class start
    {
        public static void Main(string[] args)
        {
            String mapPath = @"C:\Users\jonat\Desktop\Coding\C#\OOP21-Descent-CSharp\Jonathan-Lupini\testMap.txt";
            Map map = new Map(mapPath);
            MapManager.printMap(map);
        }
    }
   
    public class Map
    {
        private readonly string _mapPath;
        public int Columns { get ; }
        public int Rows { get; }
        public Dictionary<Point, char> mapStatus { get; }

        public Map(String path)
        {
            _mapPath = path;
            var (width, height) = MapManager.MapSize(_mapPath);
            Columns = height;
            Rows = width;
            mapStatus = MapManager.readMap(_mapPath);
        }

    }
    public static class MapManager
    {   
        public static Dictionary<Point, char> readMap(String path)
        {
            var stream = File.OpenRead(path);
            var reader = new StreamReader(stream, Encoding.UTF8);
            var dict = new Dictionary<Point, char>();
            var (columns, rows) = MapSize(path);
            var buffer = reader.ReadLine();
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    char next = (char)reader.Read();
                    while (next.Equals('\n') || next.Equals('\r') || next.Equals(' '))
                    {
                        next = (char)reader.Read();
                    }
                    dict.Add(new Point(x, y), next);
                }
            }
            return dict;
        }

        public static void printMap(Map map)
        {
            for (int y = 0; y < map.Rows; y++)
            {
                for (int x = 0; x < map.Columns; x++)
                {
                    Console.Write(map.mapStatus[new Point(x, y)]);
                }
                Console.WriteLine(" ");
            }
        }

        public static Point? findCharacter(Dictionary<Point, char> dict, int size, char character)
        {
            for (int y = 1; y < size - 1; y++)
            {
                for (int x = 1; x < size - 1; x++)
                {
                    var position = new Point(x, y);
                    if (dict[position] == character) return position;
                }
            }
            return null;
        }

        public static Tuple<int, int> MapSize(String path)
        {
            var stream = File.OpenRead(path);
            var reader = new StreamReader(stream, Encoding.UTF8);
            var dict = new Dictionary<Point, char>();
            var columns = Char.GetNumericValue((char)reader.Read());
            var useless = (int)reader.Read();
            var rows = Char.GetNumericValue((char)reader.Read());
            return new Tuple<int, int>((int)columns, (int)rows);
        }
    }
}
