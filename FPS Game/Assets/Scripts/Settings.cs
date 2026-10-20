using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider VolumeSlider;
    public float MasterVolume = 1.0f;
    public AudioSource MenuMusicPlayer;
    public bool Mute;
    

    public void SetVolume()
    {
        MasterVolume = VolumeSlider.value;
        MenuMusicPlayer.volume = MasterVolume;
    }

    public void MuteToggle(bool mute)
    {
        Mute = mute;

        if (!Mute)
        {
            MenuMusicPlayer.volume = 0;
            return;
        }
        else
        {
            MenuMusicPlayer.volume = MasterVolume;
            return;
        }
    }

}
