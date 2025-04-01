using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LiftButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player interacts with the lift button
        if (other.CompareTag("Player"))
        {
            // Trigger the Mock Drill Failed screen after 3 seconds
            GameManager.instance.TriggerMockDrillFailed();
        }
    }
}
