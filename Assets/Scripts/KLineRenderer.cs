﻿using UnityEngine;
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
		//Debug.Log("ANIMATION CYCLE #"+ animationCycleIndex);
		Color greyRed = new Color(0.1f, 0.1f, 0.25f);
		Color red = new Color(1.0f, 0, 0);
		float MAXIMUM_RED_INTENSITY = 0.5f;
		float width = 0.7f;
		float minimumHeight = 0.3f;
		if (animationCycleIndex == 0)
		{
			timeElapsedToStart += Time.deltaTime;
			float ratio = Mathf.Min(1, timeElapsedToStart / timeNeededToStart);
			//Debug.Log("RATIO " + ratio);
			colorGradient.colorKeys = new GradientColorKey[] {  new GradientColorKey(new Color(MAXIMUM_RED_INTENSITY*ratio, 0,0),0), new GradientColorKey(greyRed, ratio* 0.5f) };
			widthCurve.keys = new Keyframe[] { new Keyframe(0.0f, minimumHeight + ratio * width), new Keyframe(ratio*0.5f, minimumHeight ),new Keyframe(1, minimumHeight ) };
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
			colorGradient.colorKeys = new GradientColorKey[] {  new GradientColorKey(greyRed, Mathf.Max(ratio - 0.5f, 0)), new GradientColorKey(new Color(MAXIMUM_RED_INTENSITY,0,0), ratio), new GradientColorKey(greyRed, Mathf.Min(ratio + 0.5f, 1)) };
			widthCurve.keys = new Keyframe[] { new Keyframe(Mathf.Max(ratio - 0.5f, 0), minimumHeight), new Keyframe(ratio, minimumHeight+width), new Keyframe(Mathf.Min(ratio+0.5f,1), minimumHeight) };
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
			colorGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(greyRed, 0.5f + 0.5f* ratio), new GradientColorKey(new Color(MAXIMUM_RED_INTENSITY*  (1.0f-ratio),0,0) , 1) };
			widthCurve.keys = new Keyframe[] { new Keyframe(0.5f + 0.5f * ratio, minimumHeight), new Keyframe(1, minimumHeight+width * (1.0f -ratio) ) };
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
