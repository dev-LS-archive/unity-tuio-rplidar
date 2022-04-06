using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("waitDestroy");
    }
    IEnumerator waitDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
