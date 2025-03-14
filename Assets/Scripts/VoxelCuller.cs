using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoxelCuller : MonoBehaviour
{
    [SerializeField] private float timeout;
    private float timeoutinit;
    private Rigidbody rb;
    private Vector2 rand;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        timeoutinit = timeout;
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        if (timeout <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ChangeDirection()
    {
        rand = Random.insideUnitCircle * 10;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ChangeDirection());
    }

    private void FixedUpdate()
    {
        Vector3 pointer = new Vector3(rand.x, -3.0f, rand.y);
        transform.LookAt(pointer + transform.position);
        rb.position += transform.forward * (Time.fixedDeltaTime * 50);
        timeout -= Time.fixedDeltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Voxel")) return;
        //ChunkManager cm = other.GetComponentInParent<ChunkManager>();
        if (false)
        {
            //StartCoroutine(cm.Delayexit());
        }
        Destroy(other.gameObject);
        //timeout = timeoutinit;
    }
}
