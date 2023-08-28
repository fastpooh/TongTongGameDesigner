using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource sound;

    public void PlaySound()
    {
        if (sound.isPlaying)
            return;
        sound.Play();
    }
}
