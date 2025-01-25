using UnityEngine;
using UnityEngine.EventSystems;


public class BubbleItem : MonoBehaviour
{
    public GameObject bubbleGroupPrefab;

    private bool dragging = false;
    private Vector3 offset;
    private GameObject otherItemObject;
    private GameObject bubbleGroupParent = null;
    private bool withinParent = true;
    private Vector2 startPosition;

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
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition = transform.position;
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        Vector2 endPosition = transform.position;
        float d = (endPosition - startPosition).magnitude;
        if (otherItemObject != null && otherItemObject.CompareTag("Item") && !hasBubbleGroupParent() && !otherItemObject.GetComponent<BubbleItem>().hasBubbleGroupParent())
        {
            Debug.Log("Adding");
            // Creating a new bubble with two items
            GameObject bubbleGroupObject = Instantiate(bubbleGroupPrefab, transform.position, Quaternion.identity);
            BubbleGroup bubbleGroup = bubbleGroupObject.GetComponent<BubbleGroup>();
            bubbleGroup.addItem(gameObject);
            bubbleGroup.addItem(otherItemObject);
            /*
            RelativeJoint2D joint = gameObject.GetComponent<RelativeJoint2D>();
            joint.connectedBody = bubbleGroupObject.GetComponent<Rigidbody2D>();
            joint.autoConfigureOffset = false;
            joint.linearOffset = new Vector2(1.0f, 0.0f);

            RelativeJoint2D otherJoint = otherItemObject.GetComponent<RelativeJoint2D>();
            otherJoint.connectedBody = bubbleGroupObject.GetComponent<Rigidbody2D>();
            otherJoint.autoConfigureOffset = false;
            otherJoint.linearOffset = new Vector2(-1.0f, 0.0f);
            
            setBubbleGroupParent(bubbleGroupObject);
            otherItemObject.GetComponent<BubbleItem>().setBubbleGroupParent(bubbleGroupObject);
            */
        }
        else if (hasBubbleGroupParent() && d > 1.5)
        {
            Debug.Log("Removing");
            BubbleGroup bubbleGroup = bubbleGroupParent.GetComponent<BubbleGroup>();
            bubbleGroup.removeItem(gameObject);
        }
        else if (otherItemObject != null && otherItemObject.CompareTag("Group") && !hasBubbleGroupParent())
        {
            Debug.Log("Adding to bubble!");
            BubbleGroup bubbleGroup = otherItemObject.GetComponent<BubbleGroup>();
            bubbleGroup.addItem(gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        otherItemObject = collision.gameObject;
        if (hasBubbleGroupParent() && collision.gameObject == bubbleGroupParent)
        {
            withinParent = true;
            Debug.Log("Entered parent!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hasBubbleGroupParent() && collision.gameObject == bubbleGroupParent)
        {
            withinParent = false;
            Debug.Log("Left parent!");
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

}
