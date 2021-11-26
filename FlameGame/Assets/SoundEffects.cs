using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    public List < AudioClip > ChopSounds;
    public List<AudioClip> SwingSounds;
    public AudioClip DropInStoreHouse;
    public AudioClip DropOnGrass;
    public AudioClip Pickup;

    private  AudioSource _audio;
    private  float _timeSinceLastSoundEffect;
    private  float _timeBetweenChoppingEffects = 0.4f;
    private float _timeBetweenSwingEffects = 0.7f;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _timeSinceLastSoundEffect = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPickup()
    {
        _audio.clip = Pickup;
        _audio.Play();
    }

    public void PlayDropOnGrass()
    {
        _audio.clip = DropOnGrass;
        _audio.Play();
    }
    public void PlayDropInStoreHouse()
    {
        _audio.clip = DropInStoreHouse;
        _audio.Play();
    }


    public void PlaySwingSound()
    {
        _audio.clip = SwingSounds[Random.Range(0, SwingSounds.Count)];
        _audio.Play();
    }
    public void PlaySwingSound(float timeDiff)
    {

        if (_timeSinceLastSoundEffect > _timeBetweenSwingEffects)
        {
            Debug.Log("playing");
            _audio.clip = SwingSounds[Random.Range(0, SwingSounds.Count)];
            _audio.Play();
            _timeSinceLastSoundEffect = 0.0f;
        }
        _timeSinceLastSoundEffect += timeDiff;

    }

    public void PlayChopSound(float timeDiff)
    {
        if (_timeSinceLastSoundEffect > _timeBetweenChoppingEffects)
        {
            Debug.Log("playing");
            _audio.clip = ChopSounds[Random.Range(0, ChopSounds.Count)];
            _audio.Play();
            _timeSinceLastSoundEffect = 0.0f;
        }
        _timeSinceLastSoundEffect += timeDiff;
       
    }
}
