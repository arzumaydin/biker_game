using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovement : MonoBehaviour
{
    public Transform target;
	private Vector3 offset = new Vector3(2f, -2f, -3f);

	void Update() {
		transform.position = target.position + offset;
	}
}
