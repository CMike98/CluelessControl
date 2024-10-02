using NAudio.Wave;

namespace CluelessControl.Sounds
{
    public class Sound : IDisposable
    {
        private string _audioPath;
        private AudioFileReader? _audioFileReader;
        private WaveOutEvent? _waveOut;
        private float _volume;

        private bool _isPaused;
        private int _loopCount;
        private SoundLoopType _loopType;

        public EventHandler<Sound>? EventStoppedPlayback;
        private bool disposedValue;

        public string AudioPath => _audioPath;
        public float Volume => _volume;
        public PlaybackState PlayBackState => _waveOut?.PlaybackState ?? PlaybackState.Stopped;


        public int LoopCount => _loopCount;
        public SoundLoopType LoopType => _loopType;


        #region Constructor
        public Sound(string filePath, float volume = 1)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Sound file not found.", filePath);

            _audioPath = filePath;
            _volume = Utils.Clamp(volume, min: 0, max: 1);
            _loopType = SoundLoopType.NO_LOOP;

            _audioFileReader = new AudioFileReader(filePath);

            _waveOut = new WaveOutEvent();
            _waveOut.Init(_audioFileReader);
            _waveOut.Volume = volume;

            _waveOut.PlaybackStopped += OnPlayBackStopped;
        }

        private void OnPlayBackStopped(object? sender, StoppedEventArgs e)
        {
            if (_audioFileReader == null)
                throw new InvalidOperationException("No audioFileReader found.");
            if (_waveOut == null)
                throw new InvalidOperationException("No waveOut found.");

            if (_loopType == SoundLoopType.INFINITE_LOOP)
            {
                _audioFileReader.Position = 0;
                _waveOut.Play();
            }
            else if (_loopType == SoundLoopType.FINITE_LOOP && _loopCount > 0)
            {
                _audioFileReader.Position = 0;
                _waveOut.Play();

                _loopCount--;
            }
            else
            {
                EventStoppedPlayback?.Invoke(this, this);
            }
        }

        #endregion

        #region Methods
        public void SetInfiniteLoop()
        {
            _loopType = SoundLoopType.INFINITE_LOOP;
        }

        public void SetFiniteLoop(int loopCount)
        {
            if (loopCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(loopCount), $"The loop count must be greater than zero.");

            _loopType = SoundLoopType.FINITE_LOOP;
            _loopCount = loopCount; 
        }

        public void SetNoLoop()
        {
            _loopType = SoundLoopType.NO_LOOP;
            _loopCount = 0;
        }

        public void Play()
        {
            _waveOut?.Play();
            _isPaused = false;
        }

        public void Stop()
        {
            _waveOut?.Stop();
            _isPaused = false;
        }

        public void Pause()
        {
            _waveOut?.Pause();
            _isPaused = true;
        }
        #endregion

        public void SetVolume(float newVolume)
        {
            _volume = Utils.Clamp(newVolume, min: 0, max: 1);

            if (_waveOut is not null)
                _waveOut.Volume = _volume;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _audioFileReader?.Dispose();
                    _waveOut?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _audioFileReader = null;
                _waveOut = null;
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
