using System;
using System.Collections.Generic;
using UnityEngine;


public enum Track
{
    Idle,
    Run,
    Jump
}


[CreateAssetMenu(fileName =  "SpriteAnimationConfig", menuName = "Configs/SpriteAnimationsConfig", order = 1)]
public class SpriteAnimationsConfig : ScriptableObject
{
    [Serializable]
    public class SpriteSequence
    {
        public Track Track;
        public List<Sprite> Sprites = new List<Sprite>();
    }

    public List<SpriteSequence> Sequences = new List<SpriteSequence>();
}
