using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public Transform target;
    public Vector3 TargetPos;
    public Quaternion lastPos;
    public float turnSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        TargetPos = target.position - transform.position;
        lastPos = Quaternion.LookRotation(-TargetPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, lastPos, Time.deltaTime * turnSpeed);
    }
}
