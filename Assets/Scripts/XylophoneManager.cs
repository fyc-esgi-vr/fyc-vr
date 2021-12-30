using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneManager : MonoBehaviour
{
    public GameObject projectile;
    public List<Transform> KeyTransforms;
    
    private Transform _transform;
    private List<Vector3> positions;

    private float _time = 0.0f;
    private void Start()
    {
        positions = new List<Vector3>();
        foreach (var transform in KeyTransforms)
        {
            var pos = transform.position;
            Vector3 position = new Vector3(pos.x, projectile.transform.position.y, pos.z);
            positions.Add(position);
        }
    }

    void PlayNote(int index, float zOffset = 0)
    {
        positions[index] = new Vector3(positions[index].x, positions[index].y, zOffset);
        Instantiate(projectile, positions[index], Quaternion.identity);
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
        if (_time > 0.5 && _time < 0.52)
        {
            PlayNote(0);
        }
        if (_time > 0.9 && _time < 0.92)
        {
            PlayNote(1, 1.5f);
        }
        if (_time > 1.7 && _time < 1.72)
        {
            PlayNote(2,-1.5f);
        }
        if (_time > 2.7 && _time < 2.72)
        {
            PlayNote(5);
        }
        if (_time > 3.7 && _time < 3.72)
        {
            PlayNote(4, 1.5f);
        }
        if (_time > 4.7 && _time < 4.72)
        {
            PlayNote(4, -1.5f);
        }
    }
}
