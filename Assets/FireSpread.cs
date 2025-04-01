using UnityEngine;
using System.Collections;

public class FireSpread : MonoBehaviour
{
    [System.Serializable]
    public class FloorFire
    {
        public GameObject[] subFires;
        public float spreadDelay = 3f;
    }

    public FloorFire[] floors;
    public float floorSpreadDelay = 5f;
    public float reigniteDelay = 10f;
    public float damagePerSecond = 5f;  // Damage dealt per second to the player

    private void Start()
    {
        StartCoroutine(SpreadFire());
    }

    IEnumerator SpreadFire()
    {
        // Spread fire upwards from Floor 1 to higher floors
        for (int i = 0; i < floors.Length; i++)
        {
            // Activate sub-fires for the current floor
            for (int j = 0; j < floors[i].subFires.Length; j++)
            {
                floors[i].subFires[j].SetActive(true);
                Debug.Log($"🔥 Fire started on Floor {i + 1}!");
                yield return new WaitForSeconds(floors[i].spreadDelay);
            }

            yield return new WaitForSeconds(floorSpreadDelay);
        }
    }

    // Instantly extinguish fire when spray hits
    public void ExtinguishFire(GameObject fireObject)
    {
        if (fireObject != null)
        {
            fireObject.SetActive(false);
            Debug.Log($"✅ Fire extinguished: {fireObject.name}");
        }
        else
        {
            Debug.LogWarning("❌ Tried to extinguish null object!");
        }
    }

    // Damage the player if they stay in the fire
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime);  // Gradual damage
            }
        }
    }
}
