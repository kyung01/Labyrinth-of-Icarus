using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityListManager<T> where T:Entity
{
	List<T> list = new List<T>();
	int currentIndex = 0;

	public void addEntity(T entity)
	{
		list.Add(entity);
	}
	T getNextEntity()
	{
		T entity = list[currentIndex];
		currentIndex = (currentIndex + 1) % list.Count;
		return entity;
	}
	public T getNextDeadEntity()
	{
		for(int i = 0; i < list.Count; i++)
		{
			var entity = getNextEntity();
			if (!entity.IsAlive) return entity;
		}
		return null;
	}
}
public class Game : MonoBehaviour
{
	[SerializeField]
	int BULLET_GENERATION_PREPARATION_COUNT = 10;
	[SerializeField]
	int SEED_GENERATION_PREPARATION_COUNT = 10;
	[SerializeField]
	int TREE_GENERATION_PREPARATION_COUNT = 10;

	[SerializeField]
	PrefabBank prefabBank;
	[SerializeField]
	Player player;
	[SerializeField]
	Transform weaponFiringPosition;
	[SerializeField]
	float weaponFiringRate = 1.0f;
	[SerializeField]
	float weaponFiringForce = 1;
	[SerializeField]
	float weaponFiringTorqueMin = 1;
	[SerializeField]
	float weaponFiringTorqueMax = 1;
	float timeRemainingToFireBullet = 0;
	[SerializeField]
	List<SimpleBullet> playerBullets = new List<SimpleBullet>();
	EntityListManager<SimpleBullet> bulletManager = new EntityListManager<SimpleBullet>();
	int playerBulletIndex = 0;
	// Use this for initialization
	void Start()
	{
		for(int i = 0; i < 30; i++)
		{
			var bullet = Instantiate(prefabBank.playerBullet);
			bulletManager.addEntity(bullet);
			bullet.kill();

		}
	}

	// Update is called once per frame
	void Update()
	{
		timeRemainingToFireBullet -= Time.deltaTime;
		if (Input.GetMouseButton(0))
		{
			//mouse 0 is held fire weapon
			playerFireWeapon();
		}
	}
	void playerFireWeapon()
	{
		if (timeRemainingToFireBullet > 0) return;
		timeRemainingToFireBullet = (1.0f / weaponFiringRate);

		var bullet = bulletManager.getNextDeadEntity();

		bullet.transform.position = weaponFiringPosition.position;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		bullet.transform.LookAt2D(mousePosition);
		bullet.setActive(true);
		bullet.rigidBody.AddForce(new Vector2(mousePosition.x - bullet.transform.position.x, mousePosition.y - bullet.transform.position.y).normalized * weaponFiringForce, ForceMode2D.Impulse);
		bullet.rigidBody.AddTorque(Random.Range(weaponFiringTorqueMin, weaponFiringTorqueMax),ForceMode2D.Impulse);
	}
}
