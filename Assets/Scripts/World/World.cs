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

				var roomList = new List<Room>();
				for(int j = 0; j < 4; j++)
				{
					roomList.Add(new Room( new Vector2(j,i) ));
				}
				rooms.Add(roomList);
			}


			for(int i = 0; i < 4; i++)
			{
				for(int j = 0; j < 4; j++)
				{
					for(int x = 0; x <20; x++)
					{
						for(int y= 0; y < 20; y++)
						{
							//Debug.Log(""+rooms[i][j].tiles+ 20 * i + y + " , " + 20 * j + x + " , " + tiles[20 * i + y][20 * j + x]);
							rooms[i][j].tiles.Add(tiles[20 * i + y][20 * j + x]);
						}
					}
				}
			}
			
		}
		bool isValidRoomConnectionPosition(Vector2 roomPosition)
		{
			if (!isVallidRoomPosition(roomPosition)) return false;
			if (!rooms[(int)roomPosition.y][(int)roomPosition.x].isAllDoorsClosed()) return false;
			return true;
		}
		bool isVallidRoomPosition(Vector2 position)
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
			do
			{
				Debug.Log("Room is at " + selectedRoom.ToString());
				var movementList = new List<Vector2>();
				bool isUPOpenable = isValidRoomConnectionPosition(selectedRoom.position + new Vector2(0, +1));
				bool isLeftOpenable = isValidRoomConnectionPosition(selectedRoom.position + new Vector2(-1, 0));
				bool isDownOpenable = isValidRoomConnectionPosition(selectedRoom.position + new Vector2(0, -1));
				bool isRIghtOpenable = isValidRoomConnectionPosition(selectedRoom.position + new Vector2(1, 0));
				selectedRoom.open(isUPOpenable, isRIghtOpenable, isDownOpenable, isLeftOpenable);

				if (selectedRoom.openLeft) movementList.Add(new Vector2(-1, 0));
				if (selectedRoom.openRight) movementList.Add(new Vector2(1, 0));
				if (selectedRoom.openDown) movementList.Add(new Vector2(0, -1));
				if (movementList.Count == 0)
				{
					Debug.Log("Unavailalbe movment detected");
					break;
				}
				Vector2 newSelectedRoomPoision = Vector2.zero;
				int randomChosenPath = Random.Range(1, 100) % movementList.Count;
				bool newRoomIsChosen = false;
				for (int i = 0; i <3; i++)
				{
					//Debug.Log("newSelectedRoomPoision = " + selectedRoom.position + " + " + movementList[randomChosenPath]);
					newSelectedRoomPoision = selectedRoom.position +  movementList[randomChosenPath];
					//Debug.Log("newSelectedRoomPoision = " + newSelectedRoomPoision);
					if (isVallidRoomPosition(newSelectedRoomPoision)&&
						 rooms[(int)newSelectedRoomPoision.y][(int)newSelectedRoomPoision.x].isAllDoorsClosed())
					{
						newRoomIsChosen = true;
						break;
					}
				}
				if (newRoomIsChosen)
				{
					selectedRoom = rooms[(int)newSelectedRoomPoision.y][(int)newSelectedRoomPoision.x];
					//Debug.Log("Selecting room " + selectedRoom.ToString());
				}
				else
				{
					//chooinsg a room is failed 
					Debug.Log("Map creaion failed ");
					break;
				}
			} while (true);
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
