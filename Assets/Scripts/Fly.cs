using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class Fly : MonoBehaviour
{

    public GameObject flyObject;
    public GameObject leftController;
    public Transform playerTransform;
    
    public float distance = 5f;
    public float radius = 2f;
    
    private Vector3 _playerPosition;

    
    private float _distance;
    private Vector3 flyPos;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(flyObject.transform.position, radius);
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerPosition = playerTransform.position;
        flyObject.transform.position = new Vector3(_playerPosition.x + distance, _playerPosition.y, _playerPosition.z);
    }

    public void MoveFly()
    {
        flyPos = flyObject.transform.position;
        //flyObject.transform.RotateAround(_playerPosition, new Vector3(0, 1, 0), 50 * Time.deltaTime);
        Vector3 vec = leftController.GetComponent<Rigidbody>().velocity;
        flyObject.GetComponent<Rigidbody>().velocity = vec * -1;
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector3.Distance(leftController.transform.position, flyObject.transform.position);

        if (_distance < radius)
        {
            MoveFly();
        }
        else if (Vector3.Distance(flyObject.transform.position, flyPos) > 2)
        {
            flyObject.transform.position = flyPos;
        }

        //leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool returnToLobby);
    }
}