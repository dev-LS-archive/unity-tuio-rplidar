using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyBtn : MonoBehaviour, IPointerDownHandler
{

    public string myName;
    public GameObject myPos;
    public AudioSource mySound;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        int a = Random.Range(0,PianoManager.Instance.Fires.Length);
        GameObject clone = Instantiate(PianoManager.Instance.Fires[a]);
        clone.transform.position = new Vector3(transform.position.x, transform.position.y, -4) ;
        Destroy(clone, 6.0f);
       
        StartCoroutine("FireSound");
        mySound.Play();

    }


    IEnumerator FireSound()
    {
        yield return new WaitForSeconds(2.0f);
        SoundManager.Instance.fireSFX.Play();
    }


}
