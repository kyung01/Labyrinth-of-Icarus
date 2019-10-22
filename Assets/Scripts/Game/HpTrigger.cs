using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HpTrigger : MonoBehaviour
{
	[SerializeField]
	List<Entity.OWNER_TYPE> targetOwnerTypes;
	[SerializeField]
	List<Entity.ENTITY_TYPE> targetTypes;
	[SerializeField]
	float dealtHPChange;
	[SerializeField]
	float forceApplied;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		var component = collision.gameObject.GetComponentRecursively<Entity>();
		if (component == null) return;
		if (!component.CheckOwnerAndTarget(targetOwnerTypes, targetTypes))
		{
			return;
		}

		component.changeHpBy(dealtHPChange);
		var rigidBody = component.gameObject.GetComponentRecursively<Rigidbody2D>();
		if (rigidBody == null) return;
		var dir = (collision.transform.position - this.transform.position).normalized;
		rigidBody.velocity = Vector2.zero;
		rigidBody.AddForceAtPosition(dir*forceApplied, this.transform.position, ForceMode2D.Impulse);

	}
}

