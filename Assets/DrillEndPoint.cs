using UnityEngine;

public class DrillEndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure Player is tagged properly
        {
            Debug.Log("✅ Player reached end of mock drill! Success triggered.");
            GameManager.instance.TriggerMockDrillSuccess();  // Call success function
        }
    }
}
