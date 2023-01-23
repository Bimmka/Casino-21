using System;
using System.Collections.Generic;
using Features.Constants;
using Features.StaticData.Audio;
using FMOD.Studio;
using FMODUnity;
using Newtonsoft.Json;
using StaticData.Audio;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Features.Services.Audio
{
    public class AudioService : IAudioService
    {
        private readonly AudioContainer audioContainer;
        private readonly Dictionary<AudioEventType, AudioEvent> events;
        private readonly Dictionary<AudioBankType, AudioBank> banks;
        private readonly Dictionary<AudioVolumeType, VolumeParameter> parameters;
        private readonly Dictionary<AudioEventType, EventInstance> eventInstances;
        private readonly Dictionary<AudioVolumeType, Bus> volumesBus;
        private readonly List<string> loadedBanks;
    
        private readonly int maxOneTimePlayingEventsCount;
        private readonly int maxHoldingInstanceCount;

        public bool IsEnable { get; private set; }
        public bool IsCleanedUp { get; private set; }

        public AudioService(AudioContainer audioContainer)
        {
            this.audioContainer = audioContainer;
            events = new Dictionary<AudioEventType, AudioEvent>(audioContainer.Events.Length);
            banks = new Dictionary<AudioBankType, AudioBank>(audioContainer.Banks.Length);
            parameters = new Dictionary<AudioVolumeType, VolumeParameter>(audioContainer.Parameters.Length);
            eventInstances = new Dictionary<AudioEventType, EventInstance>(audioContainer.MaxHoldingInstancesCount);
            volumesBus = new Dictionary<AudioVolumeType, Bus>(parameters.Count);
            
            loadedBanks = new List<string>(5);
        
            for (int i = 0; i < audioContainer.Events.Length; i++)
            {
                events.Add(audioContainer.Events[i].Type, audioContainer.Events[i]);
            }
            
            for (int i = 0; i < audioContainer.Banks.Length; i++)
            {
                banks.Add(audioContainer.Banks[i].Type, audioContainer.Banks[i]);
            }
            maxOneTimePlayingEventsCount = audioContainer.MaxPlayingEventsOnOneTime;
            maxHoldingInstanceCount = audioContainer.MaxHoldingInstancesCount;

           
        }

        public void Cleanup()
        {
            foreach (KeyValuePair<AudioEventType,EventInstance> instance in eventInstances)
            {
                instance.Value.release();
            }

            IsCleanedUp = true;
        }

        public void InitializeBuses()
        {
            Bus bus;
            for (int i = 0; i < audioContainer.Parameters.Length; i++)
            {
                bus = RuntimeManager.GetBus(audioContainer.Parameters[i].Name);
                bus.setVolume(audioContainer.Parameters[i].DefaultValue);
                volumesBus.Add(audioContainer.Parameters[i].Type, bus);
                parameters.Add(audioContainer.Parameters[i].Type, audioContainer.Parameters[i]);
            }
            
            LoadSettings();
        }

        public void LoadBank(AudioBankType type)
        {
            AudioBank bank = Bank(type);
            
            if (string.IsNullOrEmpty(bank.Path))
                return;
            
            LoadBank(bank.Path, bank.IsPreloadSimples);
        }

        public void ReleaseBank(AudioBankType type)
        {
            AudioBank bank = Bank(type);
            
            if (string.IsNullOrEmpty(bank.Path))
                return;
            
            ReleaseBank(bank.Path);
        }
        
        public bool IsBankLoaded(AudioBankType type)
        {
            AudioBank bank = Bank(type);

            return loadedBanks.Contains(bank.Path);
        }

        public void SetEnableState(bool isEnable)
        {
            IsEnable = isEnable;
            SaveSettings();
        }
        
        public void Play(AudioEventType type)
        {
            if (IsHaveEvent(type) == false || IsEnable == false)
                return;
        
            if (IsBankLoaded(events[type].BankType) == false)
                LoadBank(events[type].BankType);
            
            if (IsInstanceCreated(type))
                Play(eventInstances[type]);
            else if (IsCanCreateNewInstance())
                Play( NewInstance(type, events[type].Path));
            
        }

        public void SetVolumeValue(AudioVolumeType volume, float value)
        {
            volumesBus[volume].setVolume(value);
            SaveSettings();
        }

        public float GetVolumeValue(AudioVolumeType type)
        {
            volumesBus[type].getVolume(out float volume);
            return volume;
        }

        public void Stop(AudioEventType type, STOP_MODE stopMode)
        {
            if (IsInstanceCreated(type))
                eventInstances[type].stop(stopMode);
        }

        private void Play(EventInstance instance) => 
            instance.start();

        private EventInstance NewInstance(AudioEventType type, string path)
        {
            try
            { 
                EventInstance instance = RuntimeManager.CreateInstance(path);
                if (IsNeedToRemoveOldInstance())
                    RemoveOldInstance();
        
                eventInstances.Add(type, instance);
                return instance;
            }
            catch (Exception e)
            {
                RuntimeUtils.DebugLogException(e);
                throw;
            }
        }

        private void RemoveOldInstance()
        {
            AudioEventType type = FirstPausedEventType();
        
            if (type == AudioEventType.None)
                return;

            eventInstances[type].release();
            eventInstances.Remove(type);
        }

        private void LoadBank(string bankPath, bool isPreloadSamples)
        {
            try
            {
                RuntimeManager.LoadBank(bankPath, isPreloadSamples);
                loadedBanks.Add(bankPath);
            }
            catch (BankLoadException e)
            {
                RuntimeUtils.DebugLogException(e);
            }
        }
        
        private void ReleaseBank(string bankPath)
        {
            RuntimeManager.UnloadBank(bankPath);
            loadedBanks.Remove(bankPath);
        }

        private AudioBank Bank(AudioBankType type)
        {
            if (banks.ContainsKey(type))
                return banks[type];
            return new AudioBank();
        }

        private bool IsHaveEvent(AudioEventType type) => 
            events.ContainsKey(type);

        private bool IsCanCreateNewInstance()
        {
            if (IsNeedToRemoveOldInstance() == false)
                return true;

            return IsHaveStoppedInstance();
        }

        private bool IsHaveStoppedInstance()
        {
            PLAYBACK_STATE playbackState;
            foreach (KeyValuePair<AudioEventType,EventInstance> instance in eventInstances)
            {
                instance.Value.getPlaybackState(out playbackState);
                if (playbackState == PLAYBACK_STATE.STOPPED)
                    return true;
            }

            return false;
        }

        private AudioEventType FirstPausedEventType()
        {
            PLAYBACK_STATE playbackState;
            foreach (KeyValuePair<AudioEventType,EventInstance> instance in eventInstances)
            {
                instance.Value.getPlaybackState(out playbackState);
                if (playbackState == PLAYBACK_STATE.STOPPED)
                    return instance.Key;
            }

            return AudioEventType.None;
        }

        private bool IsInstanceCreated(AudioEventType type) => 
            eventInstances.ContainsKey(type);

        private bool IsNeedToRemoveOldInstance() => 
            eventInstances.Count >= maxHoldingInstanceCount;

        private void SaveSettings()
        {
            PlayerPrefs.SetInt(GameConstants.SoundKey, IsEnable ? 1 : 0);
            List<float> volumes = new List<float>(volumesBus.Count);
            float value;
            foreach (KeyValuePair<AudioVolumeType,Bus> bus in volumesBus)
            {
                bus.Value.getVolume(out value);
                volumes.Add(value);
            }
            PlayerPrefs.SetString(GameConstants.SoundMusicKey, JsonConvert.SerializeObject(volumes));
        }

        private void LoadSettings()
        {
            IsEnable = PlayerPrefs.GetInt(GameConstants.SoundKey, 1) == 1;
            string savedVolumes = PlayerPrefs.GetString(GameConstants.SoundMusicKey, "");
            if (string.IsNullOrEmpty(savedVolumes))
                return;

            /*List<float> volumes = JsonConvert.DeserializeObject<List<float>>(savedVolumes);
            for (int i = 0; i < volumes.Count; i++)
            {
                volumesBus[(AudioVolumeType) i].setVolume(volumes[i]);
            }*/
        }
    }
}
