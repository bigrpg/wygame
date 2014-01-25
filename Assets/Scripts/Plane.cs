using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {

	Renderer render;

	public static Plane Instance;
	// Use this for initialization
	void Start () {
		Instance = this;
		render = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetColor(Color c)
	{
		render.material.color = c;
	}
}