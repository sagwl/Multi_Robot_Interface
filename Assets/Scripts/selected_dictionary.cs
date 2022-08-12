using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selected_dictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedTable.ContainsKey(id)))
        {
            selectedTable.Add(id, go);
            go.AddComponent<Outline>();
            go.GetComponent<Outline>().OutlineColor = Color.red;
            go.GetComponent<Outline>().OutlineWidth = 10f;
            Debug.Log("Added " + id + " to selected dict");
        }
    }

    public void deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<Outline>());
        selectedTable.Remove(id);
    }

    public void deselectAll()
    {
        foreach (KeyValuePair<int, GameObject> pair in selectedTable)
        {
            Destroy(selectedTable[pair.Key].GetComponent<Outline>());
            if (pair.Value != null)
            {
                Destroy(selectedTable[pair.Key].GetComponent<Outline>());
            }
        }
        selectedTable.Clear();
    }
}
