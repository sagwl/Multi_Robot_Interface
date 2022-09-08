using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionList : MonoBehaviour
{
    public static List<GameObject> selectedList = new List<GameObject>();
    // Start is called before the first frame update
    public void addtoSelected(GameObject go)
    {
        if (selectedList.Contains(go) == false)
        {
            selectedList.Add(go);
            if (go.GetComponent<Outline>() == null)
            {
                go.AddComponent<Outline>();
            }
            go.GetComponent<Outline>().OutlineColor = Color.red;
            go.GetComponent<Outline>().OutlineWidth = 12f;
        }
    }

    public void deselectList()
    {
        if (selectedList.Count != 0)
        {
            foreach (GameObject item in selectedList)
            {
                
                if (item.GetComponent<Outline>() != null)
                {
                    Destroy(item.GetComponent<Outline>());
                }
            }
            selectedList.Clear();
        }
        
    }
}
