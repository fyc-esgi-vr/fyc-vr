using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile")) return;
        
        audioSource.clip = audioClip;
        audioSource.gameObject.transform.position = collision.contacts[0].point;
        audioSource.Play();
        Debug.Log("AIE");
        Destroy(collision.gameObject);
    }
        
}
