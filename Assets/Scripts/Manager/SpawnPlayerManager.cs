using UnityEngine;

public class SpawnPlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("FirePoint");
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }
}
