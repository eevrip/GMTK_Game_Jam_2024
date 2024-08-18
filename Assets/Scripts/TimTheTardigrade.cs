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

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toDesiredPosition = player.transform.position + timsDesiredOffset - transform.position;
        toDesiredPosition.z = 0;

        currDeviationAngle += Random.Range(-randomAngleDeviation, randomAngleDeviation) * deviationSpeed * Time.deltaTime;
        currDeviationAngle = Mathf.Clamp(currDeviationAngle, -randomAngleDeviation, randomAngleDeviation);
        toDesiredPosition = Quaternion.AngleAxis(currDeviationAngle, Vector3.forward) * toDesiredPosition;
        if (toDesiredPosition.magnitude > 1 )
        {
            toDesiredPosition.Normalize();
        }
        transform.position += toDesiredPosition * speed * Time.deltaTime;
    }
}
