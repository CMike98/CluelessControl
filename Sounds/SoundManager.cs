
namespace CluelessControl.Sounds
{
    public class SoundManager
    {
        private readonly List<Sound> _sounds = [];

        private float _volume = 1;

        public float Volume => _volume;

        #region Get Sound
        public Sound? GetSound(string filePath)
        {
            return _sounds.FirstOrDefault(sound => sound.AudioPath == filePath);
        }
        #endregion

        #region Play, Pause, Stop
        public void PlaySound(string filePath)
        {
            var newSound = new Sound(filePath, _volume);
            newSound.EventStoppedPlayback += SoundManager_StoppedPlayback;

            newSound.Play();
        }

        public void PauseSound(string filePath)
        {
            Sound? sound = GetSound(filePath);
            sound?.Pause();
        }

        public void StopSound(string filePath)
        {
            Sound? sound = GetSound(filePath);
            sound?.Stop();
        }

        public void StopAll()
        {
            _sounds.ForEach(sound => sound.Stop());
        }
        #endregion

        #region Set Volume
        public void SetVolume(float newVolume)
        {
            _volume = Utils.Clamp(newVolume, min: 0, max: 1);
            _sounds.ForEach(sound => sound.SetVolume(_volume));
        }
        #endregion

        #region Stopped Playback Event
        private void SoundManager_StoppedPlayback(object? sender, Sound e)
        {
            _sounds.Remove(e);
        }
        #endregion
    }
}
