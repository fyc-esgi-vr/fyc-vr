using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip audioClip;
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile")) return;
        AudioSource.PlayClipAtPoint(audioClip,collision.contacts[0].point);
        Debug.Log("AIE");
        Destroy(collision.gameObject);
    }
        
}
