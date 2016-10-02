using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    public GameObject follow;
    private Vector3 offset;
    private float fixedZPosition;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - follow.transform.position;
        fixedZPosition = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = offset + follow.transform.position;
        transform.position.Set(transform.position.x, transform.position.y, fixedZPosition);
    }
}
