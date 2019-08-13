using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject previousPanel;
    public GameObject newPanel;

    public AudioSource optionsAudio;
    public AudioSource optionsSFX;

    public AudioClip sfxTest;
    public AudioClip musicTest;

    public AudioMixer BGM_audioMixer;
    public AudioMixer SFX_audioMixer;
    public AudioMixer Step_audioMixer;
    public AudioMixer rattle_audioMixer;

    public Toggle toggleMusic;

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadNewPanel()
    {
        Debug.Log("oi");
        previousPanel.SetActive(false);
        newPanel.SetActive(true);
    }

    public void SetMusicVolume(float volume)
    {
        BGM_audioMixer.SetFloat("Volume", volume);
    }

    public void PlayMusicToTest()
    {
        optionsAudio.clip = musicTest;
        optionsAudio.Play();
        if (!toggleMusic.isOn)
        {
            optionsAudio.Stop();
        }
    }

    public void SetSFXVolume(float volume)
    {
        SFX_audioMixer.SetFloat("SFXVolume", volume);
        Step_audioMixer.SetFloat("StepVolume", volume);
        rattle_audioMixer.SetFloat("RattleVolume", volume);
    }

    public void PlaySFXToTest()
    {
        optionsSFX.clip = sfxTest;
        optionsSFX.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
