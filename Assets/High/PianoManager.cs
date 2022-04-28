using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoManager : MonoBehaviour
{
    static PianoManager instance;
    public static PianoManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PianoManager>();
            }
            return instance;
        }

    }


    public GameObject[] Fires;



}
