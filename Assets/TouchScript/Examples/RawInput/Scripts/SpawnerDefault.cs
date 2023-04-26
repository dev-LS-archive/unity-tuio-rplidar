/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using System.Collections;
using UnityEngine;

namespace TouchScript.Examples.RawInput
{
    /// <exclude />
    public class SpawnerDefault : MonoBehaviour
    {
        public GameObject prefab;
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
            var obj = Instantiate(prefab);
            if (Camera.main != null)
                obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, spawnDistance));
            obj.transform.rotation = transform.rotation;
        }

        IEnumerator Delay(Vector2 center)
        {
            canSpawn = false;
            SpawnPrefabAt(center);
            yield return new WaitForSeconds(delay);
            canSpawn = true;
        }

        private void PointersPressedHandler(object sender, PointerEventArgs e)
        {
            foreach (var pointer in e.Pointers)
            {
                SpawnPrefabAt(pointer.Position);
            }
        }
    }
}
