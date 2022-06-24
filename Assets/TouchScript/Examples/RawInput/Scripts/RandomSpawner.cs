// ReSharper disable once CommentTypo
/*
 * @author Valentin Simonov / http://va.lent.in/
 */

//sing System;
using System.Collections;
using UnityEngine;
//using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
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

        public int spawnNum;

        private Vector2 _recentVector2;
        private bool _firstSpawn;

        public double sizeVector2 = 72f;
        
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
                canSpawn = true;
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
            FloorInfo.Instance.HideInfo();

            var obj = pooling.GetPooledObject(prefabs[spawnNum]
                .name); //Instantiate(prefabs[spawnNum]);//Random.Range(0, prefabs.Length)
            if (spawnNum < prefabs.Length - 1 && prefabs.Length != 1)
                spawnNum += 1;
            else
                spawnNum = 0;

            if (Camera.main != null)
                obj.transform.position =
                    Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, spawnDistance));
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);
        }

        private void PointersPressedHandler(object sender, PointerEventArgs e)
        {
            if (canSpawn != true)
                return;

            float[] x = new float[e.Pointers.Count];
            float[] y = new float[e.Pointers.Count];
            int i = 0;
            //float xSum = 0, ySum = 0;
            foreach (var pointer in e.Pointers)
            {
                x[i] = pointer.Position.x;
                y[i] = pointer.Position.y;
                var center = new Vector2(x[i], y[i]);
                StartCoroutine(Delay(center));
                //xSum += x[i];
                //ySum += y[i];
            }

            //var center = new Vector2(xSum / e.Pointers.Count, ySum / e.Pointers.Count);
            //StartCoroutine(Delay(center));
        }

        /*
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
            _recentVector2 = center;
            yield return new WaitForSeconds(delay);
            canSpawn = true;
        }
        */

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
            if (_firstSpawn == true)
            {
                _firstSpawn = false;
                SpawnPrefabAt(center);
            }
            //이전 스폰 위치와 비슷할 경우에 스폰X
            else if (!Mathf.Approximately(_recentVector2.x, center.x)
                     && !Mathf.Approximately(_recentVector2.y, center.y))
            {
                //SpawnPrefabAt(center);
                //_recentVector2 = center;
                bool areaCheckX = _recentVector2.x - sizeVector2 < center.x && _recentVector2.x + sizeVector2 > center.x;
                bool areaCheckY = _recentVector2.y - sizeVector2 < center.y && _recentVector2.y + sizeVector2 > center.y;
                //print("Mathf.Approximately1");
                //print(areaCheckX + "/" + areaCheckY);
                if (!areaCheckX || !areaCheckY)
                {
                    SpawnPrefabAt(center);
                    _recentVector2 = center;
                    //print("Mathf.Approximately2");
                }
            }
            yield return new WaitForSeconds(delay);
            canSpawn = true;
            _firstSpawn = false;
        }
    }
}