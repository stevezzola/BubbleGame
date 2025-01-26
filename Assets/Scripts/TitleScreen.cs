using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Image speaker;
    public GameObject helpPanel;
    public Sprite soundOn;
    public Sprite soundOff;

    private bool isSoundOn = true;

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
        isSoundOn = !isSoundOn;
        if (isSoundOn)
        {
            speaker.sprite = soundOn;
        }
        else
        {
            speaker.sprite = soundOff;
        }
    }
}
