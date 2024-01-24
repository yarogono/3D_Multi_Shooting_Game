namespace Server.Data
{
    #region WeaponItem
    [Serializable]
    public class WeaponItem
    {
        public int id;
        public string name;
        public int damage;
        public int attackRange;
        public int posX;
        public int posY;
        public int posZ;
    }

    [Serializable]
    public class ItemData : ILoader<int, WeaponItem>
    {
        public List<WeaponItem> WeaponItems = new List<WeaponItem>();

        public Dictionary<int, WeaponItem> MakeDict()
        {
            Dictionary<int, WeaponItem> dict = new Dictionary<int, WeaponItem>();
            foreach (WeaponItem item in WeaponItems)
                dict.Add(item.id, item);
            return dict;
        }
    }
    #endregion
}
