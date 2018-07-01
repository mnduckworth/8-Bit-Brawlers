using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioClip[] levelMusicArray;

    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode)
    {
        AudioClip currentLevelMusic = levelMusicArray[scene.buildIndex];

        if (scene.name != "00 Splash")
            audioSource.volume = PlayerPrefsManager.GetMasterVolume();

        if (currentLevelMusic)
        {
            if(audioSource.clip != currentLevelMusic)
            {
                audioSource.clip = currentLevelMusic;
                audioSource.loop = true;
                audioSource.Play();
            }

        }
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
