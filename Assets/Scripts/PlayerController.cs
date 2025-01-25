using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private GameObject selectedItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
