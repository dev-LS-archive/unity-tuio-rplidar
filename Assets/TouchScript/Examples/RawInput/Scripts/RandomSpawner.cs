/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using System.Collections;
using UnityEngine;

namespace TouchScript.Examples.RawInput
{
    /// <exclude />
    public class RandomSpawner : MonoBehaviour
    {
        public GameObject[] prefabs;
        public float spawnDistance = 10f;
        public float delay = 0.5f;
        public bool canSpawn = true;

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
            var obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
            if (Camera.main != null)
                obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, spawnDistance));
            obj.transform.rotation = transform.rotation;
        }

        private void PointersPressedHandler(object sender, PointerEventArgs e)
        {
            if (canSpawn == true)
            {
                Vector2 center = default;
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

                center = new Vector2(xSum / e.Pointers.Count, ySum / e.Pointers.Count);
                StartCoroutine(Delay(center));
            }
        }

        IEnumerator Delay(Vector2 center)
        {
            canSpawn = false;
            SpawnPrefabAt(center);
            yield return new WaitForSeconds(delay);
            canSpawn = true;
        }
    }
}
