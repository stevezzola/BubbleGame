using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;


public class BubbleItem : MonoBehaviour
{
    public GameObject bubbleGroupPrefab;
    public CategoryEnum.Category category;

    private bool dragging = false;
    private Vector3 offset;
    private List<GameObject> otherItemObjects;
    private List<GameObject> otherGroupObjects;
    private GameObject bubbleGroupParent = null;
    private Vector2 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        otherItemObjects = new List<GameObject>();
        otherGroupObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }   
    }

    private void mouseUpCheck()
    {
        Vector2 endPosition = transform.position;
        float d = (endPosition - startPosition).magnitude;
        if (otherItemObjects.Count > 0 && !hasBubbleGroupParent() && !otherItemObjects.Last().GetComponent<BubbleItem>().hasBubbleGroupParent())
        {
            if (category == otherItemObjects.Last().GetComponent<BubbleItem>().category)
            {
                // Create a new bubble with two items
                Debug.Log("Adding");
                GameObject bubbleGroupObject = Instantiate(bubbleGroupPrefab, transform.position, Quaternion.identity);
                BubbleGroup bubbleGroup = bubbleGroupObject.GetComponent<BubbleGroup>();
                bubbleGroup.addItem(gameObject);
                bubbleGroup.addItem(otherItemObjects.Last());
            }
            else
            {
                // Reject bubble creation
                Debug.Log("Rejected!");
            }
        }
        else
        {
            if (hasBubbleGroupParent() && d > bubbleGroupParent.GetComponent<CircleCollider2D>().radius)
            {
                // Remove item from bubble
                Debug.Log("Removing from bubble");
                BubbleGroup bubbleGroup = bubbleGroupParent.GetComponent<BubbleGroup>();
                bubbleGroup.removeItem(gameObject);
            }
            if (otherGroupObjects.Count > 0 && !hasBubbleGroupParent())
            {
                if (category == otherGroupObjects.Last().GetComponent<BubbleGroup>().getCategory())
                {
                    // Add item to existing bubble
                    Debug.Log("Adding to bubble!");
                    BubbleGroup bubbleGroup = otherGroupObjects.Last().GetComponent<BubbleGroup>();
                    bubbleGroup.addItem(gameObject);
                }
                else
                {
                    // Wrong item - pop bubble! - Note: not fun without physics or animation
                    Debug.Log("Rejected!");
                    //BubbleGroup bubbleGroup = otherGroupObjects.Last().GetComponent<BubbleGroup>();
                    //bubbleGroup.removeAllItems();
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
        else if (collision.CompareTag("Group"))
        {
            otherGroupObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " left " + collision.gameObject.name);
        if (collision.CompareTag("Item"))
        {
            otherItemObjects.Remove(collision.gameObject);
        }
        else if (collision.CompareTag("Group"))
        {
            otherGroupObjects.Remove(collision.gameObject);
        }
    }

    public void setBubbleGroupParent(GameObject parent)
    {
        Debug.Log("Setting parent!");

        bubbleGroupParent = parent;
        RelativeJoint2D joint = gameObject.GetComponent<RelativeJoint2D>();
        if (parent != null)
        {
            joint.connectedBody = parent.GetComponent<Rigidbody2D>();
            joint.enabled = true;
        }
        else
        {
            joint.enabled = false;
            joint.connectedBody = null;
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }

    public bool hasBubbleGroupParent()
    {
        return (bubbleGroupParent != null);
    }

    public void startDragging()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition = transform.position;
        dragging = true;
    }

    public void stopDragging()
    {
        dragging = false;
        mouseUpCheck();
    }

}
