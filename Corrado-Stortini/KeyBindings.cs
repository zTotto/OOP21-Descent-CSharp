using System;
using System.Reflection;

namespace CorradoStortini
{
    class KeyBindingAttr : Attribute
    {
        /// <summary>
        ///    Constructor for the attribute of the KeyBindings
        /// </summary>
        /// <param name="key">Key of an Action (or KeyBinding)</param>
        /// <param name="name">Name of an Action (or KeyBinding)</param>
        internal KeyBindingAttr(Keys key, string name)
        {
            Key = key;
            Name = name;
        }

        public Keys Key { get; set; }
        public string Name { get; private set; }
    }

    public static class KeyBindings
    {
        /// <summary>
        ///   This method is called to get the key of the associated action
        /// </summary>
        /// <param name="action">Action of which is wanted to get the key</param>
        /// <returns>The key of the passed action</returns>
        public static Keys GetKey(this KeyBinding action) => GetAttr(action).Key;

        /// <summary>
        ///   This method is called to change the key of the associated action
        /// </summary>
        /// <param name="action">Action of which is wanted to change the key</param>
        /// <param name="newKey">New key that will be associated to the action</param>
        public static void SetKey(this KeyBinding action, Keys newKey) => GetAttr(action).Key = newKey;


        /// <summary>
        ///   This method is called to get the name of the associated action
        /// </summary>
        /// <param name="action">Action of which is wanted to know the name</param>
        /// <returns>The name of the passed action</returns>
        public static string GetName(this KeyBinding action) => GetAttr(action).Name;

        private static KeyBindingAttr GetAttr(KeyBinding p) => (KeyBindingAttr)Attribute.GetCustomAttribute(ForValue(p), typeof(KeyBindingAttr));

        private static MemberInfo ForValue(KeyBinding p) => typeof(KeyBinding).GetField(Enum.GetName(typeof(KeyBinding), p));
    }

    public enum KeyBinding
    {
        /// <summary>
        /// Key to pause the game.
        /// </summary>
        [KeyBindingAttr(Keys.ESCAPE, "Pause")] PAUSE,

        /// <summary>
        /// Key to attack.
        /// </summary>
        [KeyBindingAttr(Keys.SPACE, "Attack")] ATTACK,

        /// <summary>
        /// Key to move up.
        /// </summary>
        [KeyBindingAttr(Keys.UP, "MoveUp")] UP,

        /// <summary>
        /// Key to move down.
        /// </summary>
        [KeyBindingAttr(Keys.DOWN, "MoveDown")] DOWN,

        /// <summary>
        /// Key to move right.
        /// </summary>
        [KeyBindingAttr(Keys.RIGHT, "MoveRight")] RIGHT,

        /// <summary>
        /// Key to move left.
        /// </summary>
        [KeyBindingAttr(Keys.LEFT, "MoveLeft")] LEFT,

        /// <summary>
        /// Key to pick up items.
        /// </summary>
        [KeyBindingAttr(Keys.E, "PickUp")] PICK_UP,

        /// <summary>
        /// Key to switch weapon.
        /// </summary>
        [KeyBindingAttr(Keys.W, "SwitchWeapon")] SWITCH_WEAPON,

        /// <summary>
        /// Key to use a health potion.
        /// </summary>
        [KeyBindingAttr(Keys.F, "UseHealthPotion")] USE_HEALTH_POTION,

        /// <summary>
        /// Key to use a mana potion.
        /// </summary>
        [KeyBindingAttr(Keys.M, "UseManaPotion")] USE_MANA_POTION,

        /// <summary>
        /// Key to use skill: Increases Speed.
        /// </summary>
        [KeyBindingAttr(Keys.SHIFT_LEFT, "HoldIncreasesSpeed")] INCREASES_SPEED,

        /// <summary>
        /// Key to use skill: Heal.
        /// </summary>
        [KeyBindingAttr(Keys.A, "Heal")] HEAL,

        /// <summary>
        /// Key to open the skill menu.
        /// </summary>
        [KeyBindingAttr(Keys.T, "SkillMenu")] SKILL_MENU,

        /// <summary>
        /// Key to use the door key and go to the next level.
        /// </summary>
        [KeyBindingAttr(Keys.K, "UseKey")] USE_KEY
    }
}
