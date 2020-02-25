using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected private int score;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Move(float speed)
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * -1 * speed;
    }

    protected void MoveLeftAndRight(Vector2 direction) 
    {
        GetComponent<Rigidbody2D>().velocity = direction;
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), Speed * Time.deltaTime);

    }
}
