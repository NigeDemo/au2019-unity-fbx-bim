using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A component which can be added to a Camera to let the user to click on an imported
/// Vim game object and output its Bim data to the debug log or to the provided BimDataText component.
/// </summary>
[ExecuteAlways]
public class BimDataClicker : MonoBehaviour
{   
    private Camera _camera;
    private GameObject _lastSelected;
 
    public Text ObjectName;
    public string[] BimData = NoStrings;

    public static string[] NoStrings = new string[0];
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

        RaycastHit hit;
        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (ObjectName != null)
                ObjectName.text = "nothing selected";
            BimData = NoStrings;
            return;
        }

        var go = hit.collider.gameObject;

        if (go == _lastSelected)
            return;
        _lastSelected = go;

        go.TryGetComponent<BimData>(out var bimdata);

        if (bimdata == null)
        {
            BimData = NoStrings;
            return;
        }

        BimData = bimdata.Items;
        ObjectName.text = go.name;

        ParseBIMData(bimdata);
    }

    void ParseBIMData(BimData bimData)
    {
        Debug.Log($"ParseBIMData called with BimData of {bimData.Items.Length} length");

        for(int i = 0; i < bimData.Items.Length-1; i++)
        {
            string[] item = bimData.Items[i].Split('=');
            Debug.Log($"Data type: {item[0]} = {item[1]}");
        }
    }
}
