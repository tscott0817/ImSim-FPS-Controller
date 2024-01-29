using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private float footstepVolume = 0.25f;

    private bool isMoving = false;

    private void Update()
    {
        // Check if any of the WASD keys are held down
        isMoving = Input.GetKey(ManageInputs.moveForwardKey) || Input.GetKey(ManageInputs.moveBackwardKey) || Input.GetKey(ManageInputs.moveLeftKey) || Input.GetKey(ManageInputs.moveRightKey);

        // Set the AudioSource's loop property based on whether the character is moving
        footstepAudioSource.loop = isMoving;

        // Set the volume of the AudioSource
        footstepAudioSource.volume = footstepVolume;

        if (isMoving)
        {
            // If not already playing, start playing the footstep sound
            if (!footstepAudioSource.isPlaying)
            {
                PlayFootstepSound();
            }
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepAudioSource != null)
        {
            // Play the footstep sound
            footstepAudioSource.Play();
        }
    }
}
