using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneManager : MonoBehaviour
{
    public GameObject projectile;
    private Transform _transform;
    private void Start()
    {
        _transform = projectile.transform;
        Instantiate(projectile);
        Vector3 position = _transform.position;
        position = new Vector3(position.x + 1.5f, position.y, position.z);
        _transform.position = position;
        Instantiate(projectile, _transform);
    }
}
