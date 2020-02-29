using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private int scoreValue = 0;
    public int ScoreValue => scoreValue;

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

    protected Vector2 MoveLeftAndRight(Vector2 enemyPosition,Vector2 playerPos, float speed) 
    {
        Vector3 position =  Vector3.MoveTowards(enemyPosition, new Vector3(playerPos.x, playerPos.y,-1), speed * Time.deltaTime);
        return position;
    }

    public void StopMove()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * 0;
    }
}
