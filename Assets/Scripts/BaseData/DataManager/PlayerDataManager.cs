using UnityEngine;
namespace BaseData
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "DataManager/PlayerDataManager", order = 1)]
    public class PlayerDataManager : ScriptableObject
    {
        public string className;
        public string classDescription;

        public float HP;
        public float percentRegenHP = 0;
        public float armor;

        public float percentDamage = 0;
        public float meleeDamage;
        public float rangeDamage;
        public float elementDamage;
        public float percentLifeSteel = 0;
        public float range = 0;
        public float percentAttackSpeed = 0;

        public float percentSpeed = 0;

        public float percentExpGains = 0;
        public float percentCoinGains = 0;
    }
}
