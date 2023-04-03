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

        public double sizeVector2 = 72f;
        [SerializeField] private float xSum, ySum;
        [Range(0, 8)] public int filterLevel = 0;

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
            float[] x = new float[e.Pointers.Count];
            float[] y = new float[e.Pointers.Count];

            int i = 0;
            // //float xSum = 0, ySum = 0;
            //
            // foreach (var pointer in e.Pointers)
            // {
            //     if (e.Pointers.Count < 10)
            //     {
            //         x[i] = pointer.Position.x;
            //         y[i] = pointer.Position.y;
            //         var center = new Vector2(x[i], y[i]);
            //         if (!Mathf.Approximately(_recentVector2.x, center.x)
            //             && !Mathf.Approximately(_recentVector2.y, center.y))
            //         {
            //             print("center");
            //             Filter(center);
            //             //if(canSpawn == true)
            //                 //SpawnCheck();
            //         }
            //         print(i);
            //
            //         i++;
            //         //xSum += x[i];
            //         //ySum += y[i];
            //     }
            // }

            foreach (var pointer in e.Pointers)
            {
                if (e.Pointers.Count < 10)
                {
                    if (filterLevel == i)
                    {
                        x[filterLevel] = pointer.Position.x;
                        y[filterLevel] = pointer.Position.y;
                        var center = new Vector2(x[filterLevel], y[filterLevel]);
                        if (!Mathf.Approximately(_recentVector2.x, center.x)
                            && !Mathf.Approximately(_recentVector2.y, center.y))
                        {
#if UNITY_EDITOR
                            //print("center");
#endif
                            Filter(center);
                            //if(canSpawn == true)
                                //SpawnCheck();
                        }
                        //break;
                    }
                    //print(i);
                    i++;
                }
            }
            //var center1 = new Vector2(x[0], y[0]);
            //print("center");
            //Filter(center1);
        }
        /*
        IEnumerator Filter(Vector2 center)
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

        private void Filter(Vector2 center)
        {
            // for (int i = 0; i < backChanger.sceneObjects.Length; i++)
            // {
            //     if (backChanger.sceneObjects[i].activeSelf == true)
            //     {
            //         backChanger.activeObject = i;
            //         Instantiate(backEffects[backChanger.activeObject]);
            //     }
            // }
            //print(center);
            //SpawnPrefabAt(center);
            //_recentVector2 = center;
            if (canSpawn != true)
                return;
            
            
            bool areaCheckX = _recentVector2.x - sizeVector2 < center.x && _recentVector2.x + sizeVector2 > center.x;
            bool areaCheckY = _recentVector2.y - sizeVector2 < center.y && _recentVector2.y + sizeVector2 > center.y;
#if UNITY_EDITOR
            //print("Mathf.Approximately1");
#endif
            //print(areaCheckX + "/" + areaCheckY);
            //print(center);
            if (!areaCheckX || !areaCheckY)
            {
                SpawnPrefabAt(center);
                _recentVector2 = center;
#if UNITY_EDITOR
                //print("Mathf.Approximately2");
#endif
            }
                //yield return new WaitForSeconds(delay);
            
        }

        private void SpawnCheck()
        {
            if (canSpawn != true)
                return;
            canSpawn = false;
            Invoke(nameof(CanSpawnControl), delay);
        }

        private void CanSpawnControl()
        {
            //print("SC");
            canSpawn = true;
        }
    }
}