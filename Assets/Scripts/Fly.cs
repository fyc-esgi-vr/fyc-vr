using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class Fly : MonoBehaviour
{

    public GameObject flyObject;
    public Transform playerTransform;
    
    public float distance = 5f;
    public InputDevice leftController;

    private Vector3 _playerPosition;


    // Start is called before the first frame update
    void Start()
    {
        _playerPosition = playerTransform.position;
        flyObject.transform.position = new Vector3(_playerPosition.x + distance, _playerPosition.y, _playerPosition.z);
    }

    private void MoveFly()
    {
        flyObject.transform.RotateAround(_playerPosition, new Vector3(0, 1, 0), 50 * Time.deltaTime);

    }

    // Update is called once per frame
    void Update()
    {
        MoveFly();

        //leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool returnToLobby);
    }
}