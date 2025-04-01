using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keeps GameManager alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ================================
    // 🚀 Start Game from Main Menu
    // ================================
    public void StartGame()
    {
        SceneManager.LoadScene("BuildingFire");  // Name of your main game scene
    }

    // ================================
    // 🚨 Trigger Mock Drill Failed
    // ================================
    public void TriggerMockDrillFailed()
    {
        Debug.Log("❌ Mock Drill Failed! Loading Failure Scene...");
        Invoke("LoadMockDrillFailedScene", 3f);  // Delays for 3 seconds
    }

    private void LoadMockDrillFailedScene()
    {
        SceneManager.LoadScene("MockDrillFailed");  // Name of failure scene
    }

    // ================================
    // 🎉 Trigger Mock Drill Success
    // ================================
    public void TriggerMockDrillSuccess()
    {
        Debug.Log("✅ Mock Drill Completed! Loading Success Scene...");
        Invoke("LoadMockDrillSuccessScene", 2f);  // Delays for 2 seconds
    }

    private void LoadMockDrillSuccessScene()
    {
        SceneManager.LoadScene("MockDrillSuccess");  // Name of success scene
    }

    // ================================
    // 🔁 Restart Game or Previous Scene
    // ================================
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // ================================
    // 🔙 Go Back to Main Menu
    // ================================
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // ================================
    // ❌ Quit Application
    // ================================
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
