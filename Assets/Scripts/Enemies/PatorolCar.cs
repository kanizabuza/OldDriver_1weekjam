using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatorolCar : BaseEnemy
{
    private bool moved = false;
    private float playerX = 5.0f;
    private Vector2 playerPos;
    private Vector2 direction;
    private bool isPlayerRight = true;
    private bool isStop = false;
    // Start is called before the first frame update
    void Start()
    {
        Move();
        direction = transform.right;
        if (transform.position.x > playerX) {
            direction = transform.right * -1;
            isPlayerRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (GetComponent<SpriteRenderer>().isVisible) {
            if (!moved) {
                playerPos = new Vector2(playerX, 0);
                transform.position = MoveLeftAndRight(transform.position ,playerPos, 1.0f);
                
                //moved = true;
            }
        }
        if (isPlayerRight) {
            if (transform.position.x > playerX && !isStop) {
                Debug.Log("1");
                GetComponent<Rigidbody2D>().velocity = direction * 0;
                isStop = true;
                //Move(0f);
                Move();
            }
        } else {
            if (transform.position.x < playerX && !isStop) {
                isStop = true;
                Debug.Log("0k");
                GetComponent<Rigidbody2D>().velocity = direction * 0;
            }
        }
    }
}
