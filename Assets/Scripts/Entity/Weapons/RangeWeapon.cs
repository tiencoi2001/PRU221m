using Assets.Scripts.Entity.Weapons;
using UnityEngine;

public class RangeWeapon : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(player.transform.position, transform.position) > range)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Enemy"))
        {
            if (Random.Range(1, 101) < 100 - critRate)
            {
                collision.gameObject.GetComponent<HealthSystem>().GotHitFor(ATK);
            }
            else
            {
                collision.gameObject.GetComponent<HealthSystem>().GotHitFor(ATK * 1.5f);
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
