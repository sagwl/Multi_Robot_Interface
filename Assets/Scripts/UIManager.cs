using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI textMeshPro;
    public UnityEngine.UI.Button waypointmodebutton;
    public UnityEngine.UI.Button waypointmode_back_button;
    public UnityEngine.UI.Button overviewModeButton;
    public UnityEngine.UI.Button focusModeButton;
    public UnityEngine.UI.Button homeButton;
    public GameObject navigationPanel;
    public GameObject waypointPanel;
    public Camera mainCamera;
    public Transform cameraReferenceTransform;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        home();
    }

    // Update is called once per frame
    void Update()
    {
        // This part updates the Selection Menu with the selected items
        string text = "";
        int serialNumber = 0;
        foreach (KeyValuePair<int, GameObject> pair in selected_dictionary.selectedTable)
        {
            serialNumber += 1;
            text += serialNumber.ToString() + " " + pair.Value.name + "\n";
        }
        textMeshPro.SetText(text);

        // This part is to enable waypoint mode when the waypoint button is pressed
        waypointmodebutton.onClick.AddListener(() =>
        {
            navigationPanel.SetActive(false);
            waypointPanel.SetActive(true);
        });

        // This part is to disable waypoint mode when the waypoint back button is pressed
        waypointmode_back_button.onClick.AddListener(() =>
        {
            waypointPanel.SetActive(false);
            navigationPanel.SetActive(true);
        });

        // This part is to reset camera position when overview button is pressed
        overviewModeButton.onClick.AddListener(() =>
        {
            mainCamera.transform.position = cameraReferenceTransform.position;
            mainCamera.transform.rotation = cameraReferenceTransform.rotation;
        });

        // This part is to focus on 1 selected object
        focusModeButton.onClick.AddListener(CameraFocus);

        // This part is to reset UI and camera when Home button is pressed
        homeButton.onClick.AddListener(home);
    }

    // Home configuration of camera and UI
    void home()
    {
        // Setting Navigation Panel Active
        navigationPanel.SetActive(true);

        // Setting Waypoint Panel Inactive
        waypointPanel.SetActive(false);

        // Resetting camera
        mainCamera.transform.position = cameraReferenceTransform.position;
        mainCamera.transform.rotation = cameraReferenceTransform.rotation;
    }

    // Function to Focus Camera on 1 selected object
    void CameraFocus()
    {
        if (selected_dictionary.selectedTable.Count == 1)
        {
            foreach (KeyValuePair<int, GameObject> pair in selected_dictionary.selectedTable)
            {
                target = pair.Value.transform;
            }
        }
        Vector3 pointOnside = target.position + new Vector3(target.localScale.x * 1f, target.localScale.x * 1f, target.localScale.z * 1f);
        float aspect = (float)Screen.width / (float)Screen.height;
        float maxDistance = (target.localScale.y * 0.5f) / Mathf.Tan(Mathf.Deg2Rad * (Camera.main.fieldOfView / aspect));
        Camera.main.transform.position = Vector3.MoveTowards(pointOnside, target.position, -maxDistance);
        Camera.main.transform.LookAt(target.position);
    }
}
