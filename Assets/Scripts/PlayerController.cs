using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public List<CategoryEnum.Category> categoriesToDo;
    public TextMeshProUGUI winText;
    public Image winImage;
    public Button nextButton;

    private List<bool> isComplete;
    private GameObject selectedItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isComplete = new List<bool>();
        foreach (CategoryEnum.Category catetory in categoriesToDo)
        {
            isComplete.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 ray = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray, Vector2.zero);
            bool hitItem = false;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Item"))
                {
                    selectedItem = hit.collider.gameObject;
                    selectedItem.GetComponent<BubbleItem>().startDragging();
                    hitItem = true;
                    break;
                }
            }
            if (!hitItem)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.CompareTag("Group"))
                    {
                        selectedItem = hit.collider.gameObject;
                        selectedItem.GetComponent<BubbleGroup>().startDragging();
                        hitItem = true;
                        break;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selectedItem != null && selectedItem.CompareTag("Item"))
            {
                selectedItem.GetComponent<BubbleItem>().stopDragging();
                selectedItem = null;
            }
            else if (selectedItem != null && selectedItem.CompareTag("Group"))
            {
                selectedItem.GetComponent<BubbleGroup>().stopDragging();
                selectedItem = null;
            }
        }
    }

    public void reportComplete(CategoryEnum.Category category)
    {
        int i = categoriesToDo.IndexOf(category);
        isComplete[i] = true;
        bool done = true;
        foreach (bool b in isComplete)
        {
            if (!b)
            {
                done = false;
            }
        }
        if (done)
        {
            winText.gameObject.SetActive(true);
            StartCoroutine(WinTimer());
        }
    }

    IEnumerator WinTimer()
    {
        yield return new WaitForSeconds(2);
        winImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        nextButton.gameObject.SetActive(true);
    }

    public void OnNextButtonClick()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene("Title");
        }
    }
}
