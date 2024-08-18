using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlowUpEnemy : MonoBehaviour
{
    public GameObject player;
    public int speed = 5;
    public int damage = 8;
    private bool movingDown = false;

    private void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 3f)
        {
            movingDown = true;
        }

        if (movingDown)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 5)
        {
            // Respawn
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
