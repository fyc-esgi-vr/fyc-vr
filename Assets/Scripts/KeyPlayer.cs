using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlayer : MonoBehaviour
{
    public AudioClip audioClip;
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile")) return;
        AudioSource.PlayClipAtPoint(audioClip,collision.contacts[0].point);
        StartCoroutine(DestroyCoroutine(collision.gameObject));
    }

    IEnumerator DestroyCoroutine(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(obj);
    }
        
}