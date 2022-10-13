using TouchScript;
using UnityEngine;

public class RoseManager : MonoBehaviour
{
    public float coolTime;
    public float currentCool;

    public Collider groundCollider;

    int _num;

    public int currentSceneNum;

    public MultipleObjectPooling myMagicParticles;

    public MultipleObjectPooling myParticles;

    public GameObject magicWindow;

    public GameObject flowerWindow;

    public bool isQuit;

    public float endTime;

    public bool canSpawn = true;
    
    [Range(0, 8)] public int filterLevel = 0;
    
    private Vector2 _recentVector2;
    
    public double sizeVector2 = 90f;
    
    public float spawnDistance1 = 10f;
    
    public float spawnDistance2 = 10f;


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
    
    // Start is called before the first frame update
    void Start()
    {
        myParticles.CreateMultiplePoolObjects();
        myMagicParticles.CreateMultiplePoolObjects();
        
        currentSceneNum = 0;
        _num = -1001;
        // SwitchNum();
    }

    private void SwitchNum()
    {
        switch (currentSceneNum)
        {
            case 0:
                currentSceneNum = 1;
                flowerWindow.SetActive(false);
                magicWindow.SetActive(true);
                break;


            case 1:
                currentSceneNum = 0;
                _num = -1001;
                flowerWindow.SetActive(true);
                magicWindow.SetActive(false);
                break;

        }
    }

    void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {

        if(endTime < 17700)
        {
            endTime += Time.deltaTime;
        }
        else
        {

            if (!isQuit)
            {
                isQuit = true;
                Invoke(nameof(GameQuit), 10.0f);
            }
            endTime = 0;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isQuit)
            {
                isQuit = true;
                Invoke(nameof(GameQuit), 10.0f);
            }

        }

        if (isQuit || SizeControl.Instance.isWindowOpen)
        {
            return;
        }

        if (currentCool < 120)
        {
            currentCool += Time.deltaTime;
        }
        else
        {
            currentCool = 0;
            SwitchNum();
        }
    }

    private void SpawnPrefabAt(Vector2 position)
    {
        //if (Camera.main != null && groundCollider.Raycast(Camera.main.ScreenPointToRay(position), out var hit, 9999f)) {}
        
        switch (currentSceneNum)
            {
                case 0:

                    int ran22 = Random.Range(0, myParticles.poolPrefabs.Length);
                    var particle = myParticles.GetPooledObject(myParticles.poolPrefabs[ran22]
                        .name); //GameObject  = Instantiate(myParticles.poolPrefabs[ran22]);
                    
                    if (Camera.main != null)
                        particle.transform.position =
                            Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, spawnDistance1));
                    //particle.transform.position = hit.point + particle.transform.position + new Vector3(0, 0.1f, 0);
                    particle.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
                    float ran = Random.Range(0.5f, 1.1f);
                    particle.transform.localScale = new Vector3(ran, ran, ran);
                    particle.SetActive(true);
                    //Destroy(particle, 18.0f);

                    _num++;

                    if (_num >= -2)
                        _num = -1001;
                    particle.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = _num;
                    SoundManager.Instance.roseSFX.Play();

                    break;
                case 1:

                    int ran33 = Random.Range(0, myMagicParticles.poolPrefabs.Length);
                    var particle2 = myMagicParticles.GetPooledObject(myMagicParticles.poolPrefabs[ran33]
                        .name); //GameObject  = Instantiate(myParticles.poolPrefabs[ran22]);

                    // particle2.transform.position = hit.point + particle2.transform.position + new Vector3(0, 0.5f, 0);

                    if (Camera.main != null)
                        particle2.transform.position =
                            Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, spawnDistance2));
                    
                    particle2.SetActive(true);
                    //Destroy(particle2, 5.0f);

                    SoundManager.Instance.shockSFX.Play();

                    break;
            }
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
        print("Mathf.Approximately1");
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
}
