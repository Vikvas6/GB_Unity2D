using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lessons : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private SpriteRenderer _back;
    [SerializeField] private GunView _gun;
    

    //[SerializeField] private SomeView _someView;
    [SerializeField] private SpriteAnimatorController _spriteAnimator;
    [SerializeField] private int _animationSpeed = 10;

    private MainHeroWalker _hero;
    private AimingMuzzle _muzzle;
    private BulletsEmitter _bulletEmitter;
    
    private void Start()
    {
        SpriteAnimationsConfig config = Resources.Load("SpriteAnimationConfig", typeof(SpriteAnimationsConfig)) as SpriteAnimationsConfig;
        _spriteAnimator = new SpriteAnimatorController(config);
        _spriteAnimator.StartAnimation(_characterView.SpriteRenderer, Track.Run, true, _animationSpeed);
        _hero = new MainHeroWalker(_characterView, _spriteAnimator);
        
        _muzzle = new AimingMuzzle(_gun._muzzleTransform, _characterView.transform);
        _bulletEmitter = new BulletsEmitter(_gun._bullets, _gun._emitterTransform);
    }

    private void Update()
    {
        _spriteAnimator.UpdateTick();
        _hero.UpdateTick();
        _muzzle.UpdateTick();
        _bulletEmitter.UpdateTick();
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
