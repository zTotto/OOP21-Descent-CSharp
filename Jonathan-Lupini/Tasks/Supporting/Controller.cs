using System.Drawing;

namespace Jonathan_Lupini.Tasks.Supporting
{   /// <summary>
    /// Class to visually test the simple implementation of the game.
    /// The mob M moves randomly until it sees the hero H, it then tries
    /// to get closer until it can attack and kill him.
    /// </summary>
    public class Controller
    {
        private const string MapPath =
            "testMap.txt";

        public static void Main(string[] args)
        {
            var map = new Map(MapPath);
            var level = new Level(map);
            var mob = new Mob(new Point(2, 4), 'M');
            var hero = new Character(new Point(6, 4), 'H');
            level.Characters.Add(hero);
            level.Characters.Add(mob);
            while (true)
            {

                mob.Update(level);
                Console.Clear();
                level.PrintLevel();
                Thread.Sleep(500);
                if (hero.Hp <= 0) break;
            }

            Console.Clear();
            Console.WriteLine("Hero is D E A D");
            Console.WriteLine("Game Over");
        }
    }
}
