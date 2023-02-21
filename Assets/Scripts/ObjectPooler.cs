using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public int maxSize;
        public void SetSize(int size)
        {
            this.size = size;
        }
    }
    public List<Pool> pools;

    private Dictionary<string, List<GameObject>> poolDictionary;
    private void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (var pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.name = pool.tag + (i + 1);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        List<GameObject> items;

        var p = pools.FirstOrDefault(e => e.tag.Equals(tag));
        if (p != null)
        {
            if (!poolDictionary.TryGetValue(tag, out items))
            {
                return null;
            }

            if (p.maxSize < p.size)
            {
                return null;
            }

            GameObject objectToSpawn = items.FirstOrDefault(e => !e.activeInHierarchy);

            if (objectToSpawn != null)
            {
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                return objectToSpawn;
            } else
            {
                if (p.size == p.maxSize)
                {
                    return null;
                }

                objectToSpawn = items.ElementAtOrDefault(0);
                if (objectToSpawn == null)
                {
                    return null;
                }
                GameObject obj = Instantiate(objectToSpawn);

                obj.name = tag + (p.size + 1);
                obj.SetActive(true);
                obj.transform.position = position;
                obj.transform.rotation = rotation;

                items.Add(obj);
                p.SetSize(p.size + 1);

                return objectToSpawn;
            }
        } else
        {
            
        }

        return null;
    }
}
