using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	public float m_speed = 3;
	public float m_life = 5;

	protected Transform m_transform;


	// Use this for initialization
	void Start () {
		m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		m_life -= Time.deltaTime;
		if(m_life<0)
			Destroy(this.gameObject);

		m_transform.Translate(new Vector3(0,0,-m_speed * Time.deltaTime));
	}
}
