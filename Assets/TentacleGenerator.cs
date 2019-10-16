using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGenerator : MonoBehaviour
{
	[SerializeField]
	Tentacle PREFAB_TENTACLE;
	[SerializeField]
	TentacleUnit PREFAB_TETACLE_UNIT;
	[SerializeField]
	TentacleLipLinker PREFAB_TETACLE_LIP_LINKER;
	[SerializeField]
	TentacleLip PREFAB_LIP;
	private void Start()
	{
		generateTentacleLengthof(3);
	}
	void generateTentacleLengthof(int length)
	{
		Tentacle tentacle = Instantiate(PREFAB_TENTACLE);
		TentacleUnit unit = null;
		Vector3 alignPosition = this.transform.position;
		for(int i = 0; i < length; i++)
		{
			//create first half
			if (unit == null)
			{

				unit = Instantiate(PREFAB_TETACLE_UNIT);
				unit.transform.position = alignPosition;
				alignPosition += new Vector3(0.55f, 0, 0); //add a bumper
			}
			else
			{
				//there is a previous 
				var linker = Instantiate(PREFAB_TETACLE_LIP_LINKER);
				var newTentacle = Instantiate(PREFAB_TETACLE_UNIT);
				newTentacle.transform.position = alignPosition + new Vector3(0.05f, 0, 0);
				linker.transform.position = alignPosition;
				//linker.jointFrom.autoConfigureConnectedAnchor = true;
				//linker.jointTo.autoConfigureConnectedAnchor = true;
				linker.jointFrom.connectedBody = unit.bodySecond.GetComponent<Rigidbody2D>();
				linker.jointTo.connectedBody = newTentacle.bodyFirst. GetComponent<Rigidbody2D>();
				linker.jointFrom.enabled = true;
				linker.jointTo.enabled = true;
				unit = newTentacle;
				alignPosition += new Vector3(0.6f, 0, 0); //add a bumper
			}
			//add a lip
			//create next half
			//connect with joints
			//pass second half as a joint for the next
		}

	}

}
