using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1 : MonoBehaviour
{
//speed variable
    public float speed;

    //rigidbody variable called player1Body
    //Rigidbody2D player1Body;

    public float knockbackTimeP2;
    public bool player2Hit;

    // Start is called before the first frame update
    void Start()
    {
        //setting player 1's rigidbody into the variable
        //player1Body = gameObject.GetComponent<Rigidbody2D>();
        knockbackTimeP2 = 0.6f;
        player2Hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if player 1 presses W
        if (Input.GetKey(KeyCode.W)) {
            //then player 1 will move up
            Move(Vector3.up);
        }
        //else if player 1 presses S
        else if (Input.GetKey(KeyCode.S)) {
            //and if the player's y position is above the bottom of the screen
            // if (transform.position.y > -9.4f) {
            //     //then player 1 will move down
                Move(Vector3.down);
            // }
        }

        if (Input.GetKey(KeyCode.D)) {
            Move(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A)) {
            Move(Vector3.left);
        }

        //if player is hit, then the timer will countdown
        if (player2Hit == true) {
            knockbackTimeP2 -= Time.deltaTime; 
        }
        //if the countdown finishes, player is not hit. reset the timer
        else if (player2Hit == false) {
            knockbackTimeP2 = 0.6f;             
        }

        // //if the countdown hits zero
        // if (knockbackTimeP2 <= 0) {
        //     //stop player from moving
        //     player2Body.velocity = Vector2.zero;
        //     //set bool to false; player is no longer hit. bool triggers timer reset
        //     player2Hit = false;
        //     //set back to kinematic; player is no longer affected by gravity
        //     player2Body.isKinematic = true;
        // }
    }

    //move function
    void Move(Vector3 direction) {
        transform.position += direction * speed;
    }

    //if the player collides with an enemy
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "player2") {
            Rigidbody2D player2Body = collision.GetComponent<Rigidbody2D>();
            Debug.Log("i hit player 2");
            //set kinematic to dynamic so force is applied (gravity on)
            player2Body.isKinematic = false;
            //force applied to the player to create knock back effect
            player2Body.AddForce(-transform.up, ForceMode2D.Impulse);
            // Vector2 difference = player2.transform.position - transform.position;
            // difference = difference.normalized * 4;
            // player2.AddForce(difference, ForceMode2D.Impulse);
            //set bool to true; player has been hit
            player2Hit = true;
        }
    }
}
