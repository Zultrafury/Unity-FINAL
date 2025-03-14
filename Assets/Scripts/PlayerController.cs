using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private int hp = 3;
    [SerializeField] private TMP_Text hptext;
    [SerializeField] private TMP_Text scoretext;
    [SerializeField] GameObject damagesparks;
    [SerializeField] GameObject hitsound;
    private Vector2 movement = Vector2.zero;
    private float rot = 0f;
    void Start()
    {
        hptext.text = ""+hp;
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TakeDamage()
    {
        hp--;
        hptext.text = ""+hp;
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = "" + (int)(-1 * transform.position.y);
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement *= 10;
        
        rot = Input.GetAxis("Mouse X");

        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("SwordSwing");
        }
    }

    private void FixedUpdate()
    {
        Vector3 movvec = new Vector3(movement.x, 0, movement.y);
        rb.AddRelativeForce(movvec);
        rb.rotation = Quaternion.Euler(0,(rot * 5) + rb.rotation.eulerAngles.y,0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            GameObject v = Instantiate(damagesparks, transform.position, transform.rotation);
            v.GetComponent<ParticleSystem>().Play();
            Instantiate(hitsound, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
