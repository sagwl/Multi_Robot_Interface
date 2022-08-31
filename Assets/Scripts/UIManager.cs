using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI textMeshPro;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string text = "";
        int serialNumber = 0;
        foreach (KeyValuePair<int, GameObject> pair in selected_dictionary.selectedTable)
        {
            serialNumber += 1;
            text +=serialNumber.ToString() + " " + pair.Value.name + "\n";
        }
        textMeshPro.SetText(text);
        //addMenuItem(selected_dictionary.selectedTable);       
    }
}