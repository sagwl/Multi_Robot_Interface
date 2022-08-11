using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public List<GameObject> selectedList; //list to store all the selected objects

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
        // Using raycast (ray from mouse position to object) to determine which object to select
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            var hitObject = hit.collider.gameObject;
            var outline = hitObject.GetComponent<Outline>();
            //When the ray hits an object and left mouse button is pressed, outline the object and store it in the list if it is not already there
            if (selectionRenderer != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (outline != null)
                    {
                        outline.enabled = true;
                        if (!selectedList.Contains(hitObject))
                        {
                            selectedList.Add(hit.collider.gameObject);
                        }
                    }
                }
            }
        }

        //If right mouse button is pressed, remove outline from selected objects and clean the list of selected objects
        if (Input.GetMouseButtonDown(1))
        {
            foreach (var item in selectedList)
            {
                if (item.GetComponent<Outline>() != null)
                {
                    item.GetComponent<Outline>().enabled = false;
                }
            }
            selectedList.Clear();
        }
    }
}
