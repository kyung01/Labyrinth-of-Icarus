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
	}
	// Use this for initialization
	void Start()
	{

	}
	float timeElapsed = 0;
	// Update is called once per frame
	void Update()
	{
	
		Gradient colorGradient = lineRenderer.colorGradient;
		var widthCurve = lineRenderer.widthCurve;
		//colorGradient.colorKeys[1].time = ratio;
		Debug.Log("ANIMATION CYCLE #"+ animationCycleIndex);
		Color greyRed = new Color(0.1f, 0, 0);
		Color red = new Color(1.0f, 0, 0);
		if (animationCycleIndex == 0)
		{
			timeElapsedToStart += Time.deltaTime;
			float ratio = Mathf.Min(1, timeElapsedToStart / timeNeededToStart);
			Debug.Log("RATIO " + ratio);
			colorGradient.colorKeys = new GradientColorKey[] {  new GradientColorKey(new Color(ratio,0,0),0), new GradientColorKey(Color.black, ratio* 0.5f) };
			widthCurve.keys = new Keyframe[] { new Keyframe(0.0f, ratio), new Keyframe(ratio, 0),new Keyframe(1, 0) };
			if (timeElapsedToStart > timeNeededToStart)
			{
				animationCycleIndex = 1;
				timeElapsedToStart = 0;

			}
		}
		else if (animationCycleIndex == 1)
		{
			timeElapsedReachingEnd += Time.deltaTime;
			float ratio = Mathf.Min(1, timeElapsedReachingEnd / timeNeededToReachEnd);
			colorGradient.colorKeys = new GradientColorKey[] {  new GradientColorKey(Color.black, Mathf.Max(ratio - 0.5f, 0)), new GradientColorKey(Color.red, ratio), new GradientColorKey(Color.black, Mathf.Min(ratio + 0.5f, 1)) };
			widthCurve.keys = new Keyframe[] { new Keyframe(Mathf.Max(ratio - 0.5f, 0), 0), new Keyframe(ratio, 1), new Keyframe(Mathf.Min(ratio+0.5f,1), 0) };
			if(timeElapsedReachingEnd > timeNeededToReachEnd)
			{
				animationCycleIndex = 2;
				timeElapsedReachingEnd = 0;

			}
		}
		else if(animationCycleIndex == 2)
		{
			timeElapsedFinishingAnimation += Time.deltaTime;
			float ratio = Mathf.Min(1, timeElapsedFinishingAnimation / timeNeededToFinish);
			colorGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(Color.black, 0.5f + 0.5f* ratio), new GradientColorKey(new Color(1.0f-ratio,0,0) , 1) };
			widthCurve.keys = new Keyframe[] { new Keyframe(0.5f + 0.5f * ratio, 0), new Keyframe(1, 1.0f-ratio) };
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
