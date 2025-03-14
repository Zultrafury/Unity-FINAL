using System;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    [SerializeField] GameObject damagesparks;
    [SerializeField] GameObject hitsound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject v = Instantiate(damagesparks, transform.position, transform.rotation);
            v.GetComponent<ParticleSystem>().Play();
            Instantiate(hitsound, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
