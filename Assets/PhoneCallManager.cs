using System.Collections;
using UnityEngine;

public class PhoneCallManager : MonoBehaviour
{
    public AudioSource phoneAudioSource; // The audio source for the phone ringing sound
    public AudioClip phoneRingClip;      // The phone ringing sound clip
    public float vibrationDuration = 1.0f; // Duration of the controller vibration
    public float delayBeforeRing = 10.0f;   // Delay before the phone starts ringing

    private bool isPhonePickedUp = false;  // Flag to check if the phone is picked up
    private Vector3 initialPosition;       // Initial position of the phone

    void Start()
    {
        initialPosition = transform.position; // Store initial position of the phone

        // Start the coroutine to ring the phone after a delay
        StartCoroutine(RingPhoneAfterDelay());
    }

    IEnumerator RingPhoneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeRing);

        // Play the phone ringing sound
        phoneAudioSource.clip = phoneRingClip;
        phoneAudioSource.Play();

        // Enable checking for phone position change
        StartCoroutine(CheckPhonePosition());
    }

    IEnumerator CheckPhonePosition()
    {
        while (!isPhonePickedUp)
        {
            yield return null;

            // Check if the phone's position has changed significantly
            if (Vector3.Distance(transform.position, initialPosition) > 0.1f) // Adjust threshold as needed
            {
                isPhonePickedUp = true;

                // Stop the ringing sound
                phoneAudioSource.Stop();

                // Additional logic when the call is answered, if necessary
                Debug.Log("Phone picked up!");

                // Stop controller vibration (if needed)
                StopControllerVibration();
            }
        }
    }

    void StopControllerVibration()
    {
        // Stop vibration on both controllers
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}

