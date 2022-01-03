using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneManager : MonoBehaviour
{
    [Tooltip("Prefab used to play a note.")]
    public GameObject projectile;
    [Tooltip("Stores the Transforms of the keys.")]
    public List<Transform> keyTransforms;
    
    //stores the positions without having the instance of the transform so that we can modify them
    private List<Vector3> _positions;
    
    [Tooltip("Store the song to be played.\nindexes store the notes to play : 0 = Do, 1 = Re...\nIntervals store the amount to wait before playing the next key.")]
    public Key keys;
 
    //determines the time passed since last key was played
    private float _time = 0.0f;
    private int _index = 0;
    private bool _isValid = true;
    private void Start()
    {
        //if data is stored incorrectly, don't play the music
        if (keys.indexes.Count != keys.intervals.Count)
        {
            Debug.LogError("The numbers of keys and intervals are not equal");
            _isValid = false;
            return;
        }
        _positions = new List<Vector3>();
        //get all the positinos from the transforms of the keys
        foreach (var transform in keyTransforms)
        {
            var pos = transform.position;
            Vector3 position = new Vector3(pos.x, projectile.transform.position.y, pos.z);
            _positions.Add(position);
        }
    }

    //drops a projectile on the key of the index given in parameter.
    void PlayNote(int index, float zOffset = 0)
    {
        _positions[index] = new Vector3(_positions[index].x, _positions[index].y, zOffset);
        Instantiate(projectile, _positions[index], Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if (_index > keys.indexes.Count - 1) return;
        if (!_isValid) return;
        _time += Time.deltaTime;

        if (_time < keys.intervals[_index]) return;
        float zPos = (keyTransforms[0].localScale.z / 2) - projectile.transform.localScale.z;
        PlayNote(keys.indexes[_index], UnityEngine.Random.Range(-zPos, zPos));
        _time = 0f;
        _index++;
    }
}
