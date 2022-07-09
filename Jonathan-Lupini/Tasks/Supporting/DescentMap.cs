using System.Drawing;
using System.Text;

namespace Jonathan_Lupini.Tasks.Supporting
{
    public class Map
    {
        private readonly string _mapPath;
        public int Columns { get; }
        public int Rows { get; }
        public Dictionary<Point, char> MapStatus { get; }

        public Map(string path)
        {
            _mapPath = path;
            var (width, height) = MapManager.MapSize(_mapPath);
            Columns = height;
            Rows = width;
            MapStatus = MapManager.readMap(_mapPath);
        }

        public bool IsWall(Point point)
        {
            if (!MapStatus.ContainsKey(point)) return false;
            if (MapStatus[point] == 'X') return true;
            return false;
        }

    }
    public static class MapManager
    {
        public static Dictionary<Point, char> readMap(string path)
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

        public static void PrintMap(Map map)
        {
            for (int y = 0; y < map.Rows; y++)
            {
                for (int x = 0; x < map.Columns; x++)
                {
                    Console.Write(map.MapStatus[new Point(x, y)]);
                }
                Console.WriteLine(" ");
            }
        }

        public static Point? FindCharacter(Dictionary<Point, char> dict, int size, char character)
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

        public static Tuple<int, int> MapSize(string path)
        {
            var stream = File.OpenRead(path);
            var reader = new StreamReader(stream, Encoding.UTF8);
            var dict = new Dictionary<Point, char>();
            var columns = char.GetNumericValue((char)reader.Read());
            var useless = reader.Read();
            var rows = char.GetNumericValue((char)reader.Read());
            return new Tuple<int, int>((int)columns, (int)rows);
        }
    }
}
