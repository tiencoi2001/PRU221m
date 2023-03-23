using Assets.Scripts.Entity.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    [SerializeField]
    public int damage = 30;
    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            if (Random.Range(1, 101) < 100 - critRate)
            {
                collision.gameObject.GetComponent<HealthSystem>().GotHitFor(damage);   
            }
            else
            {
                collision.gameObject.GetComponent<HealthSystem>().GotHitFor(damage * 1.5f);
            }
            gameObject.SetActive(false);
        }
        else
        {
            return;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Map Boundary")
        {
            gameObject.SetActive(false);
        }
    }
}
