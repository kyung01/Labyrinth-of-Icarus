using UnityEngine;
using System.Collections;

public static class ExtensionMethods 
{

	public static void LookAt2D(this Transform thisTransform, Vector3 otherTransformPosition)
	{

		var dir = (otherTransformPosition - thisTransform.position).normalized;
		var parent = thisTransform.parent;
		thisTransform.parent = null;
		thisTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * (180.0f / Mathf.PI));
		thisTransform.parent = parent;
	}
}
