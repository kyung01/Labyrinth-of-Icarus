using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	static readonly float GROUND_HEIGHT = 10;
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
	EntitySmartList<Vein> veinList = new EntitySmartList<Vein>();
	EntitySmartList<Tumor> tumorList = new EntitySmartList<Tumor>();
	EntitySmartList<TumorCore> tumorCoreList = new EntitySmartList<TumorCore>();
	EntitySmartList<Flower> flowerList = new EntitySmartList<Flower>();
	// Use this for initialization
	void instantiate<T>(EntitySmartList<T> list, int count) where T:Entity
	{

		for (int i = 0; i < count; i++)
		{
			var entity = Instantiate(prefabBank.get<T>());
			list.addEntity(entity);
			entity.kill();

		}
	}
	void instantiate<T>(EntitySmartList<T> list, T instance, int count) where T:Entity
	{
		for (int i = 0; i < count; i++)
		{
			var entity = Instantiate(instance);
			list.addEntity(entity);
			entity.kill();

		}
	}
	void Start()
	{
		instantiate<SimpleBullet>(bulletList,prefabBank.simpleBullet,  30);
		instantiate<Seed>(seedList, prefabBank.seed, 30);
		instantiate<Vein>(veinList, prefabBank.vein, 30);
		instantiate<Tumor>(tumorList, prefabBank.tumor, 30);
		instantiate<TumorCore>(tumorCoreList, prefabBank.tumorCore, 30);
		instantiate<Flower>(flowerList, prefabBank.flower, 30);
		for (int i = 0; i < seedList.Count; i++)
		{
			var seed = seedList.getNextEntity();
			seed.evntBloom = hdlSeedBloom;
		}
		for (int i = 0; i < tumorCoreList.Count; i++)
		{
			var tumorCore = tumorCoreList.getNextEntity();
			tumorCore.evntFinishedGrowing = hdlFinishedGrowing;
		}
		for (int i = 0; i < flowerList.Count; i++)
		{
			var flower = flowerList.getNextEntity();
			flower.evntBloom = hdlFlowerBloom;
		}

	}
	void hdlSeedBloom(Seed seed)
	{
		var flower = flowerList.getNextDeadEntity();
		if (flower == null) return;
		flower.transform.position = seed.transform.position;
		flower.transform.rotation = seed.transform.rotation;
		flower.respawn();
	}
	void hdlFlowerBloom(Flower flower)
	{
		float SEED_THROWING_FORCE = 30;
		List<float> angles = new List<float>() { flower.transform.rotation.eulerAngles.z, flower.transform.rotation.eulerAngles.z + Random.Range(-45, 0), flower.transform.rotation.eulerAngles.z + Random.Range(0, 45) };
		for (int i = 0; i < angles.Count; i++)
		{
			//Debug.Log("Angle at " + veinAngles[i]);

			var seed = seedList.getNextDeadEntity();
			if (seed == null) break;
			seed.transform.position = flower.transform.position;
			seed.transform.rotation = Quaternion.Euler(0, 0, angles[i]);
			seed.respawn();
			seed.rigidbody.AddForce(seed.transform.right * SEED_THROWING_FORCE, ForceMode2D.Impulse);
		}
	}
	void hdlSeedBloomVeins(Seed seed)
	{
		List<float> veinAngles = new List<float>() { seed.transform.rotation.eulerAngles.z, seed.transform.rotation.eulerAngles.z + Random.Range(-45, 0), seed.transform.rotation.eulerAngles.z + Random.Range(0, 45) };
		for (int i = 0; i < veinAngles.Count; i++)
		{
			//Debug.Log("Angle at " + veinAngles[i]);

			var newVein = veinList.getNextDeadEntity();
			newVein.transform.position = seed.transform.position;
			newVein.transform.rotation = Quaternion.Euler(0, 0, veinAngles[i]);
			newVein.respawn();
		}
	}
	void hdlFinishedGrowing(TumorCore core)
	{
		//spawn 
		List<Vector3> relativeSpawnPositions = new List<Vector3>();
		if(core.transform.lossyScale.x > 3){
			relativeSpawnPositions.AddRange(new Vector3[] {
				new Vector3(-1.0f, 1.0f, 0), new Vector3(0.0f, 1.0f, 0), new Vector3(1.0f, 1.0f, 0),
				new Vector3(-1.0f, .0f, 0), new Vector3(0.0f, .0f, 0), new Vector3(1.0f, .0f, 0),
				new Vector3(-1.0f, -1.0f, 0), new Vector3(0.0f, -1.0f, 0), new Vector3(1.0f, -1.0f, 0)
			});
		}
		else if (core.transform.lossyScale.x > 2)
		{
			//spawn 4 tumors
			relativeSpawnPositions.AddRange(new Vector3[] { new Vector3(-0.5f, 0.5f, 0), new Vector3(0.5f, 0.5f, 0), new Vector3(-0.5f, -0.5f, 0), new Vector3(0.5f, -0.5f, 0) });
		}
		else if (core.transform.lossyScale.x > 1)
		{
			relativeSpawnPositions.AddRange(new Vector3[] { new Vector3() });
		}
		Vector3 coreScale = core.transform.localScale;
		core.transform.localScale = Vector3.one;
		for (int i = 0; i < relativeSpawnPositions.Count; i++)
		{
			var tumor = tumorList.getNextDeadEntity();
			if (tumor == null) break;
			tumor.transform.parent = core.transform;
			tumor.transform.localRotation = Quaternion.identity;
			tumor.transform.localPosition = relativeSpawnPositions[i];
			tumor.transform.parent = null;
			tumor.respawn();

			var dirForce = (tumor.transform.position - core.transform.position).normalized;
			tumor.rigidbody.AddTorque(Random.Range(-10, 10), ForceMode2D.Impulse);
			tumor.rigidbody.AddForce(dirForce * 15, ForceMode2D.Impulse);

		}
		core.transform.localScale = coreScale;
	}

	// Update is called once per frame
	void Update()
	{
		switch (state) {
			case GAME_STATE.INITIAL:
				//loadLevel00();
				//gameLevel++;
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
		float tumorNumber = 3;
		float seedNumber = 10;
		for (int i = 0; i < tumorNumber; i++)
		{
			var tumor = tumorCoreList.getNextDeadEntity();
			if (tumor == null) return;
			tumor.respawn();

			tumor.transform.position = new Vector3(WORLD_WIDTH / (tumorNumber + 1) * (1 + i), GROUND_HEIGHT + 0.9f * (WORLD_HEIGHT - GROUND_HEIGHT), 0);
			tumor.rigidbody.AddTorque(Random.Range(-10, 10));
			tumor.rigidbody.AddForce(new Vector2(Random.Range(-300, 300), Random.Range(-300, 300)));
		}
		List<Seed> seeds = new List<Seed>();
		for (int i = 0; i < seedNumber; i++)
		{
			var seed = seedList.getNextDeadEntity();
			if (seed == null) return;
			seeds.Add(seed);
			seed.respawn();

			seed.transform.position = new Vector3(WORLD_WIDTH /(seedNumber+1)* (1 + i), GROUND_HEIGHT+0.5f*(WORLD_HEIGHT - GROUND_HEIGHT), 0);
			seed.rigidbody.AddTorque(Random.Range(-10, 10));
			seed.rigidbody.AddForce(new Vector2(Random.Range(-300, 300), Random.Range(-300,300)));
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
