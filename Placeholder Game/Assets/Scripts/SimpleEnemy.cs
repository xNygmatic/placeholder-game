using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public bool stompable;
    public float velocity;
    public Transform groundCheck;
    public float distanceToGround;
    public Transform sideCheckStart;
    public Transform sideCheckEnd;


    private CircleCollider2D hitbox;
    private Rigidbody2D rbody;
    private Animator anim;
    private bool stomped = false;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitbox = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    { 

        if (!stomped)
        {
            RaycastHit2D groundColliding = Physics2D.Raycast(groundCheck.position, Vector3.down, distanceToGround);
            RaycastHit2D sideColliding = Physics2D.Linecast(sideCheckStart.position, sideCheckEnd.position);

            if (!groundColliding)
            {
                Flip();
                return;
            }

            if (sideColliding && sideColliding.collider != null && (sideColliding.collider.tag == "Platform" || sideColliding.collider.tag == "Deadly"))
            {
                Flip();
                return;
            }
            rbody.velocity = new Vector2(velocity, rbody.velocity.y);
        }
    }

    public void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        velocity *= -1;
    }

    public void Die()
    {
        stomped = true;
        anim.SetBool("Stomped", true);
        gameObject.tag = "Untagged";
        rbody.bodyType = RigidbodyType2D.Static;
        hitbox.enabled = false;
        Player.stompsAmount += 1;
        Destroy(this.gameObject, 1f);
    }
}