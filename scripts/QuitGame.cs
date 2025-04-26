using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void ExitScene()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }
}
