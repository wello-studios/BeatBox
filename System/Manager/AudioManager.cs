using System.IO;
using FMOD;
using UnityEngine;
using Debug = FMOD.Debug;

// 이건 FMOD 배워서 해보는거로

/* {AudioManager}
 * 
 * [VARIABLE]
 * idk
 *
 * [METHOD]
 * idk
 *
 * [UNITY EVENT]
 * AWAKE - IN. TH., 곡 파일 불러오기
 * START - 
 * UPDATE - 첫 단위마디에 곡 재생
 */

namespace BeatBox.System.Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        
        private FMOD.ChannelGroup _bgmChannelGroup;
        
        private FMOD.Sound _bgmSound;
        public int songLength; // second
        
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
            
            _bgmSound.getLength(out uint length, TIMEUNIT.MS);
            songLength = (int)length;
            UnityEngine.Debug.Log("Song Length = "+songLength);
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

        void FixedUpdate()
        {
            var speed = (float)(GameManager.instance.GameSpeed.GetValue(TickManager.instance.tick));
            var currSpeed = 0.0f;
            _bgmChannel.getPitch(out currSpeed);

            if (currSpeed != speed)
            {
                _bgmChannel.setPitch(speed);
            }
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
