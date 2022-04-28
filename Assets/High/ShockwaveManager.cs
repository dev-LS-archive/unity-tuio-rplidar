using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveManager : MonoBehaviour
{

    public Collider groundCollider;

    int num;
    public GameObject[] myParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {


            Debug.Log("왼쪽클릭");
            RaycastHit hit = new RaycastHit();
            if (groundCollider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 9999f))
            {
                Debug.Log("레이");

                int ran22 = Random.Range(0, myParticles.Length);
                GameObject particle = Instantiate(myParticles[ran22]);

                particle.transform.position = hit.point + particle.transform.position + new Vector3(0, 0.5f, 0);
             //   particle.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
             //   float ran = Random.Range(0.5f, 1.1f);
            //    particle.transform.localScale = new Vector3(ran, ran, ran);
                Destroy(particle, 6.0f);

                num++;



                SoundManager.Instance.shockSFX.Play();
            }


        }

    }
}
