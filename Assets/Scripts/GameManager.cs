using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {

	struct Param
	{
		public float military;	//军队数量
		public float tech;	//科技水平
		public float power;//实力
	};

	const int m_maxArea = 1;
	const float m_vicRatio = 6; //获胜比例

	Param[]  m_param = new Param[m_maxArea*2];
	public int m_currentArea = 0;


	public static GameManager Instance;
	//玩家数值
	public float m_money = 0;	//金钱
	public float m_support = 0; 	//追随者数量
	public float m_death = 0;		//死亡人数

	public float m_powerRatio = 50;	//政府镇压力度[0--100]

	public Transform[] m_baopos;
	//fx
	public Transform m_explosionFX;
	//color
	public Color m_color0;
	public Color m_color1;
	public Color m_color2;
	public float m_colorKey0;
	public float m_colorKey1;
	public float m_colorKey2;



	protected float m_baotime = 5;
	protected float m_minbaogap_time = 5;
	protected float m_delayShowTips = -1;
	protected float m_totaltime = 0;
	public bool  m_pause = false;
	bool m_first = true;

	protected int m_failed = 0;

	//tips
	protected string[] m_msg = {
		"你们的反抗活动被发现了，努力提高你们的<color=red>隐蔽度</color>\n" +
		"可以降低政府的镇压能力",

		"你们的反抗活动在局部取得胜利了，恭喜你们\n" +
		"你们获得了资金支持\n" +
		"消耗金钱来提升军队能力和科技水平将有助你更快获胜\n" +
		"<color=red>注:点击左下角图标提升军队数量和科技水平</color>"
	
	
	};


	GUIText m_moneyLabel;
	GUIText m_supportLabel;
	GUIText m_deathLabel;
	GUITexture m_powerratio_progress_tex;
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
		for(int i=0;i<m_maxArea*2;i+=2)
		{
			m_param[i].military = 10000;
			m_param[i].tech = 100;
			m_param[i].power = 50;
			UpdateParamPower(i);

			m_param[i+1].military = 1000000;
			m_param[i+1].tech = 94; //speed 9.4
			m_param[i+1].power = 100;
			UpdateParamPower(i+1);
		}

		m_moneyLabel = this.transform.FindChild("money_label").GetComponent<GUIText>();
		m_supportLabel = this.transform.FindChild("support_label").GetComponent<GUIText>();
		m_deathLabel = this.transform.FindChild("death_label").GetComponent<GUIText>();
		m_powerratio_progress_tex = this.transform.FindChild("powerratio_progress").GetComponent<GUITexture>();
		m_tipsLabel = this.transform.FindChild("tips").GetComponent<GUIText>();
		m_tipsBg_tex = this.transform.FindChild("tipsBg").GetComponent<GUITexture>();
		m_xiulian_btn = this.transform.FindChild("xiulian_btn").GetComponent<GUITexture>();
		m_xiulian_gui = GameObject.FindGameObjectWithTag("Xiulian").GetComponent<Xiulian>();

		SetMoney(100000);
		SetSupport(1000);
		SetDeath(0);
		SetPowerRatio(0);

		SetTips("");
		m_xiulian_gui.Show(false);
	}
	
	// Update is called once per frame
	void Update () {
	
		bool mouseup = Input.GetMouseButtonUp(0);
		Vector3 mousepos = Input.mousePosition;

		SetPowerRatio(m_powerRatio);

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
				//SetTips (m_msg[1]);
				//m_pause = true;
			}
		}
		else if(m_baotime >0) //赚钱
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
				SetMoney(m_money + 200.0f);//增加100钱
			}
		}

		m_totaltime += Time.deltaTime;


		if(m_totaltime>3 || m_first)  //调整其他数值
		{
			m_totaltime = 0;

			m_support += (Random.value*0.4f) * 1000;
			m_death += (Random.value*0.4f * 100)+5;

			SetSupport(m_support);
			SetDeath(m_death);

			UpdateParam(m_currentArea); //民众
			UpdateParam(m_currentArea+1); //政府

			UpdateParamPower(m_currentArea);
			UpdateParamPower(m_currentArea+1);

			float percent = CalcAreaPowerRatio(m_currentArea);
			SetPowerRatio(percent*100);
			m_first = false;
		}

		if(m_first)
			return;

		if(m_powerRatio >= m_vicRatio*100/(m_vicRatio+1))
		{
			m_pause = true;
			m_failed = 2;
			m_delayShowTips = 0;
			SetTips("");
			m_xiulian_gui.Show(false);
		}
		else if(m_powerRatio <= 100/(m_vicRatio+1))
		{
			m_pause = true;
			m_failed = 1;
			m_delayShowTips = 0;
			SetTips("");
			m_xiulian_gui.Show(false);
		}
	}

	public void SetMoney(float m) {
		m_money = m;
		m_moneyLabel.text = "金钱:" + ((int)(m_money)).ToString();
	}

	//对当前的Area修改军队数量
	public void AddMilitary(float delta)
	{
		m_param[m_currentArea].military += delta;
	}

	public void AddTech(float delta)
	{
		m_param[m_currentArea].tech += delta;
	}

	public void SetSupport(float s) {
		m_support = s;
		m_supportLabel.text = "追随者:" + ((int)m_support).ToString();
	}

	public void SetDeath(float s) {
		m_death = s;
		m_deathLabel.text = "伤亡:" + ((int)m_death).ToString();
	}

	public void SetPowerRatio(float p) {
		m_powerRatio = p;
		float p2 = p*(m_vicRatio+1)/(m_vicRatio);
		m_powerratio_progress_tex.pixelInset=new Rect(0,-16,256*p2/100,16);
		if(Plane.Instance)
			Plane.Instance.SetColor(GetWorldColor());
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

	//根据科技水平提升军队数量
	void UpdateParam(int i)
	{
		//m_param[i].military += 1000 * m_param[i].tech;
		if(i%2 ==1)
			m_param[i].tech += 1;
	}

	//根据军队数量和科技水平计算实力
	void UpdateParamPower(int i)
	{
		m_param[i].power += (float)(System.Math.Sqrt(m_param[i].military)) * m_param[i].tech/10000;
	}

	float CalcAreaPowerRatio(int i)
	{
		float powerPeople = m_param[i].power;
		float powerGov = m_param[i+1].power;

		float percent = powerPeople/(powerGov+powerPeople);
		return percent;
	}

	Color GetWorldColor()
	{
		if(m_powerRatio <= m_colorKey0)
			return m_color0;
		if(m_powerRatio<= m_colorKey1)
		{
			float lerp = (m_powerRatio - m_colorKey0)/(m_colorKey1-m_colorKey0);
			return Color.Lerp(m_color0,m_color1,lerp);
		}
		if(m_powerRatio <= m_colorKey2)
		{
			float lerp = (m_powerRatio - m_colorKey1)/(m_colorKey2-m_colorKey1);
			return Color.Lerp(m_color1,m_color2,lerp);
		}
		return m_color2;
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
