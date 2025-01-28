using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBar : MonoBehaviour
{
    public Image speaker;
    public GameObject helpPanel;
    public Sprite soundOn;
    public Sprite soundOff;

    private void Start()
    {
        if (GameManager.Instance.isSoundOn)
        {
            GameManager.Instance.ToggleSound(true);
            speaker.sprite = soundOn;
        }
        else
        {
            GameManager.Instance.ToggleSound(false);
            speaker.sprite = soundOff;
        }
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnHelpButtonClick()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
    }

    public void OnSoundButtonClick()
    {
        if (GameManager.Instance.isSoundOn)
        {
            GameManager.Instance.ToggleSound(false);
            speaker.sprite = soundOff;
        }
        else
        {
            GameManager.Instance.ToggleSound(true);
            speaker.sprite = soundOn;
        }
    }

    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene("Title");
        GameManager.Instance.PlayButtonPress();
    }
}
