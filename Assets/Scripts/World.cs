using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KWorld {

	public class World : MonoBehaviour
	{
		List<List<Tile>> tiles = new List<List<Tile>>();
		List<List<Room>> rooms = new List<List<Room>>();


		void init() {
			for(int i = 0; i < 100; i ++ ){
				var tileList = new List<Tile>();
				for(int j = 0; j < 100; j ++)
				{
					tileList.Add(new Tile());
				}
				tiles.Add(tileList);
			}
			for(int i = 0; i < 4; i++)
			{

				var rroomList = new List<Room>();
				for(int j = 0; j < 4; j++)
				{
					rroomList.Add(new Room( new Vector2(i,j) ));
				}
			}
			for(int i = 0; i < 4; i++)
			{
				for(int j = 0; j < 4; j++)
				{
					for(int x = 0; x <20; x++)
					{
						for(int y= 0; y < 20; y++)
						{

							rooms[i][j].tile.Add(tiles[20 * i + y][20 * j + x]);
						}
					}
				}
			}
			
		}
		bool hprIsRoomEndRoomFloor(Room room)
		{
			return false;
		}
		bool hprIsPositionCorrectRoomPosition(Vector2 position)
		{
			return !(position.x < 0.0f || position.y < 0.0f ||
				position.y >= rooms.Count || position.x >= rooms[(int)position.y].Count
				);
		}
		void getMaze()
		{
			for (int i = 0; i< 4; i++)
			{
				for(int j = 0; j < 4; j++)
				{
					rooms[i][j].close();
				}
			}
			int selectedRoomIndex = Random.Range(0, 4);
			var selectedRoom = rooms[3][selectedRoomIndex];
			selectedRoom.open(false,true,true,true);
			do {
				var movementList = new List<Vector2>();
				if (selectedRoom.openLeft) movementList.Add(new Vector2(-1, 0));
				if (selectedRoom.openRight) movementList.Add(new Vector2(1, 0));
				if (selectedRoom.openDown) movementList.Add(new Vector2(0, -1));
				Vector2 newSelectedRoomPoision = Vector2.zero;
				int randomChosenPath = Random.Range(1, 100) % movementList.Count;
				bool newRoomIsChosen = false;
				for (int i = 0; i <3; i++)
				{
					newSelectedRoomPoision = selectedRoom.position+  movementList[randomChosenPath];
					if (hprIsPositionCorrectRoomPosition(newSelectedRoomPoision))
					{
						newRoomIsChosen = true;
						break;
					}
				}
				if (newRoomIsChosen)
				{
					selectedRoom = rooms[(int)newSelectedRoomPoision.y][(int)newSelectedRoomPoision.x];

				}
				else
				{
					//chooinsg a room is failed 
					Debug.Log("Map creaion failed ");
					break;
				}
			} while (hprIsRoomEndRoomFloor(selectedRoom));
		}
		private void Awake()
		{
			init();
			getMaze();
			
		}
		
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
	}

}
