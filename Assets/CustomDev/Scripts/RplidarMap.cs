using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

[RequireComponent(typeof(MeshFilter))]
public class RplidarMap : MonoBehaviour {

    private float delayTime;
    public bool m_onscan = false;

    private LidarData[] m_data;
    public string COM = "COM3";

    public Mesh m_mesh;
    private List<Vector3> m_vert;
    private List<int> m_ind;

    private MeshFilter m_meshfilter;

    private Thread m_thread;
    private bool m_datachanged = false;

    public MouseControl MC;

    public List<OverlapTouch> list_Overlap;
    public int startNum;

    #region 중복터치체크
    public class OverlapTouch
    {
        public int x;
        public int y;
        public float lifeTime;
    }
    #endregion

    void Start () {

        m_meshfilter = GetComponent<MeshFilter>();

        m_data = new LidarData[720];

        m_ind = new List<int>();
        m_vert = new List<Vector3>();
        for (int i = 0; i < 720; i++)
        {
            m_ind.Add(i);
        }
        m_mesh = new Mesh();
        m_mesh.MarkDynamic();

        RplidarBinding.OnConnect(COM);
        RplidarBinding.StartMotor();
        m_onscan = RplidarBinding.StartScan();

        list_Overlap = new List<OverlapTouch>();
        if (m_onscan)
        {
          //  m_thread = new Thread(GenMesh);
          //  m_thread.Start();
        }

    }

    void OnDestroy()
    {
      //  m_thread.Abort();

        RplidarBinding.EndScan();
        RplidarBinding.EndMotor();
        RplidarBinding.OnDisconnect();
        RplidarBinding.ReleaseDrive();

      //  m_onscan = false;
    }

    void Update()
    {
        delayTime += Time.deltaTime;

           
        if (m_datachanged)
        {
            Scan_Lidar();
            OverListCount();
        }
        

        if(delayTime >= 0.1f)
        {
            int datacount = RplidarBinding.GetData(ref m_data);
            if (datacount == 0)
            {
               // Thread.Sleep(20);
            }
            else
            {
                m_datachanged = true;
            }

            delayTime = 0;
        }

    }


    public void Scan_Lidar()
    {
        if (startNum < 1)
        {
            startNum++;
        }
        else
        {
            m_vert.Clear();

          //  Debug.Log("" + 11);
            for (int i = 0; i < 720; i++)
            {

                m_vert.Add(Quaternion.Euler(0, 0, m_data[i].theta) * Vector3.right * m_data[i].distant * 0.01f);

                Vector3 myVec = Location_Calculation(m_vert[i].x, m_vert[i].y);


                OverlapTouch myOt = list_Overlap.Find(OverlapTouch => OverlapTouch.x == myVec.x && OverlapTouch.y == myVec.y);

                if (myOt == null)
                {
                    OverlapTouch OT = new OverlapTouch();
                    OT.lifeTime = 1.5f;
                    OT.x = (int)myVec.x;
                    OT.y = (int)myVec.y;
                    list_Overlap.Add(OT);
                   

                    int X = (int)((m_vert[i].y) * 56.47f) + 950;
                    int Y = (int)((m_vert[i].x - 7) * 46.95f - 10);

                    if (m_vert[i].y < 0)
                    {
                        X = (int)((Math.Abs(m_vert[i].y) ) * 56.47f) + 950;
                    }
                    else
                    {
                        X = 950 - (int)((Math.Abs(m_vert[i].y )) * 56.47f) ;
                    }


                    //범위조정
                    if (!(m_vert[i].x < 7.02f || m_vert[i].x > 30 || m_vert[i].y < -9.5f || m_vert[i].y > 9.5f))
                    {
                        Debug.Log("리스트크기 : " + list_Overlap.Count + "지금 좌표 " + m_vert[i]);

                        //클릭까지하기

                           Vector3 vec = new Vector3(X, Y, 0);
                           MC.myQueue.Enqueue(vec);





                        //이동만테스트
                        //   MC.MC_Move(X, Y);
                       //  Debug.Log("터치 x :" + X + " y :" + Y);
                    }
                }
                else
                {
                    myOt.lifeTime = 2.0f;
                }

            }

           //  라이다 테스트 기본
            m_vert.Clear();
            for (int i = 0; i < 720; i++)
            {
                m_vert.Add(Quaternion.Euler(0, 0, m_data[i].theta) * Vector3.right * m_data[i].distant*0.01f);

            }
            m_mesh.SetVertices(m_vert);
            m_mesh.SetIndices(m_ind.ToArray(), MeshTopology.Points, 0);

            m_mesh.UploadMeshData(false);
            m_meshfilter.mesh = m_mesh;

            m_datachanged = false;
        }
    }
    public Vector3 Location_Calculation(float x, float y)
    {

        float count_cut = 1.5f;
        double xc = x / count_cut;
        double yc = y / count_cut;

        int x1 = (int)System.Math.Truncate(xc) + 1;
        int y1 = (int)System.Math.Truncate(yc) + 1;

        return new Vector3(x1, y1, 0);

    }

    public void OverListCount()
    {
        if (list_Overlap.Count != 0)
        {
            List<OverlapTouch> DelOver = new List<OverlapTouch>();

            for (int i = 0; i < list_Overlap.Count; i++)
            {
                list_Overlap[i].lifeTime -= 0.1f;

                if (list_Overlap[i].lifeTime <= 0)
                    DelOver.Add(list_Overlap[i]);
            }

            foreach (OverlapTouch item in DelOver)
            {
                list_Overlap.Remove(item);
            }

        }

    }


    // 라이다 테스트 기본
    void GenMesh()
    {
        while (true)
        {
            int datacount = RplidarBinding.GetData(ref m_data);
            if (datacount == 0)
            {
                Thread.Sleep(20);
            }
            else
            {
                m_datachanged = true;
            }
        }
    }
    

}
