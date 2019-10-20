using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
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
	int playerBulletIndex = 0;
	// Use this for initialization
	void Start()
	{
		for(int i = 0; i < 30; i++)
		{
			var bullet = Instantiate(prefabBank.playerBullet);
			playerBullets.Add(bullet);
			bullet.setActive(false);

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
	float hprGetLookAtAngle(Vector3 origin, Vector3 target)
	{
		var dir = (target - origin).normalized;
		return Mathf.Atan2(dir.y, dir.x)*(180.0f/ Mathf.PI);
	}
	void playerFireWeapon()
	{
		if (timeRemainingToFireBullet > 0) return;
		timeRemainingToFireBullet = (1.0f / weaponFiringRate);

		var bullet = playerBullets[playerBulletIndex];
		playerBulletIndex = (playerBulletIndex + 1) % playerBullets.Count;
		bullet.transform.position = weaponFiringPosition.position;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		bullet.transform.localRotation = Quaternion.Euler(0,0, hprGetLookAtAngle(bullet.transform.position, mousePosition));
		bullet.setActive(true);
		bullet.rigidBody.AddForce(new Vector2(mousePosition.x - bullet.transform.position.x, mousePosition.y - bullet.transform.position.y).normalized * weaponFiringForce, ForceMode2D.Impulse);
		bullet.rigidBody.AddTorque(Random.RandomRange(weaponFiringTorqueMin, weaponFiringTorqueMax),ForceMode2D.Impulse);
	}
}
