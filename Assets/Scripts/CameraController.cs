using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject follow;
    private Vector3 offset;
    private float zPosition;
	// Use this for initialization
	void Start () {
        zPosition = transform.position.z;
        offset = transform.position - follow.transform.position;
    }

    // Update is called once per frame
    void LateUpdate () {
        transform.position = offset + follow.transform.position;
        Debug.logger.Log("position1:" + transform.position);
        transform.position.Set(transform.position.x, transform.position.y, zPosition);
        Debug.logger.Log("position2:" + transform.position);
        Debug.logger.Log("z:" + zPosition);


    }
}
