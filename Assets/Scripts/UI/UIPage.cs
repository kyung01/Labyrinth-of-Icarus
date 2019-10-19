using UnityEngine;
using System.Collections;

public class UIPage : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	public void On()
	{
		this.gameObject.SetActive(true);
	}
	public void Off()
	{
		this.gameObject.SetActive(false);
		Debug.Log(this.gameObject.name);
	}
}
