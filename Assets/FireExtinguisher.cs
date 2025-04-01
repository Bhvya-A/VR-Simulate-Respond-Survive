using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireExtinguisher : MonoBehaviour
{
    public ParticleSystem sprayParticles;  // Assign in Inspector
    public float maxSprayTime = 15f;  // Maximum spray time
    private float remainingSprayTime;
    private bool isSpraying;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    public bool isCorrectExtinguisher = true;  // ✅ Tick in Inspector for correct one, Untick for wrong one

    private GameObject currentFireTarget;  // Current fire being sprayed

    private void Start()
    {
        remainingSprayTime = maxSprayTime;

        // Get XRGrabInteractable from parent
        grabInteractable = GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("❌ XRGrabInteractable NOT found! Check if extinguisher has XRGrabInteractable.");
        }
    }

    private void Update()
    {
        if (isSpraying && remainingSprayTime > 0)
        {
            remainingSprayTime -= Time.deltaTime;

            // Check if spraying at fire
            if (currentFireTarget != null)
            {
                ExtinguishFire(currentFireTarget);  // Instantly extinguish
            }
        }

        // Stop spray if time runs out
        if (remainingSprayTime <= 0 && isSpraying)
        {
            StopSpray();
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!isCorrectExtinguisher)
        {
            Debug.LogError("❌ Wrong Extinguisher Selected! Mock Drill Failed!");
            GameManager.instance.TriggerMockDrillFailed();
            return;
        }

        Debug.Log("🖐️ Correct Extinguisher Grabbed!");
        StartSpray();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        StopSpray();
    }

    public void StartSpray()
    {
        if (remainingSprayTime > 0 && !sprayParticles.isPlaying)
        {
            sprayParticles.Play();
            isSpraying = true;
        }
    }

    public void StopSpray()
    {
        sprayParticles.Stop();
        isSpraying = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSpraying && other.CompareTag("Fire"))
        {
            currentFireTarget = other.gameObject;
            ExtinguishFire(currentFireTarget);  // Extinguish instantly
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentFireTarget)
        {
            currentFireTarget = null;
        }
    }

    private void ExtinguishFire(GameObject fireObject)
    {
        if (fireObject != null)
        {
            fireObject.SetActive(false);  // Instantly deactivate fire
            Debug.Log($"🔥 Fire extinguished: {fireObject.name}!");
        }
    }
}
