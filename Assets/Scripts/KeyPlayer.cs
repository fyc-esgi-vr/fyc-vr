using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlayer : MonoBehaviour
{
    [Tooltip("Audio that will be played when key is touched.")]
    public AudioClip audioClip;

    [Tooltip("The amount of time the projectile will stay on the key after contact.")]
    public float destroyDelay = 0.75f;
    
    //when something collides with the key, if it's a projectile play the sound at the point of impact and destroy it.
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile")) return;
        AudioSource.PlayClipAtPoint(audioClip,collision.contacts[0].point);
        StartCoroutine(DestroyCoroutine(collision.gameObject));
    }

    IEnumerator DestroyCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(obj);
    }
        
}