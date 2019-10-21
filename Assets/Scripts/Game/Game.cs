using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntitySmartList<T> where T:Entity
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
public class GameLevel {

	public static void LoadLevel00()
	{

	}

	public static void LoadLevel01()
	{

	}
}

public class Game : MonoBehaviour
{
	static readonly float WORLD_WIDTH = 50;
	static readonly float WORLD_HEIGHT = 30;
	static readonly float LAND_HEIGHT = 10;
	enum GAME_STATE {INITIAL, PLAYING,COMPLETED };

	GAME_STATE state = GAME_STATE.INITIAL;

	int gameLevel = 0;

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


	EntitySmartList<SimpleBullet> bulletList = new EntitySmartList<SimpleBullet>();
	EntitySmartList<Seed> seedList = new EntitySmartList<Seed>();
	EntitySmartList<Vein> treeList = new EntitySmartList<Vein>();
	// Use this for initialization
	void Start()
	{
		for (int i = 0; i < 30; i++)
		{
			var bullet = Instantiate(prefabBank.playerBullet);
			bulletList.addEntity(bullet);
			bullet.kill();

		}
		for (int i = 0; i < 10; i++)
		{
			var seed = Instantiate(prefabBank.seed);
			seed.evntBloom = hdlSeedBloom;
			seedList.addEntity(seed);
			seed.kill();

		}
		for (int i = 0; i < 10; i++)
		{
			var tree = Instantiate(prefabBank.tree);
			treeList.addEntity(tree);
			tree.kill();

		}
	}
	void hdlSeedBloom(Seed seed)
	{
		//kill the seed first
		seed.kill();
		var newTree = treeList.getNextDeadEntity();
		newTree.transform.position = seed.transform.position;
		newTree.respawn();


	}

	// Update is called once per frame
	void Update()
	{
		switch (state) {
			case GAME_STATE.INITIAL:
				loadLevel00();
				gameLevel++;
				state = GAME_STATE.PLAYING;
				break;
			case GAME_STATE.PLAYING:
				break;
			case GAME_STATE.COMPLETED:
				break;
		}

		
		timeRemainingToFireBullet -= Time.deltaTime;
		if (Input.GetMouseButton(0))
		{
			//mouse 0 is held fire weapon
			playerFireWeapon();
		}
	}
	void loadLevel00()
	{
		//three seeds
		var seed_1 = seedList.getNextDeadEntity();
		var seed_2 = seedList.getNextDeadEntity();
		var seed_3 = seedList.getNextDeadEntity();
		seed_1.respawn();
		seed_2.respawn();
		seed_3.respawn();
		Seed[] seeds = { seed_1 , seed_2, seed_3};
		for(int i = 0; i < 3; i++)
		{
			seeds[i].transform.position = new Vector3(WORLD_WIDTH / 4.0f * (1 + i), (WORLD_HEIGHT-1 ), 0);
		}
		



	}
	void playerFireWeapon()
	{
		if (timeRemainingToFireBullet > 0) return;
		timeRemainingToFireBullet = (1.0f / weaponFiringRate);

		var bullet = bulletList.getNextDeadEntity();

		bullet.transform.position = weaponFiringPosition.position;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		bullet.transform.LookAt2D(mousePosition);
		bullet.setActive(true);
		bullet.rigidBody.AddForce(new Vector2(mousePosition.x - bullet.transform.position.x, mousePosition.y - bullet.transform.position.y).normalized * weaponFiringForce, ForceMode2D.Impulse);
		bullet.rigidBody.AddTorque(Random.Range(weaponFiringTorqueMin, weaponFiringTorqueMax),ForceMode2D.Impulse);
	}
}
