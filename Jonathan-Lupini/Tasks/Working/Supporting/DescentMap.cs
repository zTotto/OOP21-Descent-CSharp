using System.Drawing;

namespace Jonathan_Lupini.Tasks.Supporting
{   /// <summary>
    /// Class that models a basic rpg map.
    /// </summary>
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

        /// <summary>
        /// Returns true if a given point on the map is a wall, false otherwise.
        /// </summary>
        public bool IsWall(Point point)
        {
            if (!MapStatus.ContainsKey(point)) return false;
            if (MapStatus[point] == 'X') return true;
            return false;
        }

    }
}
