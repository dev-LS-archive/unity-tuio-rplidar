/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using UnityEngine;

namespace TouchScript.Examples.RawInput
{
    /// <exclude />
    public class Spawner : MonoBehaviour
    {
        public GameObject prefab;

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
                obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
            obj.transform.rotation = transform.rotation;
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