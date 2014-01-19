using UnityEngine;
using System.Collections;

public class Help : MonoBehaviour {

	GUIText m_tips;

	string m_helpstr = 
		"民众的不满情绪在不断扩散，\n" +
		"你们的支持者越来越多，达到\n" +
		"1000万时就胜利了。但是政府\n" +
		"不会善罢甘休，当镇压活动达\n" +
		"到100你就失败了。点击左下方\n" +
		"的图标升级反抗能力和隐蔽度\n" +
		"来削弱政府的镇压活动。请注\n" +
		"意左上角的红色进度条，它就\n" +
		"表示镇压度。通过反抗活动的\n" +
		"局部胜利你们可以获得金钱，\n" +
		"这可以用来提升你的能力.\n" +
		"起来吧，不愿做奴隶的人们！";

	// Use this for initialization
	void Start () {
		m_tips = this.transform.FindChild("tips").GetComponent<GUIText>();	
		m_tips.text = m_helpstr;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
