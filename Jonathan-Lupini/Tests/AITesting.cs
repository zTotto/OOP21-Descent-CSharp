using System.Drawing;
using Jonathan_Lupini.Tasks;
using Jonathan_Lupini.Tasks.Supporting;
using NUnit.Framework;

namespace Jonathan_Lupini.Tests
{
    [TestFixture]
    public class AiTesting
    {
        private const string MapPath =
            "testMap.txt";
        private Map _map;
        private Level _level;
        private Mob _mob;
        private Character _hero;

        [SetUp]
        public void Setup()
        {
            _map = new Map(MapPath);
            _level = new Level(_map);
            _mob = new Mob(new Point(2, 4), 'M');
            _hero = new Character(new Point(6, 4), 'H');
            _level.Characters.Add(_hero);
            _level.Characters.Add(_mob);
        }

        [Test]
        public void LineOfSightTest()
        {
            Assert.IsFalse(LineOfSight.IsTargetSeen(_level,_mob, _hero));
            _mob.Position = _mob.DirToPos(Direction.Down);
            Assert.IsFalse(LineOfSight.IsTargetSeen(_level, _mob, _hero));
            _mob.Position = _mob.DirToPos(Direction.Down);
            Assert.IsFalse(LineOfSight.IsTargetSeen(_level, _mob, _hero));
            _mob.Position = _mob.DirToPos(Direction.Down);
            Assert.IsTrue(LineOfSight.IsTargetSeen(_level, _mob, _hero));
            _mob.Position = _mob.DirToPos(Direction.Right);
            Assert.IsTrue(LineOfSight.IsTargetSeen(_level, _mob, _hero));
            _mob.Position = _mob.DirToPos(Direction.Left);
            Assert.IsTrue(LineOfSight.IsTargetSeen(_level, _mob, _hero));
            _mob.Position = _mob.DirToPos(Direction.Left);
            Assert.IsFalse(LineOfSight.IsTargetSeen(_level, _mob, _hero));
        }

        [Test]
        public void WallCollisionTest()
        {
            Assert.IsTrue(_level.ValidMovement(_mob, Direction.Right));
            _mob.Position = _mob.DirToPos(Direction.Right);
            Assert.IsFalse(_level.ValidMovement(_mob, Direction.Right));
            Assert.IsTrue(_level.ValidMovement(_mob, Direction.Up));
            _mob.Position = _mob.DirToPos(Direction.Up);
            Assert.IsTrue(_level.ValidMovement(_mob, Direction.Up));
            _mob.Position = _mob.DirToPos(Direction.Up);
            Assert.IsTrue(_level.ValidMovement(_mob, Direction.Up));
            _mob.Position = _mob.DirToPos(Direction.Up);
            Assert.IsFalse(_level.ValidMovement(_mob, Direction.Up));
            Assert.IsTrue(_level.ValidMovement(_mob, Direction.Left));
            _mob.Position = _mob.DirToPos(Direction.Left);
            Assert.IsTrue(_level.ValidMovement(_mob, Direction.Left));
            _mob.Position = _mob.DirToPos(Direction.Left);
            Assert.IsFalse(_level.ValidMovement(_mob, Direction.Left));
        }

        [Test]
        public void CharacterCollisionTest()
        {
            _mob.Position = new Point(_hero.Position.X - 1, _hero.Position.Y);
            Assert.IsFalse(_level.ValidMovement(_mob, Direction.Right));
            _mob.Position = new Point(_hero.Position.X + 1, _hero.Position.Y);
            Assert.IsFalse(_level.ValidMovement(_mob, Direction.Left));
            _mob.Position = new Point(_hero.Position.X, _hero.Position.Y-1);
            Assert.IsFalse(_level.ValidMovement(_mob, Direction.Down));
            _mob.Position = new Point(_hero.Position.X, _hero.Position.Y+1);
            Assert.IsFalse(_level.ValidMovement(_mob, Direction.Up));
        }

        [Test]
        public void PathfindingTest()
        {
            _mob.Position = new Point(5, 1);
            var previousPos = _mob.Position;
            Assert.IsTrue(LineOfSight.IsTargetSeen(_level, _mob, _hero));
            _mob.Update(_level);
            Assert.IsTrue(IsCloser(previousPos, _mob.Position, _hero.Position));
            previousPos = _mob.Position;
            _mob.Update(_level);
            Assert.IsTrue(IsCloser(previousPos, _mob.Position, _hero.Position));
            previousPos = _mob.Position;
            _mob.Update(_level);
            Assert.IsTrue(IsCloser(previousPos, _mob.Position, _hero.Position));
            previousPos = _mob.Position;
            _mob.Update(_level);
            Assert.IsFalse(IsCloser(previousPos, _mob.Position, _hero.Position)); //mob is adjacent to hero, can't get any closer

            _mob.Position = new Point(5, 7);
            previousPos = _mob.Position;
            Assert.IsTrue(LineOfSight.IsTargetSeen(_level, _mob, _hero));
            _mob.Update(_level);
            Assert.IsTrue(IsCloser(previousPos, _mob.Position, _hero.Position));
            previousPos = _mob.Position;
            _mob.Update(_level);
            Assert.IsTrue(IsCloser(previousPos, _mob.Position, _hero.Position));
            previousPos = _mob.Position;
            _mob.Update(_level);
            Assert.IsTrue(IsCloser(previousPos, _mob.Position, _hero.Position));
            previousPos = _mob.Position;
            _mob.Update(_level);
            Assert.IsFalse(IsCloser(previousPos, _mob.Position, _hero.Position)); //mob is adjacent to hero, can't get any closer
        }

        private bool IsCloser(Point previousPos, Point currentPosition, Point targetPosition)
        {
            var oldDistanceX = Math.Abs(targetPosition.X - previousPos.X);
            var oldDistanceY = Math.Abs(targetPosition.Y - previousPos.Y);
            var newDistanceX = Math.Abs(targetPosition.X - currentPosition.X);
            var newDistanceY = Math.Abs(targetPosition.Y - currentPosition.Y);
            return (oldDistanceX > newDistanceX && oldDistanceY > newDistanceY ||
                    oldDistanceX > newDistanceX && oldDistanceY >= newDistanceY ||
                    oldDistanceX >= newDistanceX && oldDistanceY > newDistanceY);
        }

        [Test]
        public void AggroTest()
        {
            _mob.Position = new Point(_hero.Position.X - 1, _hero.Position.Y);
            Assert.IsTrue(_hero.Hp == 15);
            _mob.Update(_level);
            Assert.IsTrue(_hero.Hp == 10);
            _mob.Update(_level);
            _mob.Update(_level);
            Assert.IsTrue(_hero.Hp == 0);
        }
    }
}
