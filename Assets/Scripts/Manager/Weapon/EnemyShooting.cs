using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    public float cooldownTime = 1;
    [SerializeField]
    public float bulletSpeed = 5f;
    protected float waitTime;
    [SerializeField]
    GameObject enemyWeapon;
    private float range;
    ObjectPooler objectPooler;

    void Start()
    {
        range = enemyWeapon.GetComponent<EnemyWeapon>().range;
        waitTime = cooldownTime;
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {

        GameObject target;
        target = getTarget("Player");
        bool cdFin = CoolDownAttack(Time.deltaTime);
        if (target != null && cdFin && Vector3.Distance(gameObject.transform.position, target.transform.position) <= range)
        {
            rotate(target);
            shoot(gameObject, objectPooler);
            waitTime = 0;
        }

    }
    protected void rotate(GameObject target)
    {
        if (target != null)
        {
            float angle = Vector3.Angle(target.transform.position - gameObject.transform.position, Vector3.up);
            if (target.transform.position.x > gameObject.transform.position.x)
            {
                angle = -angle;
            }
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weapon"></param>
    protected GameObject shoot(GameObject firePoint, ObjectPooler objectPooler)
    {
        GameObject bullet = objectPooler.SpawnFromPool("ebullet", firePoint.transform.position, firePoint.transform.rotation);

        if (bullet != null)
        {
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
        return bullet;
    }

    /// <summary>
    /// Find nearest enemy to shoot
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    protected GameObject getTarget(string tag)
    {

        GameObject[] enemyList = GameObject.FindGameObjectsWithTag(tag);
        GameObject target = null;
        if (enemyList.Length > 0)
        {
            float minDistance = Vector3.Distance(gameObject.transform.position, enemyList[0].transform.position);
            target = enemyList[0];
            foreach (GameObject enemy in enemyList)
            {
                if (enemy.activeSelf)
                    if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) < minDistance)
                    {
                        minDistance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
                        target = enemy;
                    }
            }

        }
        return target;
    }

    protected bool CoolDownAttack(float deltaTime)
    {
        Debug.Log(waitTime + "-" + cooldownTime);
        if (waitTime >= cooldownTime)
        {
            return true;
        }
        waitTime += deltaTime;
        return false;
    }
}
