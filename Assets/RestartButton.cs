using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.RestartScene();  // Restart if GameManager exists
        }
        else
        {
            Debug.LogError("GameManager instance is NULL. Check if GameManager is in the scene.");
        }
    }
}
