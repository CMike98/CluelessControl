using NAudio.Wave;

namespace CluelessControl.Sounds
{
    public class Sound : IDisposable
    {
        private string _audioPath;
        private AudioFileReader? _audioFileReader;
        private WaveOutEvent? _outputDevice;
        private float _volume;

        public EventHandler<Sound>? EventStoppedPlayback;
        private bool disposedValue;

        public string AudioPath => _audioPath;
        public float Volume => _volume;
        public PlaybackState PlayBackState => _outputDevice?.PlaybackState ?? PlaybackState.Stopped;

        #region Constructor
        public Sound(string filePath, float volume = 1)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Sound file not found.", filePath);

            _audioPath = filePath;
            _volume = Utils.Clamp(volume, min: 0, max: 1);

            _outputDevice = new WaveOutEvent()
            {
                Volume = _volume
            };
            _outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;

            _audioFileReader = new AudioFileReader(filePath);
            _outputDevice.Init(_audioFileReader);
        }
        #endregion

        #region Methods
        public void Play()
        {
            _outputDevice?.Play();
        }

        public void Stop()
        {
            _outputDevice?.Stop();
        }

        public void Pause()
        {
            _outputDevice?.Pause();
        }
        #endregion

        public void SetVolume(float newVolume)
        {
            _volume = Utils.Clamp(newVolume, min: 0, max: 1);

            if (_outputDevice is not null)
                _outputDevice.Volume = _volume;
        }

        private void OutputDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            EventStoppedPlayback?.Invoke(this, this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _audioFileReader?.Dispose();
                    _outputDevice?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _audioFileReader = null;
                _outputDevice = null;
                EventStoppedPlayback = null;
                
                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~Sound()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
