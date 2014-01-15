using UnityEngine;
using System.Collections;

public class SoldierSpawn : MonoBehaviour {

	public Transform m_soldier;
	public float m_gentime = 3;

	protected float m_time;
	protected Transform m_transform;

	// Use this for initialization
	void Start () {
		m_transform = this.transform;
		m_time = m_gentime;
	}
	
	// Update is called once per frame
	void Update () {
		m_time -= Time.deltaTime;
		if(m_time < 0)
		{
			m_time = Random.value * m_gentime;
			Instantiate(m_soldier,m_transform.position,Quaternion.identity);
		}
	}
}
