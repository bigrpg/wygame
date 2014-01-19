using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		// 文字大小
		GUI.skin.label.fontSize = 58;
		
		// UI中心对齐
		GUI.skin.label.alignment = TextAnchor.LowerCenter;
		
		// 显示标题
		GUI.Label(new Rect(0, 30, Screen.width, 100), "反抗之战");

		GUI.skin.label.fontSize = 20;
		GUI.skin.button.fontSize = 28;
		// 开始游戏按钮
		if (GUI.Button(new Rect(Screen.width * 0.5f - 100, Screen.height * 0.7f, 200, 50), "开始游戏"))
		{
			// 开始读取下一关
			Application.LoadLevel("level1");
		}
		GUI.skin.button.fontSize = 20;
	}
}
