using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slug : MonoBehaviour
{
	public Vector2 TargetPosition = new Vector2(-10, 0);
	public List<Foot> foots;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
		//scan the surrounding 
		//use unused foots first 
		//unlink lowest performing foor to locate towards better pefroming situations
    }
}
