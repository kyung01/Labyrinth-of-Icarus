using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBall : MonoBehaviour
{
	Rigidbody2D body;	
    // Start is called before the first frame update
    void Start()
    {
		body = GetComponent<Rigidbody2D>();
    }
	private void FixedUpdate()
	{
		body.AddForce(-Physics2D.gravity*Vector2.up* Time.fixedDeltaTime, ForceMode2D.Impulse );
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
