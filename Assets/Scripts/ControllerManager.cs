using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class ControllerManager : MonoBehaviour
{
    public InputDeviceCharacteristics  characteristic;
    private InputDevice _leftController;
    
    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        //get available devices with characteristics
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristic, devices);


        if (devices.Count > 0)
        {
            foreach (var device in devices) 
            {
                Debug.Log(device.name + " was added with char " + device.characteristics);
            }

            _leftController = devices[0];
            
        }
    }

    private void Update()
    {
        //if left trigger is pressed more than halfway through, reload current scene
        _leftController.TryGetFeatureValue(CommonUsages.trigger, out float force);
        if (force >= 0.5) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
