using UnityEngine;

namespace CustomDev.Scripts
{
    public class MiddleManager : MonoBehaviour
    {

        public float coolTime;
        public float currentCool;

        public Collider groundCollider;

        int num;

        public int currentSceneNum;

        public GameObject[] myMasicParticles;

        public GameObject[] myParticles;

        public GameObject masicWindow;

        public GameObject flowerWindow;

        public bool isQuit;

        public float endTime;



        // Start is called before the first frame update
        void Start()
        {
            currentSceneNum = 0;
            num = -1001;
            // SwichNum();
        }

        public void SwichNum()
        {
            switch (currentSceneNum)
            {
                case 0:
                    currentSceneNum = 1;
                    flowerWindow.SetActive(false);
                    masicWindow.SetActive(true);
                    break;


                case 1:
                    currentSceneNum = 0;
                    num = -1001;
                    flowerWindow.SetActive(true);
                    masicWindow.SetActive(false);
                    break;

            }
        }

        void gameQuit()
        {
            Application.Quit();
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
                    Invoke("gameQuit", 10.0f);
                }
                endTime = 0;
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isQuit)
                {
                    isQuit = true;
                    Invoke("gameQuit", 10.0f);
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
                SwichNum();
            }

            if (Input.GetMouseButtonDown(0))
            {


                // Debug.Log("왼쪽클릭");
                RaycastHit hit = new RaycastHit();
                if (groundCollider.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 9999f))
                {
                    // Debug.Log("레이");
                    switch (currentSceneNum)
                    {
                        case 0:

                            int ran22 = Random.Range(0, myParticles.Length);
                            GameObject particle = Instantiate(myParticles[ran22]);

                            particle.transform.position = hit.point + particle.transform.position + new Vector3(0, 0.1f, 0);
                            particle.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
                            float ran = Random.Range(0.5f, 1.1f);
                            particle.transform.localScale = new Vector3(ran, ran, ran);
                            Destroy(particle, 18.0f);

                            num++;

                            if (num >= -2)
                                num = -1001;
                            particle.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = num;
                            SoundManager.Instance.roseSFX.Play();

                            break;
                        case 1:

                            int ran33 = Random.Range(0, myMasicParticles.Length);
                            GameObject particle2 = Instantiate(myMasicParticles[ran33]);

                            // particle2.transform.position = hit.point + particle2.transform.position + new Vector3(0, 0.5f, 0);

                            particle2.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                            Destroy(particle2, 5.0f);

                            SoundManager.Instance.shockSFX.Play();

                            break;
                    }

                }


            }


        }
    }
}
