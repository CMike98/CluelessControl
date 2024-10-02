namespace CluelessControl.Sounds
{
    public class SoundQueue : IDisposable
    {
        private readonly Queue<Sound> _soundQueue;
        private Sound? _currentSound; // Przechowuje aktualnie odtwarzany dźwięk

        private bool _isPlaying;
        private bool _isLooping; // Flaga wskazująca, czy pętla jest odtwarzana w tle
        private bool _isPaused;

        public event Action<SoundQueue>? QueueCompleted; // Zdarzenie z kluczem kolejki

        public string QueueKey { get; }

        public SoundQueue(string queueKey)
        {
            QueueKey = queueKey; // Przechowujemy klucz kolejki
            _soundQueue = new Queue<Sound>();

            _isPlaying = false;
            _isLooping = false;
            _isPaused = false;
            _currentSound = null;
        }

        // Dodaj dźwięk do kolejki
        public void EnqueueSound(Sound sound)
        {
            _soundQueue.Enqueue(sound);
        }

        // Odtwórz dźwięki w kolejce
        public void PlayQueue()
        {
            if (_isPlaying || _soundQueue.Count == 0)
                return;

            _isPlaying = true;
            PlayNextSound();
        }

        // Odtwórz następny dźwięk w kolejce
        private void PlayNextSound()
        {
            // Sprawdzamy, czy jest coś do odtworzenia i czy pętla nie gra
            if (_soundQueue.Count == 0 && !_isLooping)
            {
                _isPlaying = false;
                QueueCompleted?.Invoke(this); // Informujemy o zakończeniu kolejki
                return;
            }

            if (_soundQueue.Count > 0 && !_isPaused) // Odtwarzamy kolejny dźwięk, jeśli jest coś w kolejce
            {
                _currentSound = _soundQueue.Dequeue();
                _currentSound.Play();

                // Subskrybujemy zakończenie odtwarzania dźwięku
                _currentSound.EventStoppedPlayback += OnCurrentSoundPlaybackStopped;

                // Jeśli dźwięk jest w nieskończonej pętli, odtwarzamy go w tle
                if (_currentSound.LoopType == SoundLoopType.INFINITE_LOOP ||
                    (_currentSound.LoopType == SoundLoopType.FINITE_LOOP && _currentSound.LoopCount > 0))
                {
                    _isLooping = true;
                    PlayNextSound(); // Przechodzimy do kolejnego dźwięku w tle
                }
            }
        }

        // Metoda wywoływana po zakończeniu odtwarzania bieżącego dźwięku
        private void OnCurrentSoundPlaybackStopped(object? sender, Sound sound)
        {
            sound.EventStoppedPlayback -= OnCurrentSoundPlaybackStopped; // Odpinamy zdarzenie

            // Jeśli dźwięk nie jest w nieskończonej pętli, przechodzimy do kolejnego
            if (sound.LoopType != SoundLoopType.INFINITE_LOOP &&
                (sound.LoopType != SoundLoopType.FINITE_LOOP && sound.LoopCount <= 0))
            {
                PlayNextSound(); // Odtwórz następny dźwięk w kolejce
            }
        }

        // Zatrzymaj pętlę i przejdź do kolejnych dźwięków
        public void StopLooping()
        {
            if (_isLooping)
            {
                _isLooping = false;    // Flaga wskazuje, że nie ma już pętli

                _currentSound?.SetNoLoop();
                _currentSound?.Stop(); // Zatrzymujemy dźwięk w nieskończonej pętli
                
                PlayNextSound();       // Sprawdzamy, czy są kolejne dźwięki w kolejce
            }
        }

        // Pauzuj aktualnie odtwarzany dźwięk
        public void PauseCurrentSound()
        {
            if (_currentSound != null && !_isPaused)
            {
                _currentSound?.Pause(); // Pauzujemy bieżący dźwięk, jeśli istnieje
                _isPaused = true;
            }
        }

        // Wznów aktualnie odtwarzany dźwięk
        public void ResumeCurrentSound()
        {
            if (_currentSound != null && _isPaused)
            {
                _isPaused = false;
                _currentSound?.Play();

                PlayNextSound();
            }
        }

        // Zatrzymaj kolejkę
        public void StopQueue()
        {
            _isPlaying = false;
            _isLooping = false;
            _isPaused = false;
            _soundQueue.Clear();

            _currentSound?.SetNoLoop();
            _currentSound?.Stop();
            _currentSound = null;
        }

        public void ClearQueue()
        {
            StopQueue();
        }

        public void SetVolume(float volume)
        {
            volume = Utils.Clamp(volume, min: 0, max: 1);

            _currentSound?.SetVolume(volume);

            foreach (Sound sound in _soundQueue)
            {
                sound.SetVolume(volume);
            }
        }

        #region Dispose

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _soundQueue.Clear();
                    _currentSound = null;
                    QueueCompleted = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SoundQueue()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        
        #endregion
    }
}
