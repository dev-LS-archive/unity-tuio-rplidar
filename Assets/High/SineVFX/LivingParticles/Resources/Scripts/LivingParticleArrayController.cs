using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingParticleArrayController : MonoBehaviour {




    static LivingParticleArrayController instance;
    public static LivingParticleArrayController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LivingParticleArrayController>();
            }

            return instance;
        }

    }

    public Transform[] affectors;

    private Vector4[] positions;
    private ParticleSystemRenderer psr;

    public List<Vector4> myList = new List<Vector4>();

	void Start () {
        psr = GetComponent<ParticleSystemRenderer>();
        Vector4[] maxArray = new Vector4[20];
        psr.material.SetVectorArray("_Affectors", maxArray);
    }

    // Sending an array of positions to particle shader
    void Update () {

        /*
        positions = new Vector4[affectors.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = affectors[i].position;
        }
        psr.material.SetVectorArray("_Affectors", positions);
        psr.material.SetInt("_AffectorCount", affectors.Length);
        */
        

        
        positions = new Vector4[myList.Count];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = myList[i];
        }
        psr.material.SetVectorArray("_Affectors", positions);
        psr.material.SetInt("_AffectorCount", myList.Count+1);
        
    }
}
