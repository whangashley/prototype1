using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{

    /*Below are the links and sources used to help code this project!
    
    “Enemy Knockback in Unity” by BliskenX
    https://www.youtube.com/watch?v=nHPXiRNaNoM*/
    
    //speed variable
    public float speed;

    //rigidbody variable called player2Body
    //Rigidbody2D player2Body;

    public float knockbackTimeP1;
    public bool player1Hit;

    public Animator animator2;
    // Start is called before the first frame update
    void Start()
    {
        //setting player 2's rigidbody into the variable
        //player2Body = gameObject.GetComponent<Rigidbody2D>();
        //knockbackTimeP1 = 0.6f;
        player1Hit = false;
        animator2.SetBool("player2Moving", false);
    }

    // Update is called once per frame
    void Update()
    {
        animator2.SetBool("player2Moving", false);
        //if player 2 presses up arrow key
        if (Input.GetKey(KeyCode.UpArrow)) {
            if (transform.position.y < 5f) {
                //then player 2 will move up
                Move(Vector3.up);
            }
        }
        //else if player 2 presses down arrow key
        else if (Input.GetKey(KeyCode.DownArrow)) {
            //and if the player's y position is above the bottom of the screen
            if (transform.position.y > -4.6f) {
                //then player 2 will move down
                Move(Vector3.down);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            if (transform.position.x < 6.6f) {
                Move(Vector3.right);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            if (transform.position.x > -6f) {
                Move(Vector3.left);
            }
        }
    }

    //move function
    void Move(Vector3 direction) {
        transform.position += direction * speed;
        animator2.SetBool("player2Moving", true);
    }
    
    //if the player hits an enemy
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "player1") {
            Rigidbody2D player1Body = collision.GetComponent<Rigidbody2D>();
            Debug.Log("i hit player 1");
            //set kinematic to dynamic so force is applied (gravity on)
            player1Body.isKinematic = false;
            Vector2 difference = player1Body.transform.position - transform.position;
            difference = difference.normalized * 2;
            player1Body.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(countDownCo(player1Body));
        }
    }
    private IEnumerator countDownCo(Rigidbody2D player1) {
        yield return new WaitForSeconds(knockbackTimeP1);
        player1.velocity = Vector2.zero;
        player1.isKinematic = true;
    }
}
