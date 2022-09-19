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
        //knockbackTimeP2 = 0.6f;
        player2Hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if player 1 presses W
        if (Input.GetKey(KeyCode.W)) {
            if (transform.position.y < 5f) {
                //then player 1 will move up
                Move(Vector3.up);
            }
        }
        //else if player 1 presses S
        else if (Input.GetKey(KeyCode.S)) {
            //and if the player's y position is above the bottom of the screen
            if (transform.position.y > -4.6f) {
                //then player 1 will move down
                Move(Vector3.down);
            }
        }

        if (Input.GetKey(KeyCode.D)) {
            if (transform.position.x < 6.6f) {
                Move(Vector3.right);
            }
        }
        else if (Input.GetKey(KeyCode.A)) {
            if (transform.position.x > -6f) {
                Move(Vector3.left);
            }
        }
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
    }
}
