using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhoneInteraction : MonoBehaviour
{
    public AudioSource phoneAudioSource; // The audio source for the phone ringing sound
    public AudioClip phoneRingClip;      // The phone ringing sound clip
    public float vibrationDuration = 1.0f; // Duration of the controller vibration
    public float delayBeforeRing = 10.0f;   // Delay before the phone starts ringing
    public Canvas phoneCanvas;            // The Canvas on the phone
    public Button answerButton;           // The Button to answer the call

    private bool isPhonePickedUp = false;  // Flag to check if the phone is picked up
    private bool isPhoneRinging = false;   // Flag to check if the phone is currently ringing
    private Coroutine ringCoroutine;       // Coroutine reference for ringing delay
    private Vector3 initialPosition;       // Initial position of the phone

    void Start()
    {
        // Store the initial position of the phone
        initialPosition = transform.position;

        // Assign the AnswerCall method to the button onClick event
        answerButton.onClick.AddListener(AnswerCall);

        // Hide the Canvas initially
        phoneCanvas.enabled = false;

        // Start the coroutine to ring the phone after a delay
        StartCoroutine(RingPhoneAfterDelay());
    }

    IEnumerator RingPhoneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeRing);

        if (!isPhonePickedUp)
        {
            // Play the phone ringing sound
            phoneAudioSource.clip = phoneRingClip;
            phoneAudioSource.loop = true; // Loop the ring sound
            phoneAudioSource.Play();

            // Vibrate the controllers
            StartCoroutine(VibrateControllers(vibrationDuration));

            // Show the phone UI
            phoneCanvas.enabled = true;

            isPhoneRinging = true;
        }
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

    public void StopPhoneRinging()
    {
        if (isPhoneRinging)
        {
            // Stop the ringing sound
            phoneAudioSource.Stop();

            // Stop the vibration
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);

            // Hide the phone UI
            phoneCanvas.enabled = false;

            isPhoneRinging = false;
        }
    }

    public void AnswerCall()
    {
        if (isPhonePickedUp)
        {
            // Stop the ringing sound
            StopPhoneRinging();

            // Additional logic when the call is answered, if necessary
        }
    }

    void Update()
    {
        // Check if the phone has moved from its initial position
        if (!isPhonePickedUp && transform.position != initialPosition)
        {
            // If phone has moved, stop ringing
            StopPhoneRinging();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // Adjust as per your hand grab setup
        {
            isPhonePickedUp = true;

            // Stop phone ringing and disable UI when picked up
            StopPhoneRinging();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // Adjust as per your hand grab setup
        {
            isPhonePickedUp = false;
        }
    }
}
