using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
	public delegate void DelEntity(Entity self);

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
	[SerializeField]
	public Rigidbody2D rigidbody;
	public DelEntity evntKill;

	public bool IsAlive
	{
		get { return isAlive; }
		set
		{
			this.isAlive = value;
			if (value)
			{
				setActive(true);
			}
			else
			{
				setActive(false);
			}
		}
	}
	public virtual void respawn()
	{
		IsAlive = true;
	}
	// Use this for initialization
	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	public virtual void kill()
	{
		isAlive = false;
		if(evntKill!= null)
		{
			evntKill(this);
		}
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
