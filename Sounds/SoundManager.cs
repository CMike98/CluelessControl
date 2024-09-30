
namespace CluelessControl.Sounds
{
    public class SoundManager
    {
        private readonly Dictionary<string, SoundQueue> _soundQueues = [];
        private readonly List<Sound> _singleSounds = [];

        #region Volume
        private float _volume = 1;

        public float Volume => _volume;

        public void SetVolume(float newVolume)
        {
            _volume = Utils.Clamp(newVolume, min: 0, max: 1);

            var soundQueues = _soundQueues.Select(queuePair => queuePair.Value);

            foreach (SoundQueue? currentQueue in soundQueues)
            {
                currentQueue.SetVolume(_volume);
            }

            foreach (Sound singleSound in _singleSounds)
            {
                singleSound.SetVolume(_volume);
            }
        }
        #endregion

        #region Single Sound

        public void PlaySingleSound(Sound sound)
        {
            if (sound is null)
                throw new ArgumentNullException(nameof(sound));

            sound.SetVolume(_volume);
            sound.EventStoppedPlayback += Sound_SoundStopped;
            sound.SetNoLoop();
            sound.Play();

            _singleSounds.Add(sound);
        }

        public void PlaySingleSound(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Sound file not found.", filePath);

            Sound sound = new(filePath, _volume);
            sound.EventStoppedPlayback += Sound_SoundStopped;
            sound.SetNoLoop();
            sound.Play();

            _singleSounds.Add(sound);
        }

        #endregion

        #region Queue Operations

        public bool DoesQueueExist(string queueName)
        {
            return _soundQueues.ContainsKey(queueName);
        }

        public void CreateQueue(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException(nameof(queueName));

            if (_soundQueues.ContainsKey(queueName))
                throw new InvalidOperationException($"There is a queue with a name: \"{queueName}\"");

            var soundQueue = new SoundQueue(queueName);
            soundQueue.QueueCompleted += SoundQueue_QueueCompleted;
            _soundQueues.Add(queueName, soundQueue);
        }

        public SoundQueue? GetQueue(string queueName)
        {
            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException(nameof(queueName));

            return _soundQueues.TryGetValue(queueName, out var result) ? result : null;
        }

        public void ClearQueue(string queueName)
        {
            var soundQueue = GetQueue(queueName) ?? throw new KeyNotFoundException($"Queue \"{queueName}\" not found.");
            soundQueue.ClearQueue();
        }

        public void AddSoundToQueue(string queueName, string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Sound file not found.");

            var soundQueue = GetQueue(queueName) ?? throw new KeyNotFoundException($"Queue \"{queueName}\" not found.");

            var sound = new Sound(filePath, _volume);
            soundQueue.EnqueueSound(sound);
        }

        public void AddSoundToQueue(string queueName, Sound sound)
        {
            var soundQueue = GetQueue(queueName) ?? throw new KeyNotFoundException($"Queue \"{queueName}\" not found.");
            soundQueue.EnqueueSound(sound);
        }

        public void PlayQueue(string queueName)
        {
            var soundQueue = GetQueue(queueName) ?? throw new KeyNotFoundException($"Queue \"{queueName}\" not found.");
            soundQueue.PlayQueue();
        }

        public void StopQueue(string queueName)
        {
            var soundQueue = GetQueue(queueName);
            soundQueue?.StopQueue();

            _soundQueues.Remove(queueName);
        }

        public void PauseQueue(string queueName)
        {
            var soundQueue = GetQueue(queueName) ?? throw new KeyNotFoundException($"Queue \"{queueName}\" not found.");
            soundQueue.PauseCurrentSound();
        }

        public void ResumeQueue(string queueName)
        {
            var soundQueue = GetQueue(queueName) ?? throw new KeyNotFoundException($"Queue \"{queueName}\" not found.");
            soundQueue.ResumeCurrentSound();
        }

        public void StopAllQueues()
        {
            List<string> queueKeys = _soundQueues.Select(queue => queue.Key).ToList();

            foreach (string key in queueKeys)
            {
                StopQueue(key);
            }
        }

        #endregion

        #region Clean Up
        private void Sound_SoundStopped(object? sender, Sound e)
        {
            _singleSounds.Remove(e);
        }

        private void SoundQueue_QueueCompleted(SoundQueue obj)
        {
            _soundQueues.Remove(obj.QueueKey);
        }
        #endregion
    }
}
