using UnityEngine;
using System.Collections;

public class Avoidance : MonoBehaviour {
	
	public bool DebugMode;
	
	void Update () 
	{
		/***if to close to a wall move away***/
		float distance = 0.5f;
		RaycastHit hit;
		if (Physics.Raycast(transform.position, new Vector3(-distance, 0, 0), out hit, distance))
		{
			if (hit.transform.tag != "con")
			{
				if (DebugMode)
					Debug.DrawLine(transform.position, transform.position + new Vector3(-distance, 0, 0));
				float resistPower = 0.1f - hit.distance/(distance*10);
				transform.position += new Vector3(resistPower, 0, 0);
			}
		}
		if (Physics.Raycast(transform.position, new Vector3(distance, 0, 0), out hit, distance))
		{
			if (hit.transform.tag != "con")
			{
				if (DebugMode)
					Debug.DrawLine(transform.position, transform.position + new Vector3(distance, 0, 0));
				float resistPower = 0.1f - hit.distance/(distance*10);
				transform.position += new Vector3(-resistPower, 0, 0);
			}
		}
		if (Physics.Raycast(transform.position, new Vector3(0, 0, distance), out hit, distance))
		{
			if (hit.transform.tag != "con")
			{
				if (DebugMode)
					Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0, distance));
				float resistPower = 0.1f - hit.distance/(distance*10);
				transform.position += new Vector3(0, 0, -resistPower);
			}
		}
		if (Physics.Raycast(transform.position, new Vector3(0, 0, -distance), out hit, distance))
		{
			if (hit.transform.tag != "con")
			{
				if (DebugMode)
					Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0, -distance));
				float resistPower = 0.1f - hit.distance/(distance*10);
				transform.position += new Vector3(0, 0, resistPower);
			}
		}
		
		if (Physics.Raycast(transform.position, new Vector3(distance, 0, distance), out hit, distance))
		{
			if (hit.transform.tag != "con")
			{
				if (DebugMode)
					Debug.DrawLine(transform.position, transform.position + new Vector3(distance, 0, distance));
				float resistPower = 0.1f - hit.distance/(distance*10);
				transform.position += new Vector3(-resistPower, 0, -resistPower);
			}
		}
		if (Physics.Raycast(transform.position, new Vector3(distance, 0, -distance), out hit, distance))
		{
			if (hit.transform.tag != "con")
			{
				if (DebugMode)
					Debug.DrawLine(transform.position, transform.position + new Vector3(distance, 0, -distance));
				float resistPower = 0.1f - hit.distance/(distance*10);
				transform.position += new Vector3(-resistPower, 0, resistPower);
			}
		}
		if (Physics.Raycast(transform.position, new Vector3(-distance, 0, distance), out hit, distance))
		{
			if (hit.transform.tag != "con")
			{
				if (DebugMode)
					Debug.DrawLine(transform.position, transform.position + new Vector3(-distance, 0, distance));
				float resistPower = 0.1f - hit.distance/(distance*10);
				transform.position += new Vector3(resistPower, 0, -resistPower);
			}
		}
		if (Physics.Raycast(transform.position, new Vector3(-distance, 0, -distance), out hit, distance))
		{
			if (hit.transform.tag != "con")
			{
				if (DebugMode)
					Debug.DrawLine(transform.position, transform.position + new Vector3(-distance, 0, -distance));
				float resistPower = 0.1f - hit.distance/(distance*10);
				transform.position += new Vector3(resistPower, 0, resistPower);
			}
		}
	}
}
