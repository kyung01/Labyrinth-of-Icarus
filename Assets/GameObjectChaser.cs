using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectChaser : MonoBehaviour
{
	[SerializeField]
	GameObject target;
	public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		this.transform.position += (target.transform.position - this.transform.position) * speed * Time.deltaTime;
    }
}
