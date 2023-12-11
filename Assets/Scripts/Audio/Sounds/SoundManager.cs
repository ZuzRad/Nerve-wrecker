using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClip jump, endLevel, collect, hit, wind, laser, trampoline;

    AudioSource audioSource;
    Dictionary<Sounds, AudioClip> sounds;

    public enum Sounds
    {
        Jump, EndLevel, Collect, Hit, Wind, Laser, Trampoline
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GlobalVolumeManager.GetSoundsVolume();
        sounds = new Dictionary<Sounds, AudioClip>
        {
            { Sounds.Jump, jump },
            { Sounds.EndLevel, endLevel },
            { Sounds.Collect, collect },
            { Sounds.Hit, hit },
            { Sounds.Wind, wind },
            { Sounds.Laser, laser },
            { Sounds.Trampoline, trampoline }
        };
        
        BindAllEnvironmentElements();
    }
    
    public void PlaySound(Sounds sound)
    {
        audioSource.volume = GlobalVolumeManager.GetSoundsVolume();
        audioSource.PlayOneShot(sounds[sound]);
    }

    public void SetClip(Sounds sound)
    {
        audioSource.volume = GlobalVolumeManager.GetSoundsVolume();
        audioSource.clip = sounds[sound];
        
        if (!audioSource.isPlaying) 
        {
            audioSource.Play();
        }
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }
    
    public void SetLoop(bool isLooped)
    {
        audioSource.loop = isLooped;
    }
    
    private void BindAllEnvironmentElements()
    {

        Movement player = FindObjectOfType<Movement>();
        if (player != null)
        {
            player.onJump += () => PlaySound(Sounds.Jump);

            HealthManager healthManager = FindObjectOfType<HealthManager>();
            healthManager.onPlayerDeath += () => PlaySound(Sounds.Hit);
            healthManager.onIncreaseHealth += () => PlaySound(Sounds.Collect);
        }


        SoundSlider slider = FindObjectOfType<SoundSlider>();
        if(slider != null)
        {
            slider.onChangeSoundVolume += () => PlaySound(Sounds.Collect);
        }

        Trampoline[] trampolines = FindObjectsOfType<Trampoline>();
        foreach (var trampoline in trampolines)
        {
            trampoline.onTrigger += () => PlaySound(Sounds.Trampoline);
        }

        BlowingFan[] blowingFans = FindObjectsOfType<BlowingFan>();
        foreach (var fan in blowingFans)
        {
            fan.onTrigger += () => PlaySound(Sounds.Wind);
        }

        ShootLaser[] lasers = FindObjectsOfType<ShootLaser>();
        foreach (var laser in lasers)
        {
            laser.onTrigger += () => PlaySound(Sounds.Laser);
        }

        EndLevel endLevel = FindObjectOfType<EndLevel>();
        if(endLevel != null)
        {
            endLevel.onEndLevel += () => PlaySound(Sounds.EndLevel);
        }

    }
}
