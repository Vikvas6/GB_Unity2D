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
    [SerializeField] private List<CharacterView> _coinViews;
    [SerializeField] private List<CharacterView> _deathZones;
    [SerializeField] private List<CharacterView> _winZones;
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private List<CharacterView> _elevators;
    [SerializeField] private List<CharacterView> _elevatorLimits;
    [SerializeField] private AstarPath _pathfinder;
    

    //[SerializeField] private SomeView _someView;
    [SerializeField] private SpriteAnimatorController _spriteAnimator;
    [SerializeField] private SpriteAnimatorController _coinSpriteAnimator;
    [SerializeField] private int _animationSpeed = 10;

    private MainHeroPhysicsWalker _hero;
    private AimingMuzzle _muzzle;
    private BulletsEmitter _bulletEmitter;
    private LevelCompleteManager _levelCompleteManager;
    private ElevatorManager _elevatorManager;

    [SerializeField] private GenerateLevelView _generateLevelView;
    private GeneratorLevelController _generatorLevelController;
    
    private void Start()
    {
        SpriteAnimationsConfig config = Resources.Load("SpriteAnimationConfig", typeof(SpriteAnimationsConfig)) as SpriteAnimationsConfig;
        _spriteAnimator = new SpriteAnimatorController(config);
        _spriteAnimator.StartAnimation(_characterView.SpriteRenderer, Track.Run, true, _animationSpeed);
        _hero = new MainHeroPhysicsWalker(_characterView, _spriteAnimator);
        
        _muzzle = new AimingMuzzle(_gun._muzzleTransform, _characterView.transform);
        _bulletEmitter = new BulletsEmitter(_gun._bullets, _gun._emitterTransform);

        SpriteAnimationsConfig coinConfig = Resources.Load<SpriteAnimationsConfig>("CoinAnimationConfig");
        _coinSpriteAnimator = new SpriteAnimatorController(coinConfig);
        CoinsManager coinsManager = new CoinsManager(_characterView, _coinSpriteAnimator, _coinViews);

        _levelCompleteManager = new LevelCompleteManager(_characterView, _deathZones, _winZones, _victoryPanel);
        _elevatorManager = new ElevatorManager(_elevators, _elevatorLimits);
    }

    private void Awake()
    {
        
        _generatorLevelController = new GeneratorLevelController(_generateLevelView);
        _generatorLevelController.Awake();
        
        Invoke(nameof(ScanPath), 0.2f);
    }

    private void ScanPath()
    {
        AstarPath.active.Scan(AstarPath.active.graphs[0]);
    }

    private void Update()
    {
        _spriteAnimator.UpdateTick();
        _muzzle.UpdateTick();
        _bulletEmitter.UpdateTick();
        _coinSpriteAnimator.UpdateTick();
    }

    private void FixedUpdate()
    {
        _hero.UpdateTick();
    }

    private void OnDestroy()
    {
        //_someManager.Dispose();
    }

    public void Exit()
    {
        _levelCompleteManager.Exit();
    }
}
