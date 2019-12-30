using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public Transform deathZone;
    public static bool hitCheckpoint;
    public Transform teleport;

    private bool dead;
    private float horizontalInput;
    private Rigidbody2D rbody;
    private Animator animator;
    private BoxCollider2D hitbox;
    private LevelManager levelManager;

    public enum DIRECTION { LEFT, RIGHT };
    DIRECTION currentDirection;

    private void Awake() //Only for checking if to respawn at the teleporter when dying, makes it just a tiny bit faster so the graphics don't glitch for a sec
    {
        rbody = GetComponent<Rigidbody2D>();
        if (hitCheckpoint)
        {
            rbody.position = teleport.position;
        }
    }

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        currentDirection = DIRECTION.RIGHT;
        animator = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
        levelManager = GetComponent<LevelManager>();
        dead = false;
    }

    void Update()
    {
        if (!dead)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            rbody.velocity = new Vector2(horizontalInput * speed, rbody.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

            switch (currentDirection)
            {
                case DIRECTION.LEFT:
                    if (horizontalInput > 0)
                    {
                        currentDirection = DIRECTION.RIGHT;
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    break;

                case DIRECTION.RIGHT:
                    if (horizontalInput < 0)
                    {
                        currentDirection = DIRECTION.LEFT;
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
                    break;
            }

            if (rbody.velocity.y < -0.1)
            {
                animator.SetBool("Falling", true);
            } else
            {
                animator.SetBool("Falling", false);
            }

            JumpCheck();

            if (rbody.position.y <= deathZone.position.y)
            {
                rbody.position = new Vector2(rbody.position.x, rbody.position.y + 3f);
                Die();
            }
        }
    }

    private void JumpCheck()
    {
        if (Grounded() && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            animator.SetTrigger("Jumping");
        }
    }

    public void Jump()
    {
        FindObjectOfType<AudioManager>().Play("Jump");
        rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
    }

    public bool Grounded()
    {
        Collider2D colliding = Physics2D.OverlapCircle(groundCheck.position, checkRadius);

        if (colliding && colliding.tag == "Platform")
        {
            animator.SetBool("Grounded", true);
            return true;
        }
        animator.SetBool("Grounded", false);
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Deadly":

                SimpleEnemy enemy = collision.collider.GetComponent<SimpleEnemy>();

                if (enemy != null)
                {
                    foreach (ContactPoint2D point in collision.contacts)
                    {
                        if (enemy.stompable && point.normal.y >= 0.5f)
                        {
                            Jump();
                            enemy.Die();
                            return;
                        }
                    }
                }
                Die();
                break;

            case "Ending":
                levelManager.LoadEndScreen();
                break;
        }
    }

    private void Die()
    {
        dead = true;
        rbody.bodyType = RigidbodyType2D.Static;
        hitbox.enabled = false;
        animator.SetBool("Dead", true);
        Player.livesAmount -= 1;

        if (Player.livesAmount >= 0)
        {
            FindObjectOfType<AudioManager>().Play("Death");
        } else
        {
            FindObjectOfType<AudioManager>().Play("GameOver");
        }

        Invoke("DeathLevelLogic", 1.3f); //lil delay so it's not instant when you die
    }

    private void DeathLevelLogic()
    {
        if (Player.livesAmount < 0)
        {
            levelManager.LoadEndScreen();
            return;
        }
        levelManager.RestartCurrentLevel();
    }
}