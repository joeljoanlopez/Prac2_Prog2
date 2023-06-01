using System;
using SFML.Audio;
using TCEngine;

namespace TCGame
{
    public class SoundEffectComponent : BaseComponent
    {
        private string _AudioPath;
        SoundManager _Sound;

        public SoundEffectComponent(string path)
        {
            _Sound = new SoundManager();
            _AudioPath = path;
        }

        public override void OnActorCreated()
        {

            _Sound.PlaySound(_AudioPath);
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            throw new NotImplementedException();
        }
    }
}