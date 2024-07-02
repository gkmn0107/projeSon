using System.Collections;
using UnityEngine;

public class RingtoneControl : MonoBehaviour
{
    public AudioSource phoneAudioSource; // The audio source for the phone ringing sound
    public AudioClip phoneRingClip;      // The phone ringing sound clip
    public float vibrationDuration = 1.0f; // Duration of the controller vibration
    public float delayBeforeRing = 5.0f;   // Delay before the phone starts ringing

    void Start()
    {
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

        // Vibrate the controllers
        StartCoroutine(VibrateControllers(vibrationDuration));
    }

    IEnumerator VibrateControllers(float duration)
    {
        // Vibrate both the left and right controllers
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);

        // Wait for the duration of the vibration
        yield return new WaitForSeconds(duration);

        // Stop the vibration
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
