using UnityEngine;
using System.Collections;

public class Xiulian : MonoBehaviour {

	GUITexture m_bg_tex;
	GUITexture m_up_tech;
	GUITexture m_up_military;

	// Use this for initialization
	void Start () {
		m_bg_tex = this.transform.FindChild("xiulian_bg").GetComponent<GUITexture>();
		m_up_tech = this.transform.FindChild("for_tech_tex").GetComponent<GUITexture>();
		m_up_military = this.transform.FindChild("for_military_tex").GetComponent<GUITexture>();
	}

	// Update is called once per frame
	void Update () {

		bool mouseup = Input.GetMouseButtonUp(0);
		Vector3 mousepos = Input.mousePosition;

		if(mouseup)
		{
			bool exit = true;
			GameManager inst = GameManager.Instance;
			if(m_up_tech.HitTest(mousepos))
			{
				float needMoney = 500;
				if(inst.m_money >= needMoney)
				{
					inst.m_money -= needMoney;
					inst.SetMoney(inst.m_money);
					//inst.m_secret += 5*Random.value;
					//if(inst.m_secret >100)
					//	inst.m_secret = 100;
					//inst.SetSecret(inst.m_secret);
					//AdjustPutdown(-20);
					inst.AddTech(50*Random.value);
				}
				else{
					exit = false;
				}
			}
			else if(m_up_military.HitTest(mousepos))
			{
				float needMoney = 500;
				if(inst.m_money >= needMoney)
				{
					inst.m_money -= needMoney;
					inst.SetMoney(inst.m_money);
					//inst.m_power += 20*Random.value;
					//inst.SetPower(inst.m_power);
					//AdjustPutdown(-20);
					inst.AddMilitary(2000*Random.value);
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

//	void AdjustPutdown(float pd)
//	{
//		GameManager inst = GameManager.Instance;
//		float putdown = inst.m_putdown;
//		putdown += pd;
//		if(putdown<0)
//			putdown = 0;
//		if(putdown > 100)
//			putdown = 100;
//		inst.SetPutDown(putdown);
//	}
}
