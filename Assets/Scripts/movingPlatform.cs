using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB; 
    public float speed = 1.0f; 

    private bool movingToB = false;

    [SerializeField]private bool isMoving;
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    
    public static buttonController.ActivationItem typeOfActivation { get; private set; } = buttonController.ActivationItem.MovingPlatform;
    void Update()
    {
       if(isMoving)
        {
            Move();
        }
    }
    
    public void Move()
    {
        float step = speed * Time.deltaTime;

        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);

            if (Vector3.Distance(transform.position, pointB.position) < 0.001f)
            {
                movingToB = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, step);

            if (Vector3.Distance(transform.position, pointA.position) < 0.001f)
            {
                movingToB = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
