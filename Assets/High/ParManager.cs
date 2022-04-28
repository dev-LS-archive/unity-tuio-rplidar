using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider groundCollider;
    public GameObject myCube;
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

                //  Vector3 pp = hit.point + new Vector3(0, 0.5f, 0);
                
                Vector4 pp = hit.point;
                LivingParticleArrayController.Instance.myList.Add(pp);

                Invoke("DEDFX",0.5f);




               // myCube.transform.position =  new Vector3(hit.point.x, 2, hit.point.z)  ;

               // StartCoroutine("delssFX");
            }


        }

    }


    public void DEDFX(Vector4 ppp)
    {
      //  int num = LivingParticleArrayController.Instance.myList.Find();
        LivingParticleArrayController.Instance.myList.Clear();
       // LivingParticleArrayController.Instance.myList.Add(asd.transform.position);
    }
    IEnumerator delssFX()
    {
        yield return new WaitForSeconds(0.1f);

        myCube.transform.position = new Vector3(99, 99, 99);

    }

}
