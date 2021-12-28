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
    public float radius = 0.8f;
    
    private Vector3 _playerPosition;
    private bool once;

    
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
        flyObject.transform.position = new Vector3(_playerPosition.x + 0.5f, _playerPosition.y, _playerPosition.z);
    }

    public void MoveFly()
    {
        flyPos = flyObject.transform.position;
        //flyObject.transform.RotateAround(_playerPosition, new Vector3(0, 1, 0), 50 * Time.deltaTime);
        Vector3 vec = leftController.GetComponent<Rigidbody>().velocity;
        Debug.Log("velocity of controller: " + vec);
        flyObject.GetComponent<Rigidbody>().AddForce((vec * -1) /4);
    }

    public void RotateFly()
    {
        flyObject.transform.RotateAround(_playerPosition, new Vector3(0, 1, 0), 50 * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector3.Distance(leftController.transform.position, flyObject.transform.position);
        //Debug.Log(_distance);
        if (_distance < radius && !once)
        {
            Debug.Log("In range!");
            MoveFly();
            once = true;
        }
        else if (Vector3.Distance(flyObject.transform.position, flyPos) > 2)
        {
            flyObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            flyObject.transform.position = flyPos;
            once = false;
        }
        else
        {
            Debug.Log("Not in range!");
        }

        //leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool returnToLobby);
    }
}