using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AliveLineRenderer : MonoBehaviour
{
	[SerializeField]
	public float timeNeededToStart;
	[SerializeField]
	public float timeNeededToReachEnd;
	[SerializeField]
	public float timeNeededToFinish;
	[SerializeField]
	public Color defaultLineColor = new Color(0.3f, 0.3f, 0.3f);
	[SerializeField]
	public Color highlightLineColor;

	[SerializeField]
	public float widthMin, widthMax;

	int animationCycleIndex = 0;
	float timeElapsedToStart = 0;
	float timeElapsedReachingEnd = 0;
	float timeElapsedFinishingAnimation = 0;

	public
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
		float widthIncreased = widthMax-this.widthMin;

		if (animationCycleIndex == 0)
		{
			float ratio = Mathf.Min(1, timeElapsedToStart / timeNeededToStart);
			timeElapsedToStart += Time.deltaTime;
			//Debug.Log("RATIO " + ratio);
			colorGradient.colorKeys = new GradientColorKey[] {
				new GradientColorKey(defaultLineColor, -0.5f),
				new GradientColorKey(new Color(
					defaultLineColor.r+ (highlightLineColor.r- defaultLineColor.r)*ratio,
					defaultLineColor.g+(highlightLineColor.g-defaultLineColor.g)*(ratio),
					defaultLineColor.b+(highlightLineColor.b-defaultLineColor.b)*(ratio)
					),0),
				new GradientColorKey(defaultLineColor, 0.5f) };
			widthCurve.keys = new Keyframe[] { new Keyframe(0.0f, widthMin + ratio * widthIncreased), new Keyframe(0.5f, widthMin), new Keyframe(1, widthMin) };
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
			colorGradient.colorKeys = new GradientColorKey[] {
				new GradientColorKey(defaultLineColor, ratio - 0.5f),
				new GradientColorKey(highlightLineColor, ratio),
				new GradientColorKey(defaultLineColor, ratio + 0.5f) };
			widthCurve.keys = new Keyframe[] { new Keyframe(ratio - 0.5f, widthMin), new Keyframe(ratio, widthMin + widthIncreased), new Keyframe(ratio + 0.5f, widthMin) };

			if (timeElapsedReachingEnd > timeNeededToReachEnd)
			{
				animationCycleIndex = 2;
				timeElapsedReachingEnd = 0;

			}
		}
		else if (animationCycleIndex == 2)
		{
			float ratio = Mathf.Min(1, timeElapsedFinishingAnimation / timeNeededToFinish);
			timeElapsedFinishingAnimation += Time.deltaTime;
			float maxedRatio = 1 - 0.00001f;

			colorGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(defaultLineColor, maxedRatio - 0.5f),
				new GradientColorKey(
					new Color(
						highlightLineColor.r + (defaultLineColor.r - highlightLineColor.r)*ratio,
					highlightLineColor.g+(defaultLineColor.g-highlightLineColor.g)*ratio,
					highlightLineColor.b +(defaultLineColor.b-highlightLineColor.b)*ratio), maxedRatio),
				new GradientColorKey(defaultLineColor, maxedRatio + 0.5f) };


			widthCurve.keys = new Keyframe[] {
				new Keyframe(0.5f + 0.5f * ratio, widthMin),
				new Keyframe(1+0.5f*ratio, widthMin+widthIncreased * (1.0f -ratio) ),
				new Keyframe(1.5f+0.5f*ratio, widthMin )
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
