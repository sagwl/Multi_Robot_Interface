using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    selectionList sl;
    RaycastHit hit;
    bool dragSelect;
    public static bool selectionReady = true;

    //vaiables for storing mouse position
    Vector3 p1;
    Vector3 p2;



    // Function to print gameobjects within a list - for debugging
    void printList(List<GameObject> list)
    {
        foreach (var x in list)
        {
            Debug.Log(x.name);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        dragSelect = false;

        //This is very important to ge the instance
        sl = GetComponent<selectionList>();

        var allObjects = Object.FindObjectsOfType<GameObject>(); //variable to store the list of all objects in scene
        // Removing outline from all the objects in the scene at the start
        foreach (var item in allObjects)
        {
            if (item.GetComponent<Outline>() != null)
            {
                item.GetComponent<Outline>().enabled = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (selectionReady == true)
        {
            //Step 1 - Checking when left mouse is clicked and not released
            if (Input.GetMouseButtonDown(0))
            {
                p1 = Input.mousePosition;
            }

            //Step 2 - Checking if left mouse is still held down
            if (Input.GetMouseButton(0))
            {
                if ((p1 - Input.mousePosition).magnitude > 40)
                {
                    dragSelect = true;
                }
            }

            //Step 3 - Check when left mouse button released
            if (Input.GetMouseButtonUp(0))
            {
                if (dragSelect == false)
                {
                    Ray ray = Camera.main.ScreenPointToRay(p1);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (Input.GetKey(KeyCode.LeftShift))  // inclusive selection
                        {
                            sl.addtoSelected(hit.transform.gameObject);
                        }
                        else // exclusive selection
                        {
                            sl.deselectList();
                            sl.addtoSelected(hit.transform.gameObject); 
                        }
                    }
                    else //if we didn't select anything and clicked on ground
                    {
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            // do nothing
                        }
                        else
                        {
                            sl.deselectList();
                        }
                    }
                }
                else // drag select is true in this case
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        p2 = Input.mousePosition;

                        var allObjects = Object.FindObjectsOfType<GameObject>();
                        Vector2 min;
                        Vector2 max;

                        if (p1.x > p2.x)
                        {
                            max.x = p1.x;
                            min.x = p2.x;
                        }
                        else
                        {
                            max.x = p2.x;
                            min.x = p1.x;
                        }
                        if (p1.y > p2.y)
                        {
                            max.y = p1.y;
                            min.y = p2.y;
                        }
                        else
                        {
                            max.y = p2.y;
                            min.y = p1.y;
                        }


                        foreach (GameObject item in allObjects)
                        {
                            Vector3 screenPos = Camera.main.WorldToScreenPoint(item.transform.position);

                            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                            {
                                sl.addtoSelected(item);
                            }
                        }
                    }
                    else
                    {
                        sl.deselectList();
                        p2 = Input.mousePosition;

                        var allObjects = Object.FindObjectsOfType<GameObject>();
                        Vector2 min;
                        Vector2 max;

                        if (p1.x > p2.x)
                        {
                            max.x = p1.x;
                            min.x = p2.x;
                        }
                        else
                        {
                            max.x = p2.x;
                            min.x = p1.x;
                        }
                        if (p1.y > p2.y)
                        {
                            max.y = p1.y;
                            min.y = p2.y;
                        }
                        else
                        {
                            max.y = p2.y;
                            min.y = p1.y;
                        }


                        foreach (GameObject item in allObjects)
                        {
                            Vector3 screenPos = Camera.main.WorldToScreenPoint(item.transform.position);

                            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                            { 
                                sl.addtoSelected(item);
                            }
                        }
                    }
                }

                dragSelect = false;
            }
            // Step 4 - Ctrl A to select all
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    var allObjects = Object.FindObjectsOfType<GameObject>();
                    foreach (GameObject item in allObjects)
                    {
                        sl.addtoSelected(item);
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        if (dragSelect == true)
        {
            var rect = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

}
