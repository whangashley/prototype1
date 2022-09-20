using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1 : MonoBehaviour
{
//speed variable
    public float speed;

    //rigidbody variable called player1Body
    // Rigidbody2D player1Body;
    // AudioSource audioSrcP1;
    // public bool isMoving;

    public float knockbackTimeP2;

    player2 player2Script;
    public GameObject objplayer2;

    public bool player2Stun;

    public Animator animator1;
    public Animator animator2;

    public ParticleSystem collisionParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        //setting player 1's rigidbody into the variable
        // player1Body = gameObject.GetComponent<Rigidbody2D>();
        // audioSrcP1 = GetComponent<AudioSource>();
        // isMoving = false;
        //knockbackTimeP2 = 0.6f;
        player2Script = objplayer2.GetComponent<player2>();
        player2Stun = false;
        animator1.SetBool("player1Moving", false);
    }

    // Update is called once per frame
    void Update()
    {
        animator1.SetBool("player1Moving", false);

        if (player2Script.player1Stun == false) {
            //if player 1 presses W
            if (Input.GetKey(KeyCode.W)) {
                if (transform.position.y < 3.5f) {
                    //then player 1 will move up
                    Move(Vector3.up);
                }
            }
            //else if player 1 presses S
            else if (Input.GetKey(KeyCode.S)) {
                //and if the player's y position is above the bottom of the screen
                if (transform.position.y > -3.3f) {
                    //then player 1 will move down
                    Move(Vector3.down);
                }
            }

            if (Input.GetKey(KeyCode.D)) {
                if (transform.position.x < 3.8f) {
                    Move(Vector3.right);
                }
            }
            else if (Input.GetKey(KeyCode.A)) {
                if (transform.position.x > -3.8f) {
                    Move(Vector3.left);
                }
            }

            // if (player1Body.velocity != Vector2.zero) {
            //     isMoving = true;
            // }
            // else {
            //     isMoving = false;
            // }

            // if (isMoving) {
            //     if (!audioSrcP1.isPlaying) {
            //         audioSrcP1.Play();
            //     }
            // }
            // else {
            //     audioSrcP1.Stop();
            // }
        }
    }

    //move function
    void Move(Vector3 direction) {
        transform.position += direction * speed;
        animator1.SetBool("player1Moving", true);
        // FindObjectOfType<audioManager>().Play("player1Move");
    }

    //if the player collides with an enemy
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "player2") {
            animator2.SetBool("player2Hit", true);
            FindObjectOfType<audioManager>().Play("bonk");
            Rigidbody2D player2Body = collision.GetComponent<Rigidbody2D>();
            Debug.Log("i hit player 2");
            player2Stun = true;
            var em = collisionParticleSystem.emission;
            var dur = collisionParticleSystem.duration;
            em.enabled = true;
            collisionParticleSystem.Play();
            //set kinematic to dynamic so force is applied (gravity on)
            player2Body.isKinematic = false;
            Vector2 difference = player2Body.transform.position - transform.position;
            difference = difference.normalized * 2;
            player2Body.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(countDownCo(player2Body));
        }
    }
    private IEnumerator countDownCo(Rigidbody2D player2) {
        yield return new WaitForSeconds(knockbackTimeP2);
        player2.velocity = Vector2.zero;
        player2.isKinematic = true;
        animator2.SetBool("player2Hit", false);
        player2Stun = false;
        FindObjectOfType<audioManager>().Play("p1giggle");
        var em = collisionParticleSystem.emission;
        em.enabled = false;
    }
}
