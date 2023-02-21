using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coin;
    public int countUpgradeHP;
    public int countUpgradeRegen;
    public int countUpgradeWeapon;
    public int countUpgradeAtk;
    public int countUpgradeCrit;
    public int countUpgradeSpeed;

    public float HP;
    public float regen;
    public float ATK;
    public float critRate;
    public float cooldownTime;

    public int nextWave;
    public GameData()
    {
        this.coin = -1;
        this.countUpgradeHP = 1;
        this.countUpgradeRegen = 1;
        this.countUpgradeWeapon = 1;
        this.countUpgradeAtk = 1;
        this.countUpgradeCrit = 1;
        this.countUpgradeSpeed = 1;
        this.HP = 1000;
        this.regen = 30;
        this.ATK = 100;
        this.critRate = 1;
        this.cooldownTime = 2;
        this.nextWave = 0;
    }

    override
    public string ToString()
    {
        return this.coin + " " +
        this.countUpgradeHP + " " +
        this.countUpgradeRegen + " " +
        this.countUpgradeWeapon + " " +
        this.countUpgradeAtk + " " +
        this.countUpgradeCrit + " " +
        this.countUpgradeSpeed + " " +
        this.HP + " " +
        this.regen + " " +
        this.ATK + " " +
        this.critRate + " " +
        this.cooldownTime + " " +
        this.nextWave;
    }
}
    