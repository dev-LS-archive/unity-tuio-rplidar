using UnityEngine;

namespace InteractiveWater2D
{
	[RequireComponent(typeof(Camera))]
	public class CameraEffect : MonoBehaviour
	{
		public Material m_Mat;
		[Range(0.9f, 0.999f)]  public float m_Damping = 0.99f;
		[Range(1.5f, 6f)]  public float m_ClickForce = 1.5f;
		public float m_ClickForceDecrease = 1f;
		float m_MouseX = 0f;
		float m_MouseY = 0f;
		RenderTexture m_RTTemp1;
		RenderTexture m_RTTemp2;
		[Header("Rain Drop")]
		public bool m_RainDrop = false;
		[Range(0.01f, 0.1f)] public float m_RainDropDelta = 0.1f;

		void GenerateARipple(float x, float y)
		{
			m_Mat.SetVector("_Center", new Vector4(x, y, Screen.width, Screen.height));
			m_Mat.SetFloat("_ForceMax", m_ClickForce);
			m_Mat.SetFloat("_ForceMin", m_ClickForce - m_ClickForceDecrease);
			m_Mat.SetInt("_MousePress", 1);
		}
		void Start()
		{
			m_MouseX = m_MouseY = 0.5f;
			if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBFloat))
			{
				m_RTTemp1 = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBFloat);
				m_RTTemp2 = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBFloat);
			}
			else if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32))
			{
				m_RTTemp1 = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
				m_RTTemp2 = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
			}
			m_RTTemp1.filterMode = FilterMode.Point;
			m_RTTemp2.filterMode = FilterMode.Point;
			InvokeRepeating ("DoRainDrop", 0f, m_RainDropDelta);
		}
		void OnDestroy()
		{
			RenderTexture.ReleaseTemporary(m_RTTemp1);
			RenderTexture.ReleaseTemporary(m_RTTemp2);
		}
		void Update()
		{
			if (Input.GetMouseButton(0))
			{
				m_MouseX = Input.mousePosition.x / Screen.width;
				m_MouseY = Input.mousePosition.y / Screen.height;
				GenerateARipple(m_MouseX, m_MouseY);
			}
			else
			{
				m_Mat.SetInt("_MousePress", 0);
			}
			m_Mat.SetFloat("_Damping", m_Damping);
		}
		void DoRainDrop()
		{
			if (m_RainDrop)
			{
				float x = Random.Range(0.01f, 0.99f);
				float y = Random.Range(0.01f, 0.99f);
				GenerateARipple(x, y);
			}
		}
		void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			if (Time.frameCount < 3)  // this is a dirty hack way to init render buffer
			{
				Graphics.Blit(null, m_RTTemp1, m_Mat, 2);
				Graphics.Blit(null, m_RTTemp2, m_Mat, 2);
				Graphics.Blit(src, dst);
				return;
			}
			if (Time.frameCount % 2 == 0)
			{
				m_Mat.SetTexture("_ColorTex", src);
				Graphics.Blit(m_RTTemp1, m_RTTemp2, m_Mat, 0);
				Graphics.Blit(m_RTTemp2, dst, m_Mat, 1);
			}
			else
			{
				m_Mat.SetTexture("_ColorTex", src);
				Graphics.Blit(m_RTTemp2, m_RTTemp1, m_Mat, 0);
				Graphics.Blit(m_RTTemp1, dst, m_Mat, 1);
			}
		}
	}
}
