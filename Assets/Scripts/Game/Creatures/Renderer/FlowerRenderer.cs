using UnityEngine;
using System.Collections;

[System.Serializable]
public struct FlowerRendererSetting {
	public float animationSpeed;
	public Color colorDefault;
	public Color colorHighlight;
	
}

public class FlowerRenderer : MonoBehaviour
{
	[SerializeField]
	Flower flower;
	[SerializeField]
	Transform flowerBody;
	[SerializeField]
	SpriteRenderer flowerBodySprite;

	[SerializeField]
	FlowerRendererSetting settingIDL;
	[SerializeField]
	FlowerRendererSetting settingFoundTarget;
	[SerializeField]
	FlowerRendererSetting settingEngaged;
	float timeElapsed = 0;
	float animationSpeed = 1.0f;

	Color colorMain;
	Color colorHighlight;

	// Use this for initialization
	void Start()
	{
		flower.evntChangedState.Add(hdlAIEntityChangedState);
		setSetting(settingIDL);
	}
	void hdlAIEntityChangedState(AIEntity entity)
	{
		switch (entity.State) {
			case AIEntity.EntityState.FOUND_TARGET:
				setSetting(settingFoundTarget);
				//Rapid motion, 
				break;
			case AIEntity.EntityState.ENGAGED:
				setSetting(settingEngaged);
				break;
		}

	}
	void setSetting(FlowerRendererSetting setting)
	{
		animationSpeed = setting.animationSpeed;
		colorMain = setting.colorDefault;
		colorHighlight = setting.colorHighlight;
	}
	// Update is called once per frame
	void Update()
	{
		timeElapsed += animationSpeed * Time.deltaTime;

		float ratioMinSize = 0.8f;
		float ratioMaxSize = 1.0f;
		var minSize = flower.transform.localScale.x * ratioMinSize;
		var maxSize = flower.transform.localScale.x * ratioMaxSize;
		float ratio = Mathf.Abs(Mathf.Sin(timeElapsed));
		float ratioColor = Mathf.Abs( 0.5f - ratio) *2.0f;
		float ratioX = ratio;
		float ratioY = 1 - ratio;

		flowerBody.localScale = new Vector3(minSize + (maxSize - minSize) * ratioX, minSize + (maxSize - minSize) * ratioY,1);
		flowerBodySprite.color = new Color(
			colorMain.r + (colorHighlight.r - colorMain.r)* ratioColor, 
			colorMain.g + (colorHighlight.g - colorMain.g) * ratioColor, 
			colorMain.b + (colorHighlight.b - colorMain.b) * ratioColor
			);
	}
}
