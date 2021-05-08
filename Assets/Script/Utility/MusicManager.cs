using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource musicPlayer; 
    [SerializeField] private AudioClip bgm;

    [SerializeField] private AudioClip enterBattle;

    void Awake (){
        instance = this;
        musicPlayer = GetComponent<AudioSource>();
        PlayMusic ();
    }

    public static void PlayMusic (){
        if(instance == null)
            return;
        instance.musicPlayer.Play();
        Debug.Log("Playmusic");
    }

    public static void StopMusic(){
        if(instance == null)
            return;
        instance.musicPlayer.Stop();
        Debug.Log("Stopmusic");
    }
}
