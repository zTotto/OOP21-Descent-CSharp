using System.Drawing;
using System.Text;

namespace Jonathan_Lupini.Tasks.Supporting
{
    /// <summary>
    /// Utility class to load and analyze a map from a text file.
    /// </summary>
    public static class MapManager
    {
        /// <summary>
        /// Utility method that reads a map from a given file path.
        /// Returns a dictionary that associates a 2d point coordinate
        /// to a character representing the point's content in the map.
        /// </summary>
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

        /// <summary>
        /// Utility method that prints the map to the console.
        /// </summary>
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

        /// <summary>
        /// Utility method to read the size of the map from a file.
        /// </summary>
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
