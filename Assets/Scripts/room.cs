using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KWorld
{
public class Room
{
		public Vector2 position;
		public List<Tile> tile;
		public bool openUP,openRight, openDown, openLeft;
		
		public Room (Vector2 position)
		{
			this.position = position;
		}
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
		public void open(bool openUp,bool openRight, bool openDown,bool openLeft)
		{

		}
		public void close()
		{
			open(false, false, false, false);
		}
}


}