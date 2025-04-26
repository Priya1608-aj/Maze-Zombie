using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToStart : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start Scene"); // Must match the scene name exactly
    }
}
