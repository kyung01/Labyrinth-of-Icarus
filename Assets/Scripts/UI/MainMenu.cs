using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	enum MAINMENU_STATE { COVER,MAINMENU }
	[SerializeField]
	CoverPage coverPage;
	[SerializeField]
	MainMenuPage mainMenuPage;

	MAINMENU_STATE state = MAINMENU_STATE.COVER;
	// Start is called before the first frame update
	private void Awake()
	{
		
	}
	void Start()
    {
		coverPage.Off();
		mainMenuPage.Off();
		Debug.Log("START");
		UpdateState(state);

	}

    // Update is called once per frame
    void Update()
    {
		switch (state) {
			case MAINMENU_STATE.COVER:
				if (Input.GetMouseButtonDown(0))
					UpdateState(MAINMENU_STATE.MAINMENU);
				break;
			case MAINMENU_STATE.MAINMENU:	
				break;
		}
    }
	void UpdateState(MAINMENU_STATE newState)
	{
		switch (state)
		{
			case MAINMENU_STATE.COVER:
				coverPage.Off();
				break;
			case MAINMENU_STATE.MAINMENU:
				mainMenuPage.Off();
				break;
		}
		this.state = newState;
		switch (state)
		{
			case MAINMENU_STATE.COVER:
				coverPage.On();
				break;
			case MAINMENU_STATE.MAINMENU:
				mainMenuPage.On();
				break;
		}

	}
}
