using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAssembler : MonoBehaviour
{
	[SerializeField]
	KKSNake PREFAB_SNAKE;
	[SerializeField]
	SnakeHead PREFAB_SNAKE_HEAD;
	[SerializeField]
	SnakeBody PREFAB_SNAKE_BODY;
	[SerializeField]
	int snakeLength;
	// Start is called before the first frame update
	void Start()
    {
		spawnSnake(snakeLength);

	}
	void spawnSnake(int snakeLength)
	{
		if (snakeLength <= 1) return;

		KKSNake snake = Instantiate(PREFAB_SNAKE);
		snake.transform.localPosition = this.transform.localPosition;
		snake.transform.localRotation = this.transform.localRotation;
		SnakeHead head = Instantiate(PREFAB_SNAKE_HEAD);
		List<SnakeBody> bodies = new List<SnakeBody>();
		for(int i = 0; i < snakeLength; i++)
		{
			bodies.Add(Instantiate(PREFAB_SNAKE_BODY));
		}
		snake.init(head, bodies);
	}
    // Update is called once per frame
    void Update()
    {
		//Debug.Log(this.transform.right);
        
    }
}
