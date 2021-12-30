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

    public Key keys;
 
    private float _time = 0.0f;
    private int _index = 0;
    private bool _isValid = true;
    private void Start()
    {
        if (keys.indexes.Count != keys.intervals.Count)
        {
            Debug.LogError("The numbers of keys and intervals are not equal");
            _isValid = false;
            return;
        }
        positions = new List<Vector3>();
        foreach (var transform in KeyTransforms)
        {
            var pos = transform.position;
            Vector3 position = new Vector3(pos.x, projectile.transform.position.y, pos.z);
            positions.Add(position);
        }
    }

    //do a map of keys to play with :
    //lvalue = key to play
    //rvalue = frequency value 
    void PlayNote(int index, float zOffset = 0, float interval = 0.5f)
    {
        //if(_time >= 0.5) 
        positions[index] = new Vector3(positions[index].x, positions[index].y, zOffset);
        Instantiate(projectile, positions[index], Quaternion.identity);
    }

    //problems:
    //1. on lui dit jamais quand s'arreter
    //2. c'est le BORDELLE
    private void FixedUpdate()
    {
        if (_index > keys.indexes.Count - 1) return;
        if (!_isValid) return;
        _time += Time.deltaTime;

        if (_time < keys.intervals[_index]) return;
        PlayNote(keys.indexes[_index]);
        _time = 0f;
        _index++;
    }
}
