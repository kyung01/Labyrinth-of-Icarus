using UnityEngine;
using System.Collections;

public static class ExtensionMethods 
{

	public static void LookAt2D(this Transform thisTransform, Vector3 otherTransformPosition)
	{

		var dir = (otherTransformPosition - thisTransform.position).normalized;
		var parent = thisTransform.parent;
		thisTransform.parent = null;
		thisTransform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * (180.0f / Mathf.PI));
		thisTransform.parent = parent;
	}
	public static Vector2 toVec2(this Vector3 v)
	{
		return new Vector2(v.x, v.y);
	}
}
