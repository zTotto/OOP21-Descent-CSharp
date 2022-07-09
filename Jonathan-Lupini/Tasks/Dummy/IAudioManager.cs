namespace Jonathan_Lupini.Tasks.Dummy
{
    /// <summary>
    /// Interface for a class that handles the audio of the game.
    /// </summary>
    internal interface IAudioManager
    {
        /// <summary>
        /// Stops the specified song.
        /// </summary>
        void StopMusic(string path);

        /// <summary>
        /// Pauses the specified song.
        /// </summary>
        void PauseMusic(string path);

        /// <summary>
        /// Starts playing the specified song.
        /// The song starts only if there isn't another instance of the 
        /// same song already playing.
        /// </summary>
        void PlayMusic(string path, bool looping, float volume);

        /// <summary>
        /// Deletes the specified song.
        /// </summary>
        void DisposeMusic(string path);

        /// <summary>
        /// Changes whether or not the song is looping and it's volume.
        /// </summary>
        void ModifyMusic(string path, bool looping, float volume);

        /// <summary>
        /// Plays a sound effect once.
        /// </summary>
        void PlaySoundEffect(string path, float volume);

        /// <summary>
        /// Changes the volume of a sound effect that was used previously.
        /// </summary>
        void ChangeSoundVolume(string path, float volume);
    }
}
