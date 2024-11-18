using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2ButtonScript : MonoBehaviour
{
    // Method to load Scene 3
    public void GoToScene3()
    {
        // Load the scene named "Scene 3"
        SceneManager.LoadScene("Scene 3");
    }
}
