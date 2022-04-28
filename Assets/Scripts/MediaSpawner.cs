using System;
using System.Collections;
using TouchScript;
using UnityEngine;
using Random = UnityEngine.Random;

public class MediaSpawner : MonoBehaviour
{
    #region Variables

    [SerializeField] private float coolTime = 0.5f;
    [SerializeField] private bool canTouch = true;

    int num;
    
    public int currentSceneNum;

    public GameObject[] myMagicParticles;

    public GameObject[] myParticles;

    public GameObject magicWindow;

    public GameObject flowerWindow;

    public bool isQuit;

    public float endTime;

    #endregion

    /// <exclude />

    public float distance = 10f;

    private void OnEnable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.PointersPressed += PointersPressedHandler;
        }
        canTouch = true;
        StartCoroutine(nameof(CoolTouch));
    }

    private void OnDisable()
    {
        if (TouchManager.Instance != null)
        {
            TouchManager.Instance.PointersPressed -= PointersPressedHandler;
        }

        StopCoroutine(nameof(CoolTouch));
    }

    private void SpawnPrefabAt(Vector2 position)
    {
        var nTh = Random.Range(0, myParticles.Length);
        var obj = Instantiate(myParticles[nTh]);
        if (Camera.main != null)
            obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, distance));
        obj.transform.rotation = transform.rotation;
    }

    private void PointersPressedHandler(object sender, PointerEventArgs e)
    {
        foreach (var pointer in e.Pointers)
        {
            SpawnPrefabAt(pointer.Position);
        }
    }

    IEnumerator CoolTouch()
    {
        canTouch = false;
        yield return new WaitForSeconds(coolTime);
        canTouch = true;
    }
}
