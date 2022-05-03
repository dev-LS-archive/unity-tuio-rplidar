using UnityEngine;

public class BackgroundsChangerCustom : MonoBehaviour
{
    public GameObject[] sceneObjects;
    private int _prefab;
    private int _activeObject;
    
    void Start()
    {
        Counter(0);
    }

    public void Counter(int count)
    {
        _prefab += count;
        if (_prefab > sceneObjects.Length - 1)
        {
            _prefab = 0;
        }
        else if (_prefab < 0)
        {
            _prefab = sceneObjects.Length - 1;
        }
        if (sceneObjects[_activeObject].activeInHierarchy)
        {
            sceneObjects[_activeObject].SetActive(false);
        }
        _activeObject = _prefab;
        sceneObjects[_prefab].SetActive(true);
    }
}