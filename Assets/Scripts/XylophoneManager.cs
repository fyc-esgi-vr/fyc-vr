using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class XylophoneManager : MonoBehaviour
{
    [Tooltip("Prefab used to play a note.")]
    public GameObject projectile;
    
    [Tooltip("Distance of the marbles to fall")]
    public float yMarbleOffset = 2f;
    
    [Space]
    
    [Tooltip("Stores the Transforms of the keys.")]
    public List<Transform> keyTransforms;

    
    [Tooltip("Store the song to be played.\nindexes store the notes to play : 0 = Do, 1 = Re...\nIntervals store the amount to wait before playing the next key.")]
    public Score score;
 
    //determines the time passed since last key was played
    private float _time = 0.0f;
    private int _index = 0;
    private bool _isValid = true;
    private void Awake()
    {
        //if data is stored incorrectly, don't play the music
        
        //if the amount of indexes and intervals are different error
        if (score.indexes.Count != score.intervals.Count)
        {
            Debug.LogError("The numbers of keys and intervals are not equal");
            _isValid = false;
        }
        
        //if invalid indices are listed error
        for(int i = 0; i < score.indexes.Count; ++i)
        {
            if (score.indexes[i] < keyTransforms.Count && score.indexes[i] >= 0) continue;
            Debug.LogError("Invalid index in list of indexes at index " + i + " with value: " + score.indexes[i] + ".");
            _isValid = false;
        }

    }

    //drops a projectile on the key of the index given in parameter.
    void PlayNote(int index, float zOffset = 0)
    {
        Vector3 spawnPoint =  new Vector3(keyTransforms[index].position.x, keyTransforms[index].position.y + yMarbleOffset, zOffset);
        Instantiate(projectile, spawnPoint, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if (_index > score.indexes.Count - 1) return;
        if (!_isValid) return;
        _time += Time.deltaTime;

        if (_time < score.intervals[_index]) return;
        float zPos = (keyTransforms[0].localScale.z / 2) - projectile.transform.localScale.z;
        PlayNote(score.indexes[_index], UnityEngine.Random.Range(-zPos, zPos));
        _time = 0f;
        _index++;
    }
}
