using UnityEngine;

public class CouchInteraction : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";  // Tag of the player GameObject
    [SerializeField] private Transform cameraRig;  // Reference to the camera rig or player GameObject
    [SerializeField] private float sitHeightOffset = 1f;  // Offset to adjust camera rig position when sitting

    private bool playerNear = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Press X on your controller to sit.");

            // You can display UI text or a canvas to show the message
            playerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // Clear any UI prompt when the player moves away
            playerNear = false;
        }
    }

    void Update()
    {
        if (playerNear && OVRInput.GetDown(OVRInput.Button.One))  // Replace Button.One with the correct button for "X"
        {
            // Simulate sitting on the couch
            SitOnCouch();
        }
    }

    void SitOnCouch()
    {
        // Example: Move camera rig down to simulate sitting
        Vector3 sitPosition = cameraRig.position;
        sitPosition.y -= sitHeightOffset;
        cameraRig.position = sitPosition;

        // Optionally disable player movement or perform other actions when sitting
    }
}

