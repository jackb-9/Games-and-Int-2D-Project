
using System.Collections;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public Animator animator;
    public string animationTrigger = "Play";
    public AudioSource song;

    [Tooltip("Time before the four objects appear.")]
    public float performanceDuration = 5f;

    public GameObject[] objectsToCollect = new GameObject[4];
    public GameObject door;
    public GameObject sax;

    private bool started;

    private void Start()
    {
        foreach (GameObject obj in objectsToCollect)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (started || !other.CompareTag("Player"))
            return;

        started = true;
        StartCoroutine(RunChallenge());
    }

    private IEnumerator RunChallenge()
    {
        if (animator != null)
            animator.SetTrigger(animationTrigger);

        if (song != null)
            song.Play();

        sax.SetActive(true);

        yield return new WaitForSeconds(performanceDuration);

        foreach (GameObject obj in objectsToCollect)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        // Destroyed Unity objects compare equal to null.
        yield return new WaitUntil(AllObjectsCollected);

        if (door != null)
            door.SetActive(false);
        sax.SetActive(false);
    }

    private bool AllObjectsCollected()
    {
        foreach (GameObject obj in objectsToCollect)
        {
           
            if (obj != null)
                return false;
        }

        return true;
    }
}

