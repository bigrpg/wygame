using UnityEngine;
using System.Collections;

public class Xiulian : MonoBehaviour {

	GUITexture m_bg_tex;
	GUITexture m_up_secret;
	GUITexture m_up_power;

	// Use this for initialization
	void Start () {
		m_bg_tex = this.transform.FindChild("xiulian_bg").GetComponent<GUITexture>();
		m_up_secret = this.transform.FindChild("for_secret_tex").GetComponent<GUITexture>();
		m_up_power = this.transform.FindChild("for_power_tex").GetComponent<GUITexture>();
	}

	// Update is called once per frame
	void Update () {

		bool mouseup = Input.GetMouseButtonUp(0);
		Vector3 mousepos = Input.mousePosition;

		if(mouseup)
		{
			bool exit = true;
			GameManager inst = GameManager.Instance;
			if(m_up_secret.HitTest(mousepos))
			{
				float needMoney = 15;
				if(inst.m_money >= needMoney)
				{
					inst.m_money -= needMoney;
					inst.SetMoney(inst.m_money);
					inst.m_secret += 5;
					if(inst.m_secret >100)
						inst.m_secret = 100;
					inst.SetSecret(inst.m_secret);
				}
				else{
					exit = false;
				}
			}
			else if(m_up_power.HitTest(mousepos))
			{
				float needMoney = 20;
				if(inst.m_money >= needMoney)
				{
					inst.m_money -= needMoney;
					inst.SetMoney(inst.m_money);
					inst.m_power += 20;
					inst.SetPower(inst.m_power);
				}
				else{
					exit = false;
				}

			}

			//关闭提示窗口
			if(exit && m_bg_tex.HitTest(mousepos))
			{
				Show(false);
				inst.m_pause = false;
			}
			
		}

	}

	public void Show(bool s)
	{
		this.gameObject.SetActiveRecursively(s);
	}
}
