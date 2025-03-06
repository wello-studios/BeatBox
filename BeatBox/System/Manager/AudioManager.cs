using System.IO;
using UnityEngine;

namespace BeatBox.System.Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        
        private FMOD.ChannelGroup _bgmChannelGroup;
        
        private FMOD.Sound _bgmSound;
        private FMOD.Sound[] _sounds;
        
        private FMOD.Channel _bgmChannel;
        
        [Header("BGM")]
        public string songName;

        public float volume;

        [Header("Sound")]
        public string[] soundNames;

        public bool inited = false;
        
        
        void LoadSong()
        {
            FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Audio", songName+".wav"), FMOD.MODE.CREATESAMPLE, out _bgmSound);

            for (int i = 0; i < soundNames.Length; i++) 
                FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Sound", soundNames[i]+".wav"), FMOD.MODE.CREATESAMPLE, out _sounds[i]);
            
            _bgmChannel.setChannelGroup(_bgmChannelGroup);
        }

        void PlaySong()
        {
            _bgmChannel.stop();
            
            FMODUnity.RuntimeManager.CoreSystem.playSound(_bgmSound, _bgmChannelGroup, true, out _bgmChannel);

            _bgmChannel.setVolume(1.0f);
            _bgmChannel.setPaused(false);
        }
        
        public void PauseSong()
        {
            _bgmChannel.setPaused(true);
        }
        
        public void ResumeSong()
        {
            _bgmChannel.setPaused(false);
        }
        
        void PlaySound(int id)
        {
            FMODUnity.RuntimeManager.CoreSystem.playSound(_sounds[id], _bgmChannelGroup, true, out _bgmChannel);

            _bgmChannel.setVolume(5.0f);
            _bgmChannel.setPaused(false);
        }

        
        
        private void Awake()
        {
            instance = this;
            
            LoadSong();
        }

        public bool BGMplay;
        private void Update()
        {
            if (!inited) return;
            
            if (!BGMplay && TickManager.instance.tick > GameManager.instance.startTick)
            {
                BGMplay = true;
                PlaySong();
            }
        }
    }
}
