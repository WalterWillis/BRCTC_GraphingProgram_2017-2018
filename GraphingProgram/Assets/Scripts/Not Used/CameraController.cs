using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;

	// Use this for initialization
	void Start () {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position - offset;

        transform.LookAt(target);
	}
}
