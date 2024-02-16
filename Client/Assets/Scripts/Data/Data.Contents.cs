using System;
using System.Collections.Generic;


namespace Data
{
    #region Weapon
    [Serializable]
    public class Weapon
    {
        public int id;
        public string name;
        public float rate;
        public int damage;
        public float attackRange;
    }

    [Serializable]
    public class WeaponData : ILoader<int, Weapon>
    {
        public List<Weapon> weapons = new List<Weapon>();

        public Dictionary<int, Weapon> MakeDict()
        {
            Dictionary<int, Weapon> dict = new Dictionary<int, Weapon>();
            foreach (Weapon weapon in weapons)
                dict.Add(weapon.id, weapon);
            return dict;
        }
    }
    #endregion
}