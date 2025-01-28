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
    public AudioSource itemSoundSource;
    public List<AudioClip> buttonPress;
    public List<AudioClip> bubbleForm2;
    public List<AudioClip> bubbleForm3;
    public List<AudioClip> bubbleForm4;
    public List<AudioClip> bubblePop;
    public List<AudioClip> itemSelect;
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
        buttonPressSource.clip = buttonPress[Random.Range(0, buttonPress.Count)];
        buttonPressSource.Play();
    }

    public void PlayBubbleForm2()
    {
        bubbleSoundSource.clip = bubbleForm2[Random.Range(0, bubbleForm2.Count)];
        bubbleSoundSource.Play();
    }

    public void PlayBubbleForm3()
    {
        bubbleSoundSource.clip = bubbleForm3[Random.Range(0, bubbleForm3.Count)];
        bubbleSoundSource.Play();
    }

    public void PlayBubbleForm4()
    {
        bubbleSoundSource.clip = bubbleForm4[Random.Range(0, bubbleForm4.Count)];
        bubbleSoundSource.Play();
    }

    public void PlayBubblePop()
    {
        bubbleSoundSource.clip = bubblePop[Random.Range(0, bubblePop.Count)];
        bubbleSoundSource.Play();
    }

    public void PlayItemDeselect()
    {
        itemSoundSource.clip = itemDeselect;
        itemSoundSource.Play();
    }
}
