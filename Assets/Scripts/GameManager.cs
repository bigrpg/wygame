using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//玩家数值
	public float m_secret = 0;	//活动隐蔽性
	public float m_power = 0;	//活动能力
	public float m_money = 0;	//金钱
	public int m_support = 0; 	//追随者数量
	public int m_death = 0;		//死亡人数



	GUIText m_moneyText;
	GUIText m_powerText;

	// Use this for initialization
	void Start () {
		m_moneyText = this.transform.FindChild("money").GetComponent<GUIText>();
		m_powerText = this.transform.FindChild("power").GetComponent<GUIText>();

		SetMoney(99999);
		SetPower(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetMoney(int m) {
		m_money = m;
		m_moneyText.text = "金钱:" + m_money.ToString();
	}

	void SetPower(int p) {
		m_power = p;
		m_powerText.text = "活动能力:" + m_power.ToString();

	}
}
