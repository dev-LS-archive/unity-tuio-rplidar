using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;


[RequireComponent(typeof(MeshFilter))]
public class RplidarMap : MonoBehaviour
{

    public float slowCount;

    public bool m_onscan = false;
    //public bool m_onscan2 = false;

    private LidarData[] m_data;
    //private LidarDataLeft[] m_data_left;

    public string COM = "COM3";
    //public string COM2 = "COM4";

    public Mesh m_mesh;

    public int startNum;

    public List<Vector3> m_vert;
    //public List<Vector3> m_vert_left;

    public List<OverlapTouch> list_Overlap;

    public float deltime;
    public float currentDeltime;

    private MeshFilter m_meshfilter;

    private Thread m_thread;
    private bool m_datachanged = false;

    public Vector3 lastTouch;

    public MouseControl MC;

    #region 중복터치체크
    public class OverlapTouch
    {
        public int x;
        public int y;
        public float lifeTime;
    }
    #endregion
    void Start()
    {
        deltime = 1;
        currentDeltime = 1;
        m_meshfilter = GetComponent<MeshFilter>();

        m_data = new LidarData[720];
        //m_data_left = new LidarDataLeft[720];

        m_vert = new List<Vector3>();
        //m_vert_left = new List<Vector3>();

        list_Overlap = new List<OverlapTouch>();

        // m_mesh = new Mesh();
        // m_mesh.MarkDynamic();

        RplidarBinding.OnConnect(COM);
        //RplidarBindingLeft.OnConnect(COM2);

        RplidarBinding.StartMotor();
        m_onscan = RplidarBinding.StartScan();

        //RplidarBindingLeft.StartMotor();
        //m_onscan2 = RplidarBindingLeft.StartScan();

        if (m_onscan)// && m_onscan2)
        {
            m_thread = new Thread(GenMesh);
            m_thread.Start();
        }
        //처음위치추가
    }

    void OnDestroy()
    {
        m_thread.Abort();

        RplidarBinding.EndScan();
        RplidarBinding.EndMotor();
        RplidarBinding.OnDisconnect();
        RplidarBinding.ReleaseDrive();

        RplidarBindingLeft.EndScan();
        RplidarBindingLeft.EndMotor();
        RplidarBindingLeft.OnDisconnect();
        RplidarBindingLeft.ReleaseDrive();

        m_onscan = false;
    }

    void Update()
    {
        if (slowCount < 0.1f)
        {
            slowCount += Time.deltaTime;
        }
        else
        {
            slowCount = 0;
            Scan_Lidar();
            OverListCount();
        }
    }

    public void Scan_Lidar()
    {

        if (m_datachanged)
        {
            if (startNum < 1)
            {
                startNum++;
            }
            else
            {
                m_vert.Clear();
                //m_vert_left.Clear();

                for (int i = 0; i < 720; i++)
                {

                    m_vert.Add(Quaternion.Euler(0, 0, m_data[i].theta) * Vector3.right * m_data[i].distant * 0.01f);
                    //m_vert_left.Add(Quaternion.Euler(0, 0, m_data_left[i].theta) * Vector3.right * m_data_left[i].distant * 0.01f);

                    #region 스크린사이즈 예외 조정
                    /*
                    if (m_vert[i].x < 7.02f || m_vert[i].x > 30 )
                        continue;

                    if (m_vert[i].y < -7.5f || m_vert[i].y > 9.5f)
                        continue;
                    */
                    #endregion

                    //터치판정부----------------------------------------------
                    Vector3 myVec = Location_Calculation(m_vert[i].x, m_vert[i].y);
                    //Vector3 myVec_left = Location_Calculation(m_vert_left[i].x, m_vert_left[i].y);


                    //오른쪽
                    OverlapTouch myOt = list_Overlap.Find(OverlapTouch => OverlapTouch.x == myVec.x && OverlapTouch.y == myVec.y);

                    if (myOt == null)
                    {
                        OverlapTouch OT = new OverlapTouch();
                        OT.lifeTime = 1.5f;
                        OT.x = (int)myVec.x;
                        OT.y = (int)myVec.y;
                        list_Overlap.Add(OT);
                     //   Debug.Log("리스트크기 오른쪽 : " + list_Overlap.Count + "지금 좌표 " + m_vert[i]);

                        int X = (int)((m_vert[i].y) * 56.47f) + 950;
                        int Y = (int)((m_vert[i].x - 7) * 46.95f - 10);

                        if (m_vert[i].y < 0)
                        {
                            X = (int)((Math.Abs(m_vert[i].y) + 9.5f) * 56.47f) + 950;
                        }
                        else
                        {
                            X = (int)((Math.Abs(m_vert[i].y - 9.5f)) * 56.47f) + 950;
                        }

                        if (!(m_vert[i].x < 7.02f || m_vert[i].x > 30 || m_vert[i].y < -9.5f || m_vert[i].y > 9.5f))
                        {

                            Vector3 vec = new Vector3(X,Y,0);

                            MC.myQueue.Enqueue(vec);

                          //  MC.MC_Move(X, Y);
                          //  Debug.Log("터치 x :" + X + " y :" + Y);
                        }

                    }
                    else
                    {
                        myOt.lifeTime = 2.0f;
                    }


                    //왼쪽
                    //OverlapTouch myOt_left = list_Overlap.Find(OverlapTouch => OverlapTouch.x == myVec_left.x && OverlapTouch.y == myVec_left.y);
/*
                    if (myOt_left == null)
                    {

                        OverlapTouch OT_left = new OverlapTouch();
                        OT_left.lifeTime = 1.5f;
                        OT_left.x = (int)myVec_left.x;
                        OT_left.y = (int)myVec_left.y;

                        list_Overlap.Add(OT_left);

                      //  Debug.Log("리스트크기  왼쪽: " + list_Overlap.Count + "지금 좌표 " + m_vert_left[i]);

                        int X2 = (int)((m_vert_left[i].y) * 56.47f);
                        int Y2 = (int)((m_vert_left[i].x - 7) * 46.95f - 10);

                        if (m_vert_left[i].y < 0)
                        {
                            X2 = (int)((Math.Abs(m_vert_left[i].y) + 9.5f) * 56.47f);
                        }
                        else
                        {
                            X2 = (int)((Math.Abs(m_vert_left[i].y - 9.5f)) * 56.47f);
                        }


                        if (!(m_vert_left[i].x < 7.02f || m_vert_left[i].x > 30 || m_vert_left[i].y < -9.5f || m_vert_left[i].y > 9.5f))
                        {

                            Vector3 vec = new Vector3(X2, Y2, 0);

                            MC.myQueue.Enqueue(vec);

                            //MC.MC_Move(X2, Y2);
                            // Debug.Log("터치 x :" + X2 + " y :" + Y2);
                        }

                       

                    }
                    else
                    {
                        myOt_left.lifeTime = 2.0f;
                    }
                    */
                    #region 터치보정보류한거
                    if (i > 0)
                    {
                        if (Vector3.Distance(m_vert[i], m_vert[i - 1]) < 0.5f && Vector3.Distance(m_vert[i], m_vert[i - 1]) != 0)//하나씩튀는걸 걸러줌
                        {


                        }

                    }
                    #endregion
                }
                #region 유니티 메쉬로 시각표현
                /*
                m_mesh.SetVertices(m_vert);
                m_mesh.SetIndices(m_ind.ToArray(), MeshTopology.Points, 0);

                m_mesh.UploadMeshData(false);
                m_meshfilter.mesh = m_mesh;*/

                m_datachanged = false;
                #endregion
            }
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

    void GenMesh()
    {
        while (true)
        {

            int datacount = RplidarBinding.GetData(ref m_data);

            //int datacount2 = RplidarBindingLeft.GetData(ref m_data_left);

            if (datacount == 0)// || datacount2 == 0)
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
