using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // The index of the scene you want to load
    public int sceneIndex;

    // Method to be called when the button is clicked
    public void LoadNewScene()
    {
        // Load the scene by index
        SceneManager.LoadScene(sceneIndex);
    }
}
