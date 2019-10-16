using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleGenerator : MonoBehaviour
{
	/*
	[SerializeField]
	Tentacle PREFAB_TENTACLE;
	[SerializeField]
	TentaclePack PREFAB_TETACLE_UNIT;
	[SerializeField]
	TentacleLipLinker PREFAB_TETACLE_LIP_LINKER;
	[SerializeField]
	TentacleLip PREFAB_LIP;
	
	 * */
	[SerializeField]
	TentacleLink PREFAB_LINK;
	private void Start()
	{
		generateTentacleLengthof(10);
	}
	void hprAdd(Tentacle tentacleAddedTo , TentaclePack unitToUnPackToAdd)
	{
		tentacleAddedTo.add(unitToUnPackToAdd.bodyFirst);
		tentacleAddedTo.add(unitToUnPackToAdd.bodySecond);
		tentacleAddedTo.add(unitToUnPackToAdd.lip);
	}

	public TentacleLink generateTentacleLengthof(int length)
	{
		TentacleLink firstLink = Instantiate(PREFAB_LINK);
		firstLink.transform.position = this.transform.position;
		TentacleLink previousLink = firstLink;
		for (int i = 0; i < length-1; i++)
		{
			var newLink = Instantiate(PREFAB_LINK);
			newLink.transform.position = this.transform.position;
			newLink.before = previousLink;
			previousLink.after = newLink;
			previousLink = newLink;

		}
		return firstLink;
	}
	/*
	public Tentacle kGenerateTentacleLengthof(int length)
	{
		Tentacle tentacle = Instantiate(PREFAB_TENTACLE);
		TentaclePack packedUnit = null;
		Vector3 alignPosition = this.transform.position;
		for(int i = 0; i < length; i++)
		{
			//create first half
			if (packedUnit == null)
			{

				packedUnit = Instantiate(PREFAB_TETACLE_UNIT);
				hprAdd(tentacle, packedUnit);
				packedUnit.transform.position = alignPosition;
				alignPosition += new Vector3(0.55f, 0, 0); //add a bumper
			}
			else
			{
				//there is a previous 
				var linker = Instantiate(PREFAB_TETACLE_LIP_LINKER);
				var newTentacle = Instantiate(PREFAB_TETACLE_UNIT);
				hprAdd(tentacle, newTentacle);
				tentacle.add(linker.GetComponent<TentacleLip>());

				newTentacle.transform.position = alignPosition + new Vector3(0.05f, 0, 0);
				linker.transform.position = alignPosition;
				//linker.jointFrom.autoConfigureConnectedAnchor = true;
				//linker.jointTo.autoConfigureConnectedAnchor = true;
				linker.jointFrom.connectedBody = packedUnit.bodySecond.GetComponent<Rigidbody2D>();
				linker.jointTo.connectedBody = newTentacle.bodyFirst. GetComponent<Rigidbody2D>();
				linker.jointFrom.enabled = true;
				linker.jointTo.enabled = true;
				if (packedUnit != null)
					GameObject.Destroy(packedUnit.gameObject);
				packedUnit = newTentacle;
				alignPosition += new Vector3(0.6f, 0, 0); //add a bumper
			}
			if(packedUnit!=null)
				GameObject.Destroy(packedUnit.gameObject);
			//add a lip
			//create next half
			//connect with joints
			//pass second half as a joint for the next
		}
		return tentacle;

	}
	 * */

}
