using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnEnemyManager : MonoBehaviour, IDataPersistance
{
    public static bool canSave = false;
    public enum SpawnState { SPAWNING, WAITTING, COUNTING }

    public Text waveCounter;
    private int nextWave;

    public float timeBetweenWaves;
    private float waveCountDown;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private int minEnemy = 10;
    private int maxEnemy = 31;

    ObjectPooler objectPooler;

    public void LoadData(GameData gameData)
    {
        this.nextWave = gameData.nextWave;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.nextWave = this.nextWave;
    }

    void Start()
    {
        WaveCountDown();
        GameObject.Find("WaveCounter").GetComponent<Text>().text = nextWave.ToString();
        waveCountDown = timeBetweenWaves;
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate()
    {
        WaveCountDown();
        if (state == SpawnState.WAITTING)
        {
            if (!EnemyIsAlive() && Convert.ToInt32(GameObject.Find("WaveCounter").GetComponent<Text>().text) != 0)
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

        Vector3 position;
        GameObject player = GameObject.FindWithTag("Player");
        do
        {
            position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);
        } while (Vector3.Distance(position, player.transform.position) < player.gameObject.GetComponent<Player>().SafeDistance);
    }

    void WaveCountDown()
    {
        GameObject waveCountdownText = GameObject.Find("WaveCountdown");
        if (waveCountDown > 0)
        {
            waveCountdownText.GetComponent<Text>().text = "Start wave in:" + String.Format(waveCountDown.ToString("0"));
        }
        else
        {
            waveCountdownText.GetComponent<Text>().text = "";
        }
    }

    void WaveCompleted()
    {
        canSave = true;
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        GetNumberOfEnemy();
        nextWave++;
    }

    private void GetNumberOfEnemy()
    {
        if (nextWave <= 10)
        {
            minEnemy = 10;
            maxEnemy = 31;
        }else if(nextWave <= 20)
        {
            minEnemy = 20;
            maxEnemy = 41;
        }
        else if (nextWave <= 30)
        {
            minEnemy = 30;
            maxEnemy = 51;
        }
        else if (nextWave <= 40)
        {
            minEnemy = 40;
            maxEnemy = 61;
        }
        else if (nextWave <= 50)
        {
            minEnemy = 50;
            maxEnemy = 71;
        }
        else if (nextWave <= 60)
        {
            minEnemy = 60;
            maxEnemy = 81;
        }
        else if (nextWave <= 70)
        {
            minEnemy = 70;
            maxEnemy = 91;
        }
        else if (nextWave <= 80)
        {
            minEnemy = 80;
            maxEnemy = 101;
        }
        else if (nextWave <= 90)
        {
            minEnemy = 90;
            maxEnemy = 111;
        }
        else if (nextWave <= 100)
        {
            minEnemy = 100;
            maxEnemy = 121;
        }
        else
        {
            minEnemy = 100;
            maxEnemy = 150;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave()
    {
        waveCounter.text = (nextWave + 1).ToString();
        state = SpawnState.SPAWNING;
        int numberOfEnemy = Random.Range(minEnemy, maxEnemy);
        if ((nextWave + 1) % 5 == 0)
        {
            SpawnEnemy();
        }
        else
        {
            for (int i = 0; i < numberOfEnemy; i++)
            {
                SpawnEnemy();
            }
        }
        state = SpawnState.WAITTING;

        yield break;
    }

    void SpawnEnemy()
    {
        int number = Random.Range(1, 4);
        switch (number)
        {
            case 1:
                objectPooler.SpawnFromPool("Enemy 1", new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                break;
            case 2:
                objectPooler.SpawnFromPool("Enemy 2", new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                break;
            case 3:
                objectPooler.SpawnFromPool("Enemy 3", new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                break;
        }
    }
}
