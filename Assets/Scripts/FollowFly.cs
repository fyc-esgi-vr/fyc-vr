using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFly : MonoBehaviour
{
    public GameObject fly;
    public Vector3 offset;
    
    private bool _flyIsMoving;
    private Transform _transform;
    private float _zOffset;
    private void Start()
    {
        _transform = this.gameObject.transform;
        _zOffset = fly.transform.position.z;
    }

    public void SetFlyMoving(bool val)
    {
        _flyIsMoving = val;
    }
         
    // Update is called once per frame
    private void Update()
    {
        //if (!_flyIsMoving) return;
        var flyTransform = fly.transform.localPosition;
        _transform.localPosition = new Vector3(flyTransform.x * 3,flyTransform.y,flyTransform.z - _zOffset);
        _transform.localRotation = fly.transform.localRotation;
    }
}
