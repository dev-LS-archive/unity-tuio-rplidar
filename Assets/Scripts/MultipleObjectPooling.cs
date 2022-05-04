using System.Collections.Generic;
using UnityEngine;

public class MultipleObjectPooling : MonoBehaviour
{
    public GameObject[] poolPrefabs;
    public int poolingCount;
    private readonly Dictionary<object, List<GameObject>> _pooledObjects = new Dictionary<object, List<GameObject>>();
    
    public void CreateMultiplePoolObjects()
    {
        for (int i = 0; i < poolPrefabs.Length; i++)
        {
            for (int j = 0; j < poolingCount; j++)
            {
                if (!_pooledObjects.ContainsKey(poolPrefabs[i].name))
                {
                    List<GameObject> newList = new List<GameObject>();
                    _pooledObjects.Add(poolPrefabs[i].name, newList);
                }

                GameObject newDoll = Instantiate(poolPrefabs[i], transform);
                newDoll.SetActive(false);
                _pooledObjects[poolPrefabs[i].name].Add(newDoll);
            }
        }
    }

    public GameObject GetPooledObject(string _name)
    {
        if (_pooledObjects.ContainsKey(_name))
        {
            for (int i = 0; i < _pooledObjects[_name].Count; i++)
            {
                if (!_pooledObjects[_name][i].activeSelf)
                {
                    return _pooledObjects[_name][i];
                }
            }

            int beforeCreateCount = _pooledObjects[_name].Count;

            CreateMultiplePoolObjects();

            return _pooledObjects[_name][beforeCreateCount];
        }
        else
        {
            return null;
        }
    }
}
