using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float timer, stopTime;
    public float clock;
    public Vector3 moveDirection;

    void Start()
    {
        clock = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= stopTime)
        {
            moveDirection *= -1;
            timer = 0;
        }
        Move();
    }

    void Move()
    {
        transform.position += moveDirection * Time.deltaTime;
    }
}
