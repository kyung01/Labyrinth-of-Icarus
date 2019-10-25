using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class KLineRenderer : MonoBehaviour
{
	[SerializeField]
	float timeNeededToStart;
	[SerializeField]
	float timeNeededToReachEnd;
	[SerializeField]
	float timeNeededToFinish;

	int animationCycleIndex = 0;
	float timeElapsedToStart= 0;
	float timeElapsedReachingEnd = 0;
	float timeElapsedFinishingAnimation = 0;

	LineRenderer lineRenderer;
	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.generateLightingData = true;
	}
	// Use this for initialization
	void Start()
	{
		timeElapsedToStart = Random.Range(0, 100.0f);
		timeElapsedReachingEnd = Random.Range(0, 100.0f);
		timeElapsedFinishingAnimation = Random.Range(0, 100.0f);

		animationCycleIndex = Random.Range(0, 3);
	}
	float timeElapsed = 0;
	// Update is called once per frame
	void Update()
	{
	
		Gradient colorGradient = lineRenderer.colorGradient;
		var widthCurve = lineRenderer.widthCurve;
		//colorGradient.colorKeys[1].time = ratio;
		//Debug.Log("ANIMATION CYCLE #"+ animationCycleIndex);
		Color greyRed = new Color(0.3f, 0.3f, 0.3f);
		Color red = new Color(1.0f, 0, 0);
		float MAXIMUM_RED_INTENSITY = 1.0f;
		float width = 0.3f;
		float minimumHeight = 0.7f;
		
		if (animationCycleIndex == 0)
		{
			float ratio = Mathf.Min(1, timeElapsedToStart / timeNeededToStart);
			timeElapsedToStart += Time.deltaTime;
			//Debug.Log("RATIO " + ratio);
			colorGradient.colorKeys = new GradientColorKey[] {
				new GradientColorKey(greyRed, -0.5f),
				new GradientColorKey(new Color(
					greyRed.r+ (MAXIMUM_RED_INTENSITY- greyRed.r)*ratio, greyRed.g*(1-ratio),greyRed.b*(1-ratio)
					),0),
				new GradientColorKey(greyRed, 0.5f) };
			widthCurve.keys = new Keyframe[] { new Keyframe(0.0f, minimumHeight + ratio * width), new Keyframe(0.5f, minimumHeight ),new Keyframe(1, minimumHeight ) };
			if (timeElapsedToStart > timeNeededToStart)
			{
				animationCycleIndex = 1;
				timeElapsedToStart = 0;

			}
		}
		else if (animationCycleIndex == 1)
		{
			float ratio = Mathf.Min(1, timeElapsedReachingEnd / timeNeededToReachEnd);
			timeElapsedReachingEnd += Time.deltaTime;
			colorGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(greyRed, ratio - 0.5f), new GradientColorKey(new Color(MAXIMUM_RED_INTENSITY, 0, 0), ratio), new GradientColorKey(greyRed, ratio + 0.5f) };
			widthCurve.keys = new Keyframe[] { new Keyframe(ratio - 0.5f, minimumHeight), new Keyframe(ratio, minimumHeight+width), new Keyframe(ratio + 0.5f, minimumHeight) };
			
			if (timeElapsedReachingEnd > timeNeededToReachEnd)
			{
				animationCycleIndex = 2;
				timeElapsedReachingEnd = 0;

			}
		}
		else if(animationCycleIndex == 2)
		{
			float ratio = Mathf.Min(1, timeElapsedFinishingAnimation / timeNeededToFinish);
			timeElapsedFinishingAnimation += Time.deltaTime;
			float maxedRatio = 1 - 0.00001f;

			colorGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(greyRed, maxedRatio - 0.5f),
				new GradientColorKey(new Color(MAXIMUM_RED_INTENSITY + (greyRed.r - MAXIMUM_RED_INTENSITY)*ratio, greyRed.g*ratio,greyRed.b*ratio), maxedRatio),
				new GradientColorKey(greyRed, maxedRatio + 0.5f) };


			widthCurve.keys = new Keyframe[] {
				new Keyframe(0.5f + 0.5f * ratio, minimumHeight),
				new Keyframe(1+0.5f*ratio, minimumHeight+width * (1.0f -ratio) ),
				new Keyframe(1.5f+0.5f*ratio, minimumHeight )
			};
			if (timeElapsedFinishingAnimation > timeNeededToFinish)
			{
				animationCycleIndex = 0;
				timeElapsedFinishingAnimation = 0;

			}
		}
		else
		{

		}

		lineRenderer.colorGradient = colorGradient;
		lineRenderer.widthCurve = widthCurve;

		//Debug.Log(ratio);
		
	}
}
