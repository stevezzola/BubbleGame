using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class BubbleGroup : MonoBehaviour
{
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite spritebg0;
    public Sprite spritebg1;
    public Sprite spritebg2;
    public SpriteRenderer bubbleBack;

    private CategoryEnum.Category category;
    private bool dragging = false;
    private Vector3 offset;
    private List<GameObject> bubbleItemObjects;
    private List<GameObject> otherItemObjects;
    private int nItems = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        otherItemObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            if (pos.x < 6.5 && pos.x > -6.5 && pos.y < 5.0 && pos.y > -5.0)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            }
        }
        if (transform.position.y > 10)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            foreach (GameObject child in bubbleItemObjects)
            {
                child.GetComponent<Rigidbody2D>().simulated = false;
            }
        }
    }

    public void startDragging()
    {
        if (nItems < 4)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
    }

    public void stopDragging()
    {
        dragging = false;
        mouseUpCheck();
    }

    public void mouseUpCheck()
    {
        if (otherItemObjects.Count > 0)
        {
            BubbleItem otherBubbleItem = otherItemObjects.Last().GetComponent<BubbleItem>();
            GameObject otherBubbleParent = otherBubbleItem.getBubbleGroupParent();
            if (otherBubbleParent != null && otherBubbleParent != gameObject)
            {
                BubbleGroup otherBubbleGroup = otherBubbleParent.GetComponent<BubbleGroup>();
                if (otherBubbleGroup.category == category)
                {
                    foreach (GameObject child in bubbleItemObjects)
                    {
                        otherBubbleGroup.addItem(child);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " entered " + collision.gameObject.name);
        if (collision.CompareTag("Item"))
        {
            otherItemObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " left " + collision.gameObject.name);
        if (collision.CompareTag("Item"))
        {
            otherItemObjects.Remove(collision.gameObject);
        }
    }

    public void addItem(GameObject bubbleItemObject)
    {
        if (nItems == 0)
        {
            bubbleItemObjects = new List<GameObject>();
        }
        if (nItems < 4)
        {
            bubbleItemObjects.Add(bubbleItemObject);
            bubbleItemObject.GetComponent<BubbleItem>().setBubbleGroupParent(gameObject);
            nItems += 1;
        }
        readjustItems();
        if (nItems == 1)
        {
            category = bubbleItemObject.GetComponent<BubbleItem>().category;
        }
        if (nItems == 4)
        {
            Debug.Log("Bubble completed!");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("FOUND! " + player.name);
            if (player != null)
            {
                player.GetComponent<PlayerController>().reportComplete(category);
            }
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
        }
    }

    public void removeItem(GameObject bubbleItemObject)
    {
        if (nItems > 0 && nItems < 4)
        {
            bubbleItemObject.GetComponent<BubbleItem>().setBubbleGroupParent(null);
            bubbleItemObjects.Remove(bubbleItemObject);
            nItems -= 1;
            if (nItems < 2)
            {
                foreach (GameObject child in bubbleItemObjects)
                {
                    child.GetComponent<BubbleItem>().setBubbleGroupParent(null);
                    nItems -= 1;
                }
                Destroy(gameObject);
            }
            else
            {
                readjustItems();
            }  
        }
    }

    public void removeAllItems()
    {
        foreach (GameObject child in bubbleItemObjects)
        {
            child.GetComponent<BubbleItem>().setBubbleGroupParent(null);
            nItems -= 1;
        }
        Destroy(gameObject);
    }

    public void readjustItems()
    {
        if (nItems == 2)
        {
            bubbleItemObjects[0].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.75f, 0.0f);
            bubbleItemObjects[1].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-0.75f, 0.0f);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite0;
            bubbleBack.sprite = spritebg0;
            gameObject.GetComponent<CircleCollider2D>().radius = 1.9f;
        }
        else if (nItems == 3)
        {
            bubbleItemObjects[0].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.75f, -0.5f);
            bubbleItemObjects[1].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-0.75f, -0.5f);
            bubbleItemObjects[2].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.0f, 1.0f);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
            bubbleBack.sprite = spritebg1;
            gameObject.GetComponent<CircleCollider2D>().radius = 2.2f;
        }
        else if (nItems == 4)
        {
            bubbleItemObjects[0].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.75f, -0.75f);
            bubbleItemObjects[1].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-0.75f, -0.75f);
            bubbleItemObjects[2].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.75f, 0.75f);
            bubbleItemObjects[3].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-0.75f, 0.75f);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
            bubbleBack.sprite = spritebg2;
            gameObject.GetComponent<CircleCollider2D>().radius = 2.3f;
        }
    }

    public CategoryEnum.Category getCategory()
    {
        return category;
    }
}
