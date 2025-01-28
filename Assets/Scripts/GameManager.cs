using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isSoundOn = true;
    public AudioSource bubbleComplete;
    public AudioSource buttonPressSource;
    public AudioSource bubbleSoundSource;
    public AudioSource itemSelectSource;
    public AudioSource itemDeselectSource;
    public AudioSource levelComplete;
    public AudioClip[] buttonPress;
    public AudioClip[] bubbleForm2;
    public AudioClip[] bubbleForm3;
    public AudioClip[] bubbleForm4;
    public AudioClip[] bubblePop;
    public AudioClip itemDeselect;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBubbleComplete()
    {
        bubbleComplete.Play();
    }

    public void PlayButtonPress()
    {
        buttonPressSource.clip = buttonPress[Random.Range(0, buttonPress.Length)];
        buttonPressSource.Play();
    }

    public void PlayBubbleForm2()
    {
        bubbleSoundSource.clip = bubbleForm2[Random.Range(0, bubbleForm2.Length)];
        bubbleSoundSource.Play();
    }

    public void PlayBubbleForm3()
    {
        bubbleSoundSource.clip = bubbleForm3[Random.Range(0, bubbleForm3.Length)];
        bubbleSoundSource.Play();
    }

    public void PlayBubbleForm4()
    {
        bubbleSoundSource.clip = bubbleForm4[Random.Range(0, bubbleForm4.Length)];
        bubbleSoundSource.Play();
    }

    public void PlayBubblePop()
    {
        bubbleSoundSource.clip = bubblePop[Random.Range(0, bubblePop.Length)];
        bubbleSoundSource.Play();
    }

    public void PlayItemSelect(AudioClip clip)
    {
        itemSelectSource.clip = clip;
        itemSelectSource.Play();
    }

    public void PlayItemDeselect()
    {
        itemDeselectSource.clip = itemDeselect;
        itemDeselectSource.Play();
    }

    public void PlayLevelComplete()
    {
        AudioSource mainSource = Instance.GetComponent<AudioSource>();
        mainSource.Stop();
        levelComplete.Play();
        mainSource.PlayDelayed(0.536f * 10);
    }
}
