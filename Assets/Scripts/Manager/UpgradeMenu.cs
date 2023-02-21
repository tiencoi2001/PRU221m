using Assets.Scripts.Entity.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour, IDataPersistance
{
    public static bool isPause = false;

    public GameObject[] weapons;

    public GameObject upgradeMenuUI;

    [SerializeField] float maxHP;
    [SerializeField] float maxRegen;
    [SerializeField] int maxWeapon;
    [SerializeField] float maxAtk;
    [SerializeField] float maxCrit;
    [SerializeField] float maxSpeed;

    [SerializeField] float baseCostHP;
    [SerializeField] float baseCostRegen;
    [SerializeField] float baseCostWeapon;
    [SerializeField] float baseCostAtk;
    [SerializeField] float baseCostCrit;
    [SerializeField] float baseCostSpeed;

    private int countUpgradeHP = 1;
    private int countUpgradeRegen = 1;
    private int countUpgradeWeapon = 1;
    private int countUpgradeAtk = 1;
    private int countUpgradeCrit = 1;
    private int countUpgradeSpeed = 1;

    private Image imageHP;
    private Image imageRegen;
    private Image imageAtk;
    private Image imageCrit;
    private Image imageSpeed;
    private Image imageWeapon;

    private bool firstTime = true;

    private int dataCoin = -1;

    public void LoadData(GameData gameData)
    {
        this.countUpgradeHP = gameData.countUpgradeHP;
        this.countUpgradeRegen = gameData.countUpgradeRegen;
        this.countUpgradeWeapon = gameData.countUpgradeWeapon;
        this.countUpgradeAtk = gameData.countUpgradeAtk;
        this.countUpgradeCrit = gameData.countUpgradeCrit;
        this.countUpgradeSpeed = gameData.countUpgradeSpeed;
        Weapon.ATK = gameData.ATK;
        Weapon.critRate = gameData.critRate;
        Shooting.cooldownTime = gameData.cooldownTime;
        dataCoin = gameData.coin;
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Player>().HP = gameData.HP;
        player.GetComponent<Player>().regen = gameData.regen;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.countUpgradeHP = this.countUpgradeHP;
        gameData.countUpgradeRegen = this.countUpgradeRegen;
        gameData.countUpgradeWeapon = this.countUpgradeWeapon;
        gameData.countUpgradeAtk = this.countUpgradeAtk;
        gameData.countUpgradeCrit = this.countUpgradeCrit;
        gameData.countUpgradeSpeed = this.countUpgradeSpeed;
        gameData.ATK = Weapon.ATK;
        gameData.critRate = Weapon.critRate;
        gameData.cooldownTime = Shooting.cooldownTime;
        gameData.coin = Convert.ToInt32(GameObject.Find("CoinCounter").GetComponent<Text>().text);
        GameObject player = GameObject.FindWithTag("Player");
        gameData.HP = player.GetComponent<Player>().HP;
        gameData.regen = player.GetComponent<Player>().regen;
    }

    private void Start()
    {
        upgradeMenuUI.SetActive(true);
        upgradeMenuUI.transform.localScale = Vector3.zero;
        imageHP = GameObject.Find("HealthFillImage").GetComponent<Image>();
        imageRegen = GameObject.Find("RegenFillImage").GetComponent<Image>();
        imageWeapon = GameObject.Find("WeaponFillImage").GetComponent<Image>();
        imageAtk = GameObject.Find("AtkFillImage").GetComponent<Image>();
        imageCrit = GameObject.Find("CritFillImage").GetComponent<Image>();
        imageSpeed = GameObject.Find("SpeedFillImage").GetComponent<Image>();

        GameObject player = GameObject.FindWithTag("Player");
        imageHP.fillAmount = player.GetComponent<Player>().HP / maxHP;
        imageRegen.fillAmount = player.GetComponent<Player>().regen / maxRegen;
        imageWeapon.fillAmount = (float)countUpgradeWeapon / (float)maxWeapon;
        imageAtk.fillAmount = Weapon.ATK / maxAtk;
        imageCrit.fillAmount = Weapon.critRate / maxCrit;
        imageSpeed.fillAmount = maxSpeed / Shooting.cooldownTime;

        if (dataCoin != -1)
        {
            GameObject.Find("CoinCounter").GetComponent<Text>().text = dataCoin.ToString();
        }
        else
        {
            GetStartCoin();
        }
    }

    private void GetStartCoin()
    {
        switch (UnityEngine.Random.Range(1, 11))
        {
            case 1:
                {
                    dataCoin = 100;
                    break;
                }
            case 2:
                {
                    dataCoin = 100;
                    break;
                }
            case 3:
                {
                    dataCoin = 100;
                    break;
                }
            case 4:
                {
                    dataCoin = 100;
                    break;
                }
            case 5:
                {
                    dataCoin = 100;
                    break;
                }
            case 6:
                {
                    dataCoin = 100;
                    break;
                }
            case 7:
                {
                    dataCoin = 100;
                    break;
                }
            case 8:
                {
                    dataCoin = 300;
                    break;
                }
            case 9:
                {
                    dataCoin = 300;
                    break;
                }
            case 10:
                {
                    dataCoin = 500;
                    break;
                }
        }
        GameObject.Find("CoinCounter").GetComponent<Text>().text = dataCoin.ToString();
    }

    private void Update()
    {
        if (imageHP == null)
        {
            imageHP = GameObject.Find("HealthFillImage").GetComponent<Image>();
        }
        if (imageRegen == null)
        {
            imageRegen = GameObject.Find("RegenFillImage").GetComponent<Image>();
        }
        if (imageWeapon == null)
        {
            imageWeapon = GameObject.Find("WeaponFillImage").GetComponent<Image>();
        }
        if (imageAtk == null)
        {
            imageAtk = GameObject.Find("AtkFillImage").GetComponent<Image>();
        }
        if (imageCrit == null)
        {
            imageCrit = GameObject.Find("CritFillImage").GetComponent<Image>();
        }
        if (imageSpeed == null)
        {
            imageSpeed = GameObject.Find("SpeedFillImage").GetComponent<Image>();
        }
        if (firstTime)
        {
            FirstTime();
        }
    }

    private void FirstTime()
    {
        GameObject.Find("HealthButtonText").GetComponent<Text>().text = (countUpgradeHP * baseCostHP).ToString();
        GameObject.Find("RegenButtonText").GetComponent<Text>().text = (countUpgradeRegen * baseCostRegen).ToString();
        GameObject.Find("WeaponButtonText").GetComponent<Text>().text = (countUpgradeWeapon * baseCostWeapon).ToString();
        GameObject.Find("AtkButtonText").GetComponent<Text>().text = (countUpgradeAtk * baseCostAtk).ToString();
        GameObject.Find("CritButtonText").GetComponent<Text>().text = (countUpgradeCrit * baseCostCrit).ToString();
        GameObject.Find("SpeedButtonText").GetComponent<Text>().text = (countUpgradeSpeed * baseCostSpeed).ToString();

        GameObject player = GameObject.FindWithTag("Player");
        for (int i = 1; i < countUpgradeWeapon; i++)
        {
            weapons[i].SetActive(true);
        }
        if (player.GetComponent<Player>().HP == maxHP)
        {
            GameObject.Find("HealthButtonText").GetComponent<Text>().text = "Max";
        }
        if (player.GetComponent<Player>().regen == maxRegen)
        {
            GameObject.Find("RegenButtonText").GetComponent<Text>().text = "Max";
        }
        if (countUpgradeWeapon == maxWeapon)
        {
            GameObject.Find("WeaponButtonText").GetComponent<Text>().text = "Max";
        }
        if (Shooting.cooldownTime <= maxSpeed)
        {
            GameObject.Find("SpeedButtonText").GetComponent<Text>().text = "Max";
        }
        if (Weapon.ATK == maxAtk)
        {
            GameObject.Find("AtkButtonText").GetComponent<Text>().text = "Max";
        }
        if (Weapon.critRate == maxCrit)
        {
            GameObject.Find("CritButtonText").GetComponent<Text>().text = "Max";
        }

        firstTime = false;
    }

    public void Resume()
    {
        upgradeMenuUI.transform.localScale = Vector3.zero;
        Time.timeScale = 1f;
        isPause = false;
    }

    public void Upgrade()
    {
        upgradeMenuUI.transform.localScale = Vector3.one;
        Time.timeScale = 0f;
        isPause = true;
    }

    public void UpgradeHealth()
    {
        float coin = Convert.ToInt32(GameObject.Find("CoinCounter").GetComponent<Text>().text);
        if (coin >= countUpgradeHP * baseCostHP)
        {
            GameObject text = GameObject.Find("HealthButtonText");
            GameObject player = GameObject.FindWithTag("Player");
            if (player.GetComponent<Player>().HP < maxHP)
            {
                coin -= countUpgradeHP * baseCostHP;
                GameObject.Find("CoinCounter").GetComponent<Text>().text = coin.ToString();
                player.GetComponent<Player>().HP += 500;
                countUpgradeHP++;
                text.GetComponent<Text>().text = (countUpgradeHP * baseCostHP).ToString();
            }
            if (player.GetComponent<Player>().HP == maxHP)
            {
                text.GetComponent<Text>().text = "Max";
            }
            imageHP.fillAmount = player.GetComponent<Player>().HP / maxHP;
        }
    }

    public void UpgradeRegen()
    {
        float coin = Convert.ToInt32(GameObject.Find("CoinCounter").GetComponent<Text>().text);
        if (coin >= countUpgradeRegen * baseCostRegen)
        {
            GameObject text = GameObject.Find("RegenButtonText");
            GameObject player = GameObject.FindWithTag("Player");
            if (player.GetComponent<Player>().regen < maxRegen)
            {
                coin -= countUpgradeRegen * baseCostRegen;
                GameObject.Find("CoinCounter").GetComponent<Text>().text = coin.ToString();
                player.GetComponent<Player>().regen += 30;
                countUpgradeRegen++;
                text.GetComponent<Text>().text = (countUpgradeRegen * baseCostRegen).ToString();
            }
            if (player.GetComponent<Player>().regen == maxRegen)
            {
                text.GetComponent<Text>().text = "Max";
            }
            imageRegen.fillAmount = player.GetComponent<Player>().regen / maxRegen;
        }
    }

    public void UpgradeWeapon()
    {
        float coin = Convert.ToInt32(GameObject.Find("CoinCounter").GetComponent<Text>().text);
        if (coin >= countUpgradeWeapon * baseCostWeapon)
        {
            GameObject text = GameObject.Find("WeaponButtonText");
            if (countUpgradeWeapon < maxWeapon)
            {
                coin -= countUpgradeWeapon * baseCostWeapon;
                GameObject.Find("CoinCounter").GetComponent<Text>().text = coin.ToString();
                weapons[countUpgradeWeapon].SetActive(true);
                countUpgradeWeapon++;
                text.GetComponent<Text>().text = (countUpgradeWeapon * baseCostWeapon).ToString();
            }
            if (countUpgradeWeapon == maxWeapon)
            {
                text.GetComponent<Text>().text = "Max";
            }
            imageWeapon.fillAmount = (float)countUpgradeWeapon / (float)maxWeapon;
        }
    }

    public void UpgradeSpeed()
    {
        float coin = Convert.ToInt32(GameObject.Find("CoinCounter").GetComponent<Text>().text);
        if (coin >= countUpgradeSpeed * baseCostSpeed)
        {
            GameObject text = GameObject.Find("SpeedButtonText");
            if (Shooting.cooldownTime > maxSpeed)
            {
                coin -= countUpgradeSpeed * baseCostSpeed;
                GameObject.Find("CoinCounter").GetComponent<Text>().text = coin.ToString();
                Shooting.cooldownTime -= (float)0.2;
                countUpgradeSpeed++;
                text.GetComponent<Text>().text = (countUpgradeSpeed * baseCostSpeed).ToString();
            }
            if (Shooting.cooldownTime <= maxSpeed)
            {
                text.GetComponent<Text>().text = "Max";
            }
            imageSpeed.fillAmount = maxSpeed / Shooting.cooldownTime;
        }
    }

    public void UpgradeAtk()
    {
        float coin = Convert.ToInt32(GameObject.Find("CoinCounter").GetComponent<Text>().text);
        if (coin >= countUpgradeAtk * baseCostAtk)
        {
            coin -= countUpgradeAtk * baseCostAtk;
            GameObject.Find("CoinCounter").GetComponent<Text>().text = coin.ToString();
            GameObject text = GameObject.Find("AtkButtonText");
            if (Weapon.ATK < maxAtk)
            {
                Weapon.ATK += 100;
                countUpgradeAtk++;
                text.GetComponent<Text>().text = (countUpgradeAtk * baseCostAtk).ToString();
            }
            if (Weapon.ATK == maxAtk)
            {
                text.GetComponent<Text>().text = "Max";
            }
            imageAtk.fillAmount = Weapon.ATK / maxAtk;
        }
    }

    public void UpgradeCrit()
    {
        float coin = Convert.ToInt32(GameObject.Find("CoinCounter").GetComponent<Text>().text);
        if (coin >= countUpgradeCrit * baseCostCrit)
        {
            coin -= countUpgradeCrit * baseCostCrit;
            GameObject.Find("CoinCounter").GetComponent<Text>().text = coin.ToString();
            GameObject text = GameObject.Find("CritButtonText");
            if (Weapon.critRate < maxCrit)
            {
                Weapon.critRate += 1;
                countUpgradeCrit++;
                text.GetComponent<Text>().text = (countUpgradeCrit * baseCostCrit).ToString();
            }
            if (Weapon.critRate == maxCrit)
            {
                text.GetComponent<Text>().text = "Max";
            }
            imageCrit.fillAmount = Weapon.critRate / maxCrit;
        }
    }
}
