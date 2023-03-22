using UnityEngine;

public class RangeShooting : Shooting
{

    [SerializeField]
    GameObject rangeWeapon;
    private float range;
    ObjectPooler objectPooler;
    // Start is called before the first frame update
    void Start()
    {
        range = rangeWeapon.GetComponent<RangeWeapon>().range;
        waitTime = cooldownTime;
        objectPooler = ObjectPooler.Instance;
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
            shoot(gameObject, objectPooler);
            waitTime = 0;
        }

    }
}
