using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    float horizontalInput;
    float moveSpeed = 288f;
    bool isFacingRight = false;

    Rigidbody2D rb;

    private Vector3 respawnPoint; //store player position;
    public GameObject fallDetector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        respawnPoint = transform.position;//initial position of player;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");//move AD or left and right key;

        FlipSprite();

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void FlipSprite()//Flip the sprite
    {
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if(collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//in the build settings, there is index for each scenes;
            respawnPoint = transform.position;//Update the respawn point in the scene;
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//For specific index number of that scene -> use SceneManager.LoadScene(index number(like 0));
            respawnPoint = transform.position;
        }
    }

}
