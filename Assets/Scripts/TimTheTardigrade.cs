using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimTheTardigrade : MonoBehaviour
{
    private Player player;
    [SerializeField]
    public Vector3 timsDesiredOffset;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float randomAngleDeviation;
    [SerializeField]
    private float deviationSpeed = 5f;
    private float currDeviationAngle;

    public bool canMove;
    public bool isUp;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            
            Vector3 toDesiredPosition = player.transform.position + timsDesiredOffset - transform.position;
            toDesiredPosition.z = 0;

            currDeviationAngle += Random.Range(-randomAngleDeviation, randomAngleDeviation) * deviationSpeed * Time.deltaTime;
            currDeviationAngle = Mathf.Clamp(currDeviationAngle, -randomAngleDeviation, randomAngleDeviation);
            toDesiredPosition = Quaternion.AngleAxis(currDeviationAngle, Vector3.forward) * toDesiredPosition;
            if (toDesiredPosition.magnitude > 1)
            {
                toDesiredPosition.Normalize();
            }
            transform.position += toDesiredPosition * speed * Time.deltaTime;
        }
    }

    public void PlayTalking()
    {
        anim.SetBool("talking", true);
    }

    public void EndTalking()
    {
        anim.SetBool("talking", false);
    }
}
