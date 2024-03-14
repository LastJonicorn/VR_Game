using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels; // Array to hold all the panels

    // Function to open a panel by name
    public void OpenPanel(string panelName)
    {
        // Loop through all panels
        foreach (GameObject panel in panels)
        {
            // Check if the panel's name matches the given name
            if (panel.name == panelName)
            {
                // Activate the panel
                panel.SetActive(true);
            }
            else
            {
                // Deactivate any other panels
                panel.SetActive(false);
            }
        }
    }

    // Function to close the active panel
    public void CloseActivePanel()
    {
        // Loop through all panels
        foreach (GameObject panel in panels)
        {
            // Check if the panel is active
            if (panel.activeSelf)
            {
                // Deactivate the panel
                panel.SetActive(false);
                return; // Exit the loop after closing the active panel
            }
        }
    }
}
