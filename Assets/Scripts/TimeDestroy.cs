using System.Collections;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(nameof(WaitDestroy));
    }
    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
