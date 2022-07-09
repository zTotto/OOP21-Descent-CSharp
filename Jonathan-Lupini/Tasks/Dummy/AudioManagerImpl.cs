using Jonathan_Lupini.Tasks.Dummy.Supporting;

namespace Jonathan_Lupini.Tasks.Dummy
{
    internal class AudioManagerImpl : IAudioManager
    {
        private readonly Dictionary<string, Sound> _soundEffects = new Dictionary<string, Sound>();
        private readonly Dictionary<string, Music> _songs = new Dictionary<string, Music>();
        public void StopMusic(string path)
        {
            if (IsAlreadyUsed(path)) _songs[path].Stop();
        }

        public void PauseMusic(string path)
        {
            if (IsAlreadyUsed(path)) _songs[path].Pause();
        }

        public void PlayMusic(string path, bool looping, float volume)
        {
            if (IsAlreadyUsed(path) && _songs[path].IsPlaying())
            {
                _songs[path].SetLooping(looping);
                _songs[path].SetVolume(volume);
                _songs[path].Play();
            }
            else if (!IsAlreadyUsed(path))
            {
                Music music = GdxAudio.NewMusic(GdxFiles.Internal(path));
                _songs.Add(path, music);
                _songs[path].SetLooping(looping);
                _songs[path].SetVolume(volume);
                _songs[path].Play();
            }
        }

        public void DisposeMusic(string path)
        {
            if (IsAlreadyUsed(path)) _songs[path].dispose();
        }

        public void ModifyMusic(string path, bool looping, float volume)
        {
            if (IsAlreadyUsed(path))
            {
                _songs[path].SetLooping(looping);
                _songs[path].SetVolume(volume);
            }
        }

        public void PlaySoundEffect(string path, float volume)
        {
            if (IsAlreadyUsed(path))
            {
                _soundEffects[path].SetVolume(0, volume);
                _soundEffects[path].Play();
            }
            else
            {
                Sound sound = GdxAudio.NewSound(GdxFiles.Internal(path));
                sound.SetVolume(0, volume);
                _soundEffects.Add(path, sound);
                _soundEffects[path].Play();
            }
        }

        public void ChangeSoundVolume(string path, float volume)
        {
            if (_soundEffects.ContainsKey(path)) _soundEffects[path].SetVolume(0, volume);
        }

        bool IsAlreadyUsed(string path)
        {
            return _songs.ContainsKey(path) || _soundEffects.ContainsKey(path);
        }

        Music GetSong(string path)
        {
            return _songs[path];
        }

        Sound GetSound(string path)
        {
            return _soundEffects[path];
        }
    }
}
