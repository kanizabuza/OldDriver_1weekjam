using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    [SerializeField] private int scoreValue = 0;
    public int ScoreValue => scoreValue;

    private float speed = 1f;

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * -1 * speed;
    }
}
