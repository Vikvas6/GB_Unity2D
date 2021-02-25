using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lessons : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private SpriteRenderer _back;

    //[SerializeField] private SomeView _someView;
    [SerializeField] private SpriteAnimator _spriteAnimator;
    
    private void Start()
    {
        SpriteAnimationsConfig config = Resources.Load("SpriteAnimationConfig", typeof(SpriteAnimationsConfig)) as SpriteAnimationsConfig;
        _spriteAnimator = new SpriteAnimator(config);
        _spriteAnimator.StartAnimation(_characterView.SpriteRenderer, Track.Run, true, 10);
    }

    private void Update()
    {
        _spriteAnimator.UpdateTick();
    }

    private void FixedUpdate()
    {
        //_someManager.FixedUpdateTick();
    }

    private void OnDestroy()
    {
        //_someManager.Dispose();
    }
}
