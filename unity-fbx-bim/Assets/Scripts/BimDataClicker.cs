using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// A component which can be added to a Camera to let the user to click on an imported
/// Vim game object and output its Bim data to the debug log or to the provided BimDataText component.
/// </summary>
[ExecuteAlways]
public class BimDataClicker : MonoBehaviour
{
    private static string[] NoStrings = new string[0];

    private Camera _camera;
    private GameObject _lastSelected;
    private string[] BimData = NoStrings;

    [SerializeField]
    private TextMeshProUGUI objectName;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        // Check if the left mouse button is down without Ctrl or Alt.
        if (Input.GetMouseButtonDown(0)) // && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftControl))
        {
            CheckRayCast();
        }
    }

    void CheckRayCast()
    {

        if (_camera == null)
            return;

        RaycastHit hit; // this variable will hold the object that was hit by the raycast

        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit)) // if the raycast from the mouse position hits nothing (Remember an exclamation mark means NOT in coding)
        {
            if (objectName != null)
                objectName.text = "Nothing Selected";
            BimData = NoStrings;
            return;
        }

        var go = hit.collider.gameObject; // we've selected sopmething so grab the game object from 'hit'

        if (go == _lastSelected) // check to see if we have clicked on the same object again - if we have then return ...
            return;

        _lastSelected = go; // Assign the selected object to the lastSelected variable

        go.TryGetComponent<BimData>(out var bimdata); // retrieve the BIMData component from the selected object - if one exists ...

        if (bimdata == null) // if there is no BIM data then return ...
        {
            BimData = NoStrings;
            return;
        }
        /*
        If we get to this part of the function then we have clicked on an object that has a BIMData component which contains BIM data ...
        */
        BimData = bimdata.Items;
        objectName.text = go.name;

        ParseBIMData(bimdata);
    }

    /// <summary>
    /// Parse the data from a BIMData component
    /// </summary>
    /// <param name="bimData"></param>
    void ParseBIMData(BimData bimData)
    {
        Debug.Log($"ParseBIMData called with BimData of {bimData.Items.Length} length");

        // Loop through all the BIM properties / data entries 
        for(int i = 0; i < bimData.Items.Length-1; i++)
        {
            string[] item = bimData.Items[i].Split('=');
            var property = item[0].Trim();
            var val = item[1].Trim();
            Debug.Log($"Data type: {property} = {val}");

            // Example for extracting specific properties ...
            if (property == "Area")
                Debug.Log("FOUND THE AREA PROPERTY!");
            if (property == "Family and Type")
                Debug.Log("FOUND FAMILY & TYPE PROPERTY!");
        }
    }
}
