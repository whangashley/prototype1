using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{

    /*Below are the links and sources used to help code this project!
    
    “How to make Frogger in Unity (Complete Tutorial)” by Zigurous
    Section Environment Loop
    https://www.youtube.com/watch?v=GxlxZ5q__Tc&list=PL3J5ZZINwCjTcqqOzdKPbZkA1KfnLsp8w&index=8&t=19s 
    “Enemy Knockback in Unity” by BliskenX
    https://www.youtube.com/watch?v=nHPXiRNaNoM*/
    
    //speed variable
    public float speed;

    //rigidbody variable called player2Body
    //Rigidbody2D player2Body;

    public float knockbackTimeP1;
    public bool player1Hit;

    // Start is called before the first frame update
    void Start()
    {
        //setting player 2's rigidbody into the variable
        //player2Body = gameObject.GetComponent<Rigidbody2D>();
        knockbackTimeP1 = 0.6f;
        player1Hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if player 2 presses up arrow key
        if (Input.GetKey(KeyCode.UpArrow)) {
            //then player 2 will move up
            Move(Vector3.up);
        }
        //else if player 2 presses down arrow key
        else if (Input.GetKey(KeyCode.DownArrow)) {
            //and if the player's y position is above the bottom of the screen
            // if (transform.position.y > -9.4f) {
            //     //then player 2 will move down
                Move(Vector3.down);
            // }
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            Move(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            Move(Vector3.left);
        }
    
        //if player is hit, then the timer will countdown
        if (player1Hit == true) {
            knockbackTimeP1 -= Time.deltaTime; 
        }
        //if the countdown finishes, player is not hit. reset the timer
        else if (player1Hit == false) {
            knockbackTimeP1 = 0.6f;             
        }

        // //if the countdown hits zero
        // if (knockbackTimeP1 <= 0) {
        //     //stop player from moving
        //     player1Body.velocity = Vector2.zero;
        //     //set bool to false; player is no longer hit. bool triggers timer reset
        //     player1Hit = false;
        //     //set back to kinematic; player is no longer affected by gravity
        //     player1Body.isKinematic = true;
        // }
    }

    //move function
    void Move(Vector3 direction) {
        transform.position += direction * speed;
    }
    
    //if the player hits an enemy
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "player1") {
            Rigidbody2D player1Body = collision.GetComponent<Rigidbody2D>();
            Debug.Log("i hit player 1");
            //set kinematic to dynamic so force is applied (gravity on)
            player1Body.isKinematic = false;
            //force applied downwards to the player to create knock back effect
            player1Body.AddForce(-transform.up, ForceMode2D.Impulse);
            //set bool to true; player has been hit
            player1Hit = true;
        }
    }
}
