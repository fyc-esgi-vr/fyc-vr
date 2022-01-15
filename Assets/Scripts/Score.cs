using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Score
{
    [Tooltip("Indicates the keys to be played. 0 = Do, 1 = Re...")]
    public List<int> indexes;
    [Tooltip("Indicates the time to wait before the next key is played for each note")]
    public List<float> intervals;
}
