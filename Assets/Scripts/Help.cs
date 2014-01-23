using UnityEngine;
using System.Collections;

public class Help : MonoBehaviour {

	GUIText m_tips;

	string m_helpstr = 
		"民众的反抗在不断加剧，你们要\n" +
		"扩充你们的军队数量来抗衡法西\n" +
		"斯政府。点击左下角的图标来提升\n" +
		"科技水平，这样军队的数量就会增\n" +
		"加的更快。你也可以直接增加军队\n" +
		"数量。军队数量和科技水平的增加\n" +
		"可以大大扩充你们的实力，只有当\n" +
		"你们的实力与法西斯政府的实力比\n" +
		"值达到一定值才能获胜。左上角的\n" +
		"进度条表示比例值，进度条到100\n" +
		"表示达到胜利条件。若比值太小，\n" +
		"你们就输了。\n" +
		"战斗起来吧，不愿做奴隶的人们！";
	

	// Use this for initialization
	void Start () {
		m_tips = this.transform.FindChild("tips").GetComponent<GUIText>();	
		m_tips.text = m_helpstr;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
