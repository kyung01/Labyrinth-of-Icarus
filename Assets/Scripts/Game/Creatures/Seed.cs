using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Once seed comes in contact with World, it dies then turn into a tree
/// </summary>
public class Seed : Entity
{
	public delegate void DEL_BLOOM(Seed seed);
	[SerializeField]
	float timeRequiredToBloom;
	float timeRemainingToBloom;

	public DEL_BLOOM evntBloom;

	// Start is called before the first frame update
	void Start()
    {
		timeRemainingToBloom = timeRequiredToBloom;

	}

    // Update is called once per frame
    void Update()
    {
		timeRemainingToBloom -= Time.deltaTime;
		if (timeRemainingToBloom > 0) return;
		//bloom condtion met


	}
	public virtual void bloom()
	{
		if(evntBloom != null)
			evntBloom(this);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{

		var objectName = collision.gameObject.name;

		if (objectName.Equals("Ground")|| objectName.Equals("Wall Left" ) || objectName.Equals("Wall Right")
			|| objectName.Equals("Ceiling"))
		
		//came in contact with the ground
		bloom();
		
	}
}
