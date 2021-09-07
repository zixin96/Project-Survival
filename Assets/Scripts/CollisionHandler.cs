using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2.0f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    AudioSource mAudioSource;

    bool isTransitioning = false;

    void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning) { return; }

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Hit");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        // TODO: Add SFX upon crash
        isTransitioning = true;
        mAudioSource.Stop();
        mAudioSource.PlayOneShot(crashSound);
        // TODO: Add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartNextLevelSequence()
    {
        // TODO: Add SFX upon success
        isTransitioning = true;
        mAudioSource.Stop();
        mAudioSource.PlayOneShot(successSound);
        // TODO: Add particle effect upon success
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex % SceneManager.sceneCountInBuildSettings);
    }
}
