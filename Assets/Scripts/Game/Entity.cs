using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	public enum OWNER_TYPE { PLAYER, ENEMY };
	public enum ENTITY_TYPE {DEFAULT,BULLET };
	[SerializeField]
	public OWNER_TYPE ownerType;
	[SerializeField]
	public ENTITY_TYPE type;
	[SerializeField]
	float health;
	[SerializeField]
	bool isAlive = true;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	public void kill()
	{
		isAlive = false;
		setActive(false);
	}
	public void changeHpBy(float amount)
	{
		health += amount;
		if(health <= 0)
		{
			kill();
		}
	}
	public virtual void setActive(bool value)
	{
		this.gameObject.SetActive(value);
	}
}
