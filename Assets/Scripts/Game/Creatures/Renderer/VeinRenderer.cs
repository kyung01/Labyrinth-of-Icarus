using UnityEngine;
using System.Collections;

public class VeinRenderer : MonoBehaviour
{
	[SerializeField]
	Vein vein;
	[SerializeField]
	LineRenderer lineRender;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		lineRender.positionCount = 1+vein.extendedRelativePositions.Count;
		var positionBegin = vein.transform.position.toVec2();
		lineRender.SetPosition(0, positionBegin);
		for (int i = 0; i< vein.extendedRelativePositions.Count; i++) {
			positionBegin += vein.extendedRelativePositions[i];
			lineRender.SetPosition(i+1, positionBegin);
		}
	}
}
