using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleRenderer : MonoBehaviour
{
	[SerializeField]
	SpriteRenderer spriteCenter;
	public
	List<Transform> foots;
	public
	List<Transform> joints;
	public
	List<GameObjectChaser> footChasers;
	public
	List<SpriteRenderer> footBrancheSprites;
	// Use this for initialization
	void Start()
	{
	}
	private void Update()
	{
		float hearthBeat = 2+Mathf.Abs(Mathf.Sin(Time.time *5 ) )*1;
		spriteCenter.transform.localScale = new Vector3(hearthBeat, hearthBeat,1);
		spriteCenter.transform.rotation = Quaternion.Euler(0, 0, Time.time*7);
		for (int i = 0;  i< footChasers.Count; i++)
		{
			var foot = footChasers[i];
			if (i >= footBrancheSprites.Count) break;
			SpriteRenderer sprite = footBrancheSprites[i];
			var dis = (foot.transform.position - joints[i].position);
			var dir = dis.normalized;
			sprite.transform.position = joints[i].position + dis * 0.5f;
			var originalParent = sprite.transform.parent;
			sprite.transform.parent = null;
			sprite.transform.localScale = new Vector3(0.5f,dis.magnitude,1);
			sprite.transform.parent = originalParent;
			float angle = Mathf.Atan2(dir.y, dir.x) * (180.0f / Mathf.PI);
			//Debug.Log(i + " " + angle + "  " + Mathf.Atan2(dir.y, dir.x));
			sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90+angle));
		}
		
	}
}
