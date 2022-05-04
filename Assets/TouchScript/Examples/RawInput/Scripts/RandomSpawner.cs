/*
 * @author Valentin Simonov / http://va.lent.in/
 */

//sing System;
using System.Collections;
using UnityEngine;
//using Random = UnityEngine.Random;

namespace TouchScript.Examples.RawInput
{
    /// <exclude />
    public class RandomSpawner : MonoBehaviour
    {
        public GameObject[] prefabs;
        public GameObject[] backEffects;
        public BackgroundsChangerCustom backChanger;
        public MultipleObjectPooling pooling;
        public float spawnDistance = 10f;
        public float delay = 0.5f;
        public bool canSpawn = true;

        public int spawnNum = 0;

        private void Start()
        {
            spawnNum = 0;
            pooling.CreateMultiplePoolObjects();
            prefabs = pooling.poolPrefabs;
        }

        private void OnEnable()
        {
            if (TouchManager.Instance != null)
            {
                TouchManager.Instance.PointersPressed += PointersPressedHandler;
            }
        }

        private void OnDisable()
        {
            if (TouchManager.Instance != null)
            {
                TouchManager.Instance.PointersPressed -= PointersPressedHandler;
            }
        }

        private void SpawnPrefabAt(Vector2 position)
        {
            var obj = pooling.GetPooledObject(prefabs[spawnNum].name);//Instantiate(prefabs[spawnNum]);//Random.Range(0, prefabs.Length)
            if (spawnNum < prefabs.Length - 1 && prefabs.Length != 1)
                spawnNum+=1;
            else
                spawnNum = 0;
            
            if (Camera.main != null)
                obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, spawnDistance));
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);
        }

        private void PointersPressedHandler(object sender, PointerEventArgs e)
        {
            if (canSpawn == true)
            {
                float[] x = new float[e.Pointers.Count];
                float[] y = new float[e.Pointers.Count];
                int i = 0; float xSum=0, ySum=0;
                foreach (var pointer in e.Pointers)
                {
                    x[i] = pointer.Position.x;
                    y[i] = pointer.Position.y;
                    xSum += x[i];
                    ySum += y[i];
                }
                
                var center = new Vector2(xSum / e.Pointers.Count, ySum / e.Pointers.Count);
                StartCoroutine(Delay(center));
            }
        }

        IEnumerator Delay(Vector2 center)
        {
            // for (int i = 0; i < backChanger.sceneObjects.Length; i++)
            // {
            //     if (backChanger.sceneObjects[i].activeSelf == true)
            //     {
            //         backChanger.activeObject = i;
            //         Instantiate(backEffects[backChanger.activeObject]);
            //     }
            // }
            
            canSpawn = false;
            SpawnPrefabAt(center);
            yield return new WaitForSeconds(delay);
            canSpawn = true;
        }
    }
}
