using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class STart : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 0.5f;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        SceneManager.LoadScene("StartScreen");
    }
}