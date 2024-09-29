
namespace CluelessControl.Sounds
{
    public class SoundManager
    {
        private readonly Dictionary<string, SoundQueue> _soundQueues = [];

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
        }
        #endregion

        #region Queue Operations

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

        #endregion

        #region Clean Up
        private void SoundQueue_QueueCompleted(SoundQueue obj)
        {
            _soundQueues.Remove(obj.QueueKey);
        }
        #endregion
    }
}
