using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatorolCar : BaseEnemy
{
    private bool moved = false;
    // Start is called before the first frame update
    void Start()
    {
        Move(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().isVisible) {
            if (moved) {

            }
        }
    }
}
