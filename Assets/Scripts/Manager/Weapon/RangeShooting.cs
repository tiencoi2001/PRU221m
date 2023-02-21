using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeShooting : Shooting
{

    [SerializeField]
    GameObject rangeWeapon;
    private float range;
    // Start is called before the first frame update
    void Start()
    {
        range = rangeWeapon.GetComponent<RangeWeapon>().range;
        //Debug.Log(range);
        waitTime = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject target;
        target = getTarget("Enemy");
        bool cdFin = CoolDownAttack(Time.deltaTime);
        if (target != null && cdFin && Vector3.Distance(player.transform.position, target.transform.position) <= range)
        {
            rotate(target);
            shoot(rangeWeapon, gameObject);
            waitTime = 0;
        }

    }
}
