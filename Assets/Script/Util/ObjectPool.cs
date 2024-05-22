using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> PoolDictionary;
    private Dictionary<string, int> PoolIndexDictionary;

    private GameObject obj = null;

    private void Awake()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        PoolIndexDictionary = new Dictionary<string, int>();

        for (int i = 0; i < pools.Count; ++i)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int j = 0; j < pools[i].size; ++j)
            {
                GameObject obj1 = Instantiate(pools[i].prefab);

                obj1.SetActive(false);

                queue.Enqueue(obj1);
            }

            PoolDictionary.Add(pools[i].tag, queue);
            PoolIndexDictionary.Add(pools[i].tag, i);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
            return null;

        if (0 == PoolDictionary[tag].Count)
            obj = Instantiate(pools[PoolIndexDictionary[tag]].prefab);
        else
            obj = PoolDictionary[tag].Dequeue();
        
        obj.SetActive(true);

        return obj;
    }

    public void RetrieveObject(string tag, GameObject obj)
    {
        obj.SetActive(false);

        PoolDictionary[tag].Enqueue(obj);
    }

    public GameObject LinkedSpawnFromPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
            return null;

        obj = PoolDictionary[tag].Dequeue();
        obj.SetActive(true);
        PoolDictionary[tag].Enqueue(obj);

        return obj;
    }
}