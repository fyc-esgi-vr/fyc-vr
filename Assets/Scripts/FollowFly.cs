using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFly : MonoBehaviour
{
    public GameObject fly;
    public Vector3 offset;
    
    private bool _flyIsMoving;
    private Vector3 _sizeFactor;
    private Transform _transform;
    private void Start()
    {
        _transform = this.gameObject.transform;
        var followFlyScale = _transform.localScale;
        var flyScale = fly.transform.localScale;
        float x = followFlyScale.x / flyScale.x;
        float y = followFlyScale.y / flyScale.y;
        float z = followFlyScale.z / flyScale.z;

        _sizeFactor = new Vector3(x, y, z);
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
        _transform.localPosition = new Vector3(flyTransform.x * 3,flyTransform.y,(flyTransform.z* (3)) - 0.9f);
        _transform.localRotation = fly.transform.localRotation;
    }
}
