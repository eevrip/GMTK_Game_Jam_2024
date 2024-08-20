using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToNextLevel : MonoBehaviour
{

    public GameObject Manager;
    public GameObject fade;
    public int levelNum;
    public AudioSource playerEnter;
    public AudioSource loop;
    public GameObject player;
    public float maxVolumeDistance = 10f;
    public float minVolumeDistance = 2f;

    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("Manager");
        player = GameObject.FindGameObjectWithTag("Player");
        loop.Play();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            loop.Stop();
            playerEnter.Play();
            Manager.GetComponent<manager>().levels[levelNum] = true;
            Manager.GetComponent<manager>().SaveManager();

            fade.GetComponent<Animator>().SetTrigger("Fade");

            StartCoroutine(loadnextLevel());
        }
    }

    IEnumerator loadnextLevel()
    {
        yield return new WaitForSeconds(1);
        Player.IsNewLevelLoaded = true;
        int nextIdx = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIdx < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIdx);
        else
            SceneManager.LoadScene(0);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        AdjustFootstepVolume(distanceToPlayer);
    }

    private void AdjustFootstepVolume(float distanceToPlayer)
    {
        float volume = Mathf.InverseLerp(maxVolumeDistance, minVolumeDistance, distanceToPlayer);
        loop.volume = Mathf.Clamp(volume, 0f, 1f);
    }
}
