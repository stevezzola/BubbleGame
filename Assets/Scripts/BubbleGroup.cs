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

    private CategoryEnum.Category category;
    private bool dragging = false;
    private Vector3 offset;
    private List<GameObject> bubbleItemObjects;
    private int nItems = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
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
            gameObject.GetComponent<CircleCollider2D>().radius = 1.9f;
        }
        else if (nItems == 3)
        {
            bubbleItemObjects[0].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.75f, -0.5f);
            bubbleItemObjects[1].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-0.75f, -0.5f);
            bubbleItemObjects[2].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.0f, 1.0f);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
            gameObject.GetComponent<CircleCollider2D>().radius = 2.2f;
        }
        else if (nItems == 4)
        {
            bubbleItemObjects[0].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.75f, -0.75f);
            bubbleItemObjects[1].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-0.75f, -0.75f);
            bubbleItemObjects[2].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.75f, 0.75f);
            bubbleItemObjects[3].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-0.75f, 0.75f);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
            gameObject.GetComponent<CircleCollider2D>().radius = 2.3f;
        }
    }

    public CategoryEnum.Category getCategory()
    {
        return category;
    }
}
