using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoad : MonoBehaviour
{
    //[SerializeField] private GameObject s1;
    //[SerializeField] private GameObject s2;

    private float speed = 0.5f;
    private GameObject tile;

    private void Start()
    {
        Move();
       // tile = s1;
    }

    private void Move()
    {
        //GetComponent<Rigidbody2D>().velocity = transform.up * -1 * speed;
    }

    private void Update()
    {
    }
}
