using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entity.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        public float range;

        public static float ATK = 600;
        public static float critRate = 1;

        public GameObject FirePoint { get; set; }
    }
}