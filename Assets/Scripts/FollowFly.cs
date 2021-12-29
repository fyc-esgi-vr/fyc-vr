using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFly : MonoBehaviour
{
    public GameObject fly;
    public Vector3 offset;
    private Transform _transform;
    private void Start()
    {
        _transform = transform;
        Vector3 pos = transform.localPosition;
        pos += offset;
        _transform.localPosition = pos;
        _transform.localScale = Vector3.one * 0.5f;
    }
         
    // Update is called once per frame
    void Update()
    {
        _transform.localPosition = fly.transform.localPosition;
        _transform.localRotation = fly.transform.localRotation;
    }
}
