using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health
    private float currentHealth;    // Current health

    public float fireDamagePerSecond = 10f;  // Damage per second from fire
    private bool isTakingDamage = false;

    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"🏃 Player health initialized: {currentHealth}/{maxHealth}");
    }

    // ================================
    // 🔥 Player enters fire zone
    // ================================
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire") && !isTakingDamage)
        {
            Debug.Log("🔥 Player entered fire zone! Damage starting...");
            isTakingDamage = true;
            InvokeRepeating(nameof(ApplyFireDamage), 0f, 1f);  // Correctly calling ApplyFireDamage()
        }
    }

    // ================================
    // 🚪 Player exits fire zone
    // ================================
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fire") && isTakingDamage)
        {
            Debug.Log("🏃 Player exited fire zone! Damage stopped.");
            isTakingDamage = false;
            CancelInvoke(nameof(ApplyFireDamage));  // Correctly cancel damage
        }
    }

    // ================================
    // 🔥 Apply Fire Damage
    // ================================
    private void ApplyFireDamage()
    {
        TakeDamage(fireDamagePerSecond);
    }

    // ================================
    // 💥 Take Damage Logic
    // ================================
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"🔥 Player taking damage! Health: {currentHealth:F2}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ================================
    // ❌ Player Dies - Mock Drill Fails
    // ================================
    private void Die()
    {
        Debug.Log("❌ Player's health reached zero! Mock Drill Failed.");
        GameManager.instance.TriggerMockDrillFailed();  // Load failure scene
    }
}
