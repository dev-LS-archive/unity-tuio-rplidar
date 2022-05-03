using UnityEngine;

public class BackgroundsChangerCustom : MonoBehaviour

{
    public GameObject[] sceneObjects;
    private int _prefab;
    public int activeObject;
    
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
        if (sceneObjects[activeObject].activeInHierarchy)
        {
            sceneObjects[activeObject].SetActive(false);
        }
        activeObject = _prefab;
        sceneObjects[_prefab].SetActive(true);
    }
    
    public void Active_Nth(int num)
    {
        if (sceneObjects[activeObject].activeInHierarchy)
        {
            sceneObjects[activeObject].SetActive(false);
        }
        activeObject = num;
        sceneObjects[num].SetActive(true);
    }
}