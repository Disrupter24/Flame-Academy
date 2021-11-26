using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    public List < AudioClip > ChopSounds;
    private  AudioSource _audio;
    private  float _timeSinceLastSoundEffect;
    private  float _timeBetweenSoundeffects = 0.4f;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _timeSinceLastSoundEffect = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayChopSound(float timeDiff)
    {
        if (_timeSinceLastSoundEffect > _timeBetweenSoundeffects)
        {
            Debug.Log("playing");
            _audio.clip = ChopSounds[Random.Range(0, 3)];
            _audio.Play();
            _timeSinceLastSoundEffect = 0.0f;
        }
        _timeSinceLastSoundEffect += timeDiff;
       
    }
}
