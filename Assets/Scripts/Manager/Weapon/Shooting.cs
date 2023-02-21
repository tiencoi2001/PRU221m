using Assets.Scripts.Entity.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static float cooldownTime = 2;
    protected float waitTime;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //protected void Update()
    //{

    //}

    /// <summary>
    /// Rotate player to the target direction
    /// </summary>
    /// <param name="target"></param>
    /// <param name="weapon"></param>
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
    protected GameObject shoot(GameObject weapon, GameObject firePoint)
    {
        GameObject bullet = Instantiate<GameObject>(weapon, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.up * 20f, ForceMode2D.Impulse);
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
        if (waitTime >= cooldownTime)
        {
            return true;
        }
        waitTime += deltaTime;
        return false;
    }
}
