using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private AudioMixerGroup _audioMixer;

    private void Start()
    {
        Pause();
    }

    public void Inject(AudioMixerGroup audioMixer)
    {
        _audioMixer = audioMixer;
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        _audioMixer.audioMixer.SetFloat("MasterVolume", -80.0f);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        _audioMixer.audioMixer.SetFloat("MasterVolume", 0.0f);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
