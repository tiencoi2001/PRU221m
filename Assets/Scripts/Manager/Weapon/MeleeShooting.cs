using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeShooting : Shooting
{

    public int countMelee = 1;
    public int maxCountMelee = 1;

    [SerializeField]
    GameObject meleeWeapon;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target;
        target = getTarget("DangerEnemy");
        if (target != null && countMelee > 0 && CoolDownAttack(Time.deltaTime))
        {
            rotate(target);
            GameObject shooter = shoot(meleeWeapon, gameObject);
            shooter.gameObject.GetComponent<MeleeWeapon>().FirePoint = gameObject;
            countMelee--;
            waitTime = 0;
        }

    }

    //bool CoolDownAttack(float deltaTime)
    //{
    //    if (waitTime >= cooldownTime)
    //    {
    //        return true;
    //    }
    //    waitTime += deltaTime;
    //    return false;
    //}
}
