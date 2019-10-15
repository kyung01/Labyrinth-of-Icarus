using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KWorld
{
	public class Room
	{
		public Vector2 position;
		public List<Tile> tiles = new List<Tile>();
		public bool openUP, openRight, openDown, openLeft;

		public Room(Vector2 position)
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
		public void open(bool openUp, bool openRight, bool openDown, bool openLeft)
		{
			this.openUP = openUp;
			this.openLeft = openLeft;
			this.openDown = openDown;
			this.openRight = openRight;

		}
		public void close()
		{
			open(false, false, false, false);
		}
		public bool isAllDoorsClosed()
		{
			return !openUP && !openRight && !openLeft && !openDown;
		}
		public override string ToString()
		{
			return "ID[" + (int)position.x + " , " + (int)position.y +"] "+ openUP + "/" + openRight + "/" + openDown + "/" + openLeft;
		}

	}



}