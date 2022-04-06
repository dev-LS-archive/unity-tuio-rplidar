//Attach this script to your Canvas GameObject.
//Also attach a GraphicsRaycaster component to your canvas by clicking the Add Component button in the Inspector window.
//Also make sure you have an EventSystem in your hierarchy.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Clickprefab_UI_Ray : MonoBehaviour
{
    public GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;

    int num;
    public GameObject[] myParticles;

    //void Start()
    //{
    //    //Fetch the Raycaster from the GameObject (the Canvas)
    //    m_Raycaster = GetComponent<GraphicRaycaster>();
    //    //Fetch the Event System from the Scene
    //    m_EventSystem = GetComponent<EventSystem>();
    //}

    void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    //Set up the new Pointer Event
        //    m_PointerEventData = new PointerEventData(m_EventSystem);
        //    //Set the Pointer Event Position to that of the mouse position
        //    m_PointerEventData.position = Input.mousePosition;

        //    Debug.DrawRay(m_PointerEventData.position, Vector3.forward, Color.red);
        //}
        //Check if the left Mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Left Clicked");
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
                int ran22 = Random.Range(0, myParticles.Length);
                GameObject particle = Instantiate(myParticles[ran22]);

                particle.transform.position = result.worldPosition + particle.transform.position + new Vector3(0, 0.5f, 0);
                //   particle.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
                //   float ran = Random.Range(0.5f, 1.1f);
                //    particle.transform.localScale = new Vector3(ran, ran, ran);
                Destroy(particle, 6.0f);

                num++;
            }
        }
    }
}
