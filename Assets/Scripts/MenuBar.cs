using UnityEngine;
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
        if (GameManager.Instance == null || GameManager.Instance.isSoundOn)
        {
            speaker.sprite = soundOn;
        }
        else
        {
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
        GameManager.Instance.isSoundOn = !GameManager.Instance.isSoundOn;
        if (GameManager.Instance.isSoundOn)
        {
            speaker.sprite = soundOn;
        }
        else
        {
            speaker.sprite = soundOff;
        }
    }

    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene("Title");
    }
}
