using UnityEngine;
using System.Collections;

public static class Layers
{
	public static LayerMask WorldAndPlayer {
		get
		{
			return LayerMask.GetMask("World", "Player");
		}
	}

	public static LayerMask World {
		get { return LayerMask.GetMask("World"); }
	}

}
