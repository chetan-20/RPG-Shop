using System;
using UnityEngine;
public class SoundManager : MonoBehaviour
{       
    [SerializeField] private AudioSource SoundEffect;
    [SerializeField] private AudioSource SoundMusic;
    [SerializeField] public SoundType[] Sou;    
   
    private void Start()
    {
        PlayMusic();
    }
    private AudioClip GetSoundClip(Sounds sound)
    {
        SoundType item = Array.Find(Sou, i => i.soundtype == sound);
        if (item != null)
        {
            return item.soundclip;
        }
        else
        {
            return null;
        }
    }   
    public void PlaySound(Sounds sound)
    {
        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            SoundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("Audio Not Assigned 2");
        }
    }
    private void PlayMusic()
    {
        SoundMusic.Play();      
    }   
}

[Serializable]
public class SoundType
{
    public Sounds soundtype;
    public AudioClip soundclip;
}
public enum Sounds
{
   ButtonClickSound,
   BuySound,
   SellSound,
   IncDecQuantitySound,
   CantBuyorSellSound,
   GenerateMoneySound
}

