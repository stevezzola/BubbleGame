using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BubbleGroup : MonoBehaviour
{
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;

    private bool dragging = false;
    private Vector3 offset;
    public List<GameObject> bubbleItemObjects;
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
            //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }   
    }

    private void OnMouseDown()
    {
        //offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        Debug.Log("Clicked on a bubble!");
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    public void addItem(GameObject bubbleItemObject)
    {
        if (nItems < 4)
        {
            bubbleItemObjects.Add(bubbleItemObject);
            bubbleItemObject.GetComponent<BubbleItem>().setBubbleGroupParent(gameObject);
            //RelativeJoint2D joint = bubbleItemObject.GetComponent<RelativeJoint2D>();
            //joint.connectedBody = gameObject.GetComponent<Rigidbody2D>();
            //joint.autoConfigureOffset = false;
            nItems += 1;
        }
        readjustItems();
    }

    public void removeItem(GameObject bubbleItemObject)
    {
        if (nItems > 0)
        {
            bubbleItemObject.GetComponent<BubbleItem>().setBubbleGroupParent(null);
            bubbleItemObjects.Remove(bubbleItemObject);
            //RelativeJoint2D joint = bubbleItemObject.GetComponent<RelativeJoint2D>();
            //joint.autoConfigureOffset = true;
            //joint.connectedBody = null;
            nItems -= 1;
            if (nItems < 2)
            {
                foreach (GameObject child in bubbleItemObjects)
                {
                    bubbleItemObject.GetComponent<BubbleItem>().setBubbleGroupParent(null);
                    //RelativeJoint2D childJoint = bubbleItemObject.GetComponent<RelativeJoint2D>();
                    //childJoint.connectedBody = null;
                    //childJoint.autoConfigureOffset = true;
                    nItems -= 1;
                }
                Destroy(gameObject);
            }
            readjustItems();
        }
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
            bubbleItemObjects[0].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(1.0f, -0.75f);
            bubbleItemObjects[1].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-1.0f, -0.75f);
            bubbleItemObjects[2].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(0.0f, 1.25f);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite1;
            gameObject.GetComponent<CircleCollider2D>().radius = 2.2f;
        }
        else if (nItems == 4)
        {
            bubbleItemObjects[0].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(1.0f, -1.0f);
            bubbleItemObjects[1].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-1.0f, -1.0f);
            bubbleItemObjects[2].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(1.0f, 1.0f);
            bubbleItemObjects[3].GetComponent<RelativeJoint2D>().linearOffset = new Vector2(-1.0f, 1.0f);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite2;
            gameObject.GetComponent<CircleCollider2D>().radius = 2.3f;
        }
    }
}
