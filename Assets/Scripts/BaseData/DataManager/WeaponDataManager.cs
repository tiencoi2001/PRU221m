using UnityEngine;

namespace BaseData
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "DataManager/WeaponDataManager", order = 1)]
    public class WeaponDataManager : ScriptableObject
    {
        public string weaponName;
        public string weaponDescription;

        public float meleeDamage;
        public float rangeDamage;
        public float elementDamage;
        public float percentLifeSteel = 0;
        public float range = 0;
        public float attackSpeed = 0;
    }
}
