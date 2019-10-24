using UnityEngine;
using System.Collections;

public static class Layers
{

	public static LayerMask World {
		get { return LayerMask.GetMask("World"); }
	}

}
