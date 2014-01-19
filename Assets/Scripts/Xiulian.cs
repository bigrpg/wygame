using UnityEngine;
using System.Collections;

public class Xiulian : MonoBehaviour {

	GUITexture m_bg_tex;
	// Use this for initialization
	void Start () {
		m_bg_tex = this.transform.FindChild("xiulian_bg").GetComponent<GUITexture>();
	}
	
	// Update is called once per frame
	void Update () {

		bool mouseup = Input.GetMouseButtonUp(0);
		Vector3 mousepos = Input.mousePosition;

		if(mouseup)
		{
			//关闭提示窗口
			if(m_bg_tex.HitTest(mousepos))
			{
				Show(false);
				GameManager.Instance.m_pause = false;
			}
			
		}

	}

	public void Show(bool s)
	{
		this.gameObject.SetActiveRecursively(s);
	}
}
