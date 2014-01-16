using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//玩家数值
	public float m_secret = 0;	//活动隐蔽性[0--100]
	public float m_power = 0;	//活动能力[0--100]
	public float m_money = 0;	//金钱
	public int m_support = 0; 	//追随者数量
	public int m_death = 0;		//死亡人数

	public int m_putdown = 0;	//政府镇压力度[0--100]



	GUIText m_moneyLabel;
	GUIText m_powerLabel;
	GUIText m_secretLabel;
	GUIText m_supportLabel;
	GUIText m_deathLabel;
	GUITexture m_putdown_progress_tex;

	// Use this for initialization
	void Start () {
		m_moneyLabel = this.transform.FindChild("money_label").GetComponent<GUIText>();
		m_powerLabel = this.transform.FindChild("power_label").GetComponent<GUIText>();
		m_secretLabel = this.transform.FindChild("secret_label").GetComponent<GUIText>();
		m_supportLabel = this.transform.FindChild("support_label").GetComponent<GUIText>();
		m_deathLabel = this.transform.FindChild("death_label").GetComponent<GUIText>();
		m_putdown_progress_tex = this.transform.FindChild("putdown_progress").GetComponent<GUITexture>();

		SetMoney(99999);
		SetPower(1);
		SetSecret(100);
		SetSupport(0);
		SetDeath(0);
		SetPutDown(20);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetMoney(int m) {
		m_money = m;
		m_moneyLabel.text = "金钱:" + m_money.ToString();
	}

	void SetPower(int p) {
		m_power = p;
		m_powerLabel.text = "斗争力:" + m_power.ToString();

	}

	void SetSecret(int s) {
		m_secret = s;
		m_secretLabel.text = "隐蔽度:" + m_secret.ToString();
	}

	void SetSupport(int s) {
		m_support = s;
		m_supportLabel.text = "追随者:" + m_support.ToString();
	}

	void SetDeath(int s) {
		m_death = s;
		m_deathLabel.text = "伤亡:" + m_death.ToString();
	}

	void SetPutDown(int p) {
		m_putdown = p;
		m_putdown_progress_tex.pixelInset=new Rect(0,-16,128*m_putdown/100,16);
	}
}
