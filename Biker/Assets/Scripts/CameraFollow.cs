using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	Vector3 offset = new Vector3(-3f, 2f, 0.4f);

	void Update()
	{
		transform.position = target.position + offset;
	}

}
