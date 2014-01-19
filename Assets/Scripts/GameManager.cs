using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	//玩家数值
	public float m_secret = 100;	//军队能力
	public float m_power = 0;	//科技水平
	public float m_money = 0;	//金钱
	public float m_support = 0; 	//追随者数量
	public float m_death = 0;		//死亡人数

	public float m_putdown = 0;	//政府镇压力度[0--100]

	public Transform[] m_baopos;
	//fx
	public Transform m_explosionFX;

	protected float m_baotime = 5;
	protected float m_minbaogap_time = 5;
	protected float m_delayShowTips = -1;
	protected float m_totaltime = 0;
	public bool  m_pause = false;

	protected int m_failed = 0;

	//tips
	protected string[] m_msg = {
		"你们的反抗活动被发现了，努力提高你们的<color=red>隐蔽度</color>\n" +
		"可以降低政府的镇压能力",

		"你们的反抗活动在局部取得胜利了，恭喜你们\n" +
		"你们获得了资金支持\n" +
		"消耗金钱来提升军队能力和科技水平将有助你更快获胜\n" +
		"<color=red>注:点击左下角图标提升军队能力和科技水平</color>"
	
	
	};


	GUIText m_moneyLabel;
	GUIText m_powerLabel;
	GUIText m_secretLabel;
	GUIText m_supportLabel;
	GUIText m_deathLabel;
	GUITexture m_putdown_progress_tex;
	GUITexture m_xiulian_btn;


	//tips
	GUIText m_tipsLabel;
	GUITexture m_tipsBg_tex;

	Xiulian m_xiulian_gui;

	void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {

		//this.gameObject.SetActiveRecursively(false);

		m_moneyLabel = this.transform.FindChild("money_label").GetComponent<GUIText>();
		m_powerLabel = this.transform.FindChild("power_label").GetComponent<GUIText>();
		m_secretLabel = this.transform.FindChild("secret_label").GetComponent<GUIText>();
		m_supportLabel = this.transform.FindChild("support_label").GetComponent<GUIText>();
		m_deathLabel = this.transform.FindChild("death_label").GetComponent<GUIText>();
		m_putdown_progress_tex = this.transform.FindChild("putdown_progress").GetComponent<GUITexture>();
		m_tipsLabel = this.transform.FindChild("tips").GetComponent<GUIText>();
		m_tipsBg_tex = this.transform.FindChild("tipsBg").GetComponent<GUITexture>();
		m_xiulian_btn = this.transform.FindChild("xiulian_btn").GetComponent<GUITexture>();
		m_xiulian_gui = GameObject.FindGameObjectWithTag("Xiulian").GetComponent<Xiulian>();

		SetMoney(10000);
		SetPower(100);
		SetSecret(100);
		SetSupport(1000);
		SetDeath(0);
		SetPutDown(0);

		SetTips("");
		m_xiulian_gui.Show(false);
	}
	
	// Update is called once per frame
	void Update () {
	
		bool mouseup = Input.GetMouseButtonUp(0);
		Vector3 mousepos = Input.mousePosition;


		if(mouseup)
		{
			//关闭提示窗口
			if(m_tipsBg_tex.HitTest(mousepos))
			{
				SetTips("");
				m_pause = false;
			}
			else if(!m_pause && m_xiulian_btn.HitTest(mousepos))
			{
				m_xiulian_gui.Show(true);
				m_pause = true;
			}
		}


		if(m_pause)
		{
			return;
		}
		//有需要等待的事件
		else if(m_delayShowTips > 0)
		{
			m_delayShowTips -= Time.deltaTime;
			if(m_delayShowTips<0)
			{
				SetTips (m_msg[1]);
				m_pause = true;
			}
		}
		else if(m_baotime >0)
		{

			m_baotime -= Time.deltaTime;
			if(m_baotime < 0)
			{
				int index = m_baopos.Length;
				index = Random.Range(0,index);
				if(index >= m_baopos.Length)
					index = 0;
				m_baotime = Random.value * 5 + m_minbaogap_time;
				Instantiate(m_explosionFX,m_baopos[index].position,Quaternion.identity);
				m_delayShowTips = 1;
				SetMoney(m_money + 100.0f);//增加100钱
			}
		}

		m_totaltime += Time.deltaTime;


		if(m_totaltime>3)  //调整其他数值
		{
			//m_secret = m_secret + 5 * Random.value;
			//if(m_secret <0)
			//	m_secret = 0;
			//SetSecret(m_secret);

			m_putdown = m_putdown + 3+2*Random.value;
			if(m_putdown >100)
			{
				m_putdown = 100;
			}
			SetPutDown(m_putdown);

			m_totaltime = 0;

			m_support += (Random.value*0.4f) * m_support;
			m_death += (Random.value*0.4f * m_death)+5;

			SetSupport(m_support);
			SetDeath(m_death);

		}

		if(m_putdown ==100)
		{
			m_pause = true;
			m_failed = 1;
			m_delayShowTips = 0;
			SetTips("");
			m_xiulian_gui.Show(false);
		}
		else if(m_support >= 10000000)
		{
			m_pause = true;
			m_failed = 2;
			m_delayShowTips = 0;
			SetTips("");
			m_xiulian_gui.Show(false);
		}
	}

	public void SetMoney(float m) {
		m_money = m;
		m_moneyLabel.text = "金钱:" + ((int)(m_money)).ToString();
	}

	public void SetPower(float p) {
		m_power = p;
		m_powerLabel.text = "科技水平:" + ((int)m_power).ToString();

	}

	public void SetSecret(float s) {
		m_secret = s;
		m_secretLabel.text = "军队能力:" + ((int)m_secret).ToString();
	}

	public void SetSupport(float s) {
		m_support = s;
		m_supportLabel.text = "追随者:" + ((int)m_support).ToString();
	}

	public void SetDeath(float s) {
		m_death = s;
		m_deathLabel.text = "伤亡:" + ((int)m_death).ToString();
	}

	public void SetPutDown(float p) {
		m_putdown = p;
		m_putdown_progress_tex.pixelInset=new Rect(0,-16,256*m_putdown/100,16);
	}

	public void SetTips(string  s) {
		m_tipsLabel.text = s;
		if( s.CompareTo("") == 0)
		{
			m_tipsLabel.enabled = false;
			m_tipsBg_tex.enabled = false;
		}
		else
		{
			m_tipsLabel.enabled = true;
			m_tipsBg_tex.enabled = true;
		}
	}
	 
	void OnGUI()
	{
		if(m_failed == 0)
			return;

		GUI.skin.label.fontSize = 50;
		
		// 显示游戏失败
		GUI.skin.label.alignment = TextAnchor.LowerCenter;
		GUI.Label(new Rect(0, Screen.height * 0.2f, Screen.width, 60), m_failed == 1 ? "游戏失败" : "取得胜利");
		
		GUI.skin.label.fontSize = 20;
		GUI.skin.button.fontSize = 28;
		// 显示按钮
		if (GUI.Button(new Rect(Screen.width * 0.5f - 100, Screen.height * 0.5f, 200, 30), "再玩一次"))
		{
			// 读取当前关卡
			Application.LoadLevel(Application.loadedLevelName);
		}
		GUI.skin.button.fontSize = 20;
	}
}
