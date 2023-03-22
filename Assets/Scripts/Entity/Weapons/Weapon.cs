using UnityEngine;

namespace Assets.Scripts.Entity.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        public float range;

        public static float ATK = 100;
        public static float critRate = 1;

        public GameObject FirePoint { get; set; }
    }
}