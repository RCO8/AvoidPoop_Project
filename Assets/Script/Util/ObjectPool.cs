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

    private GameObject obj;

    private void Awake()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        for (int i = 0; i < pools.Count; ++i)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int j = 0; j < pools[i].size; ++j)
            {
                obj = Instantiate(pools[i].prefab, transform);

                obj.SetActive(false);

                queue.Enqueue(obj);
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
            obj = Instantiate(pools[PoolIndexDictionary[tag]].prefab, transform);
        else
            obj = PoolDictionary[tag].Dequeue();
        
        obj.SetActive(true);

        return obj;
    }

    public void RetrieveObject(string tag, GameObject obj)
    {
        PoolDictionary[tag].Enqueue(obj);
    }
}