using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : IDisposable
{
    private const float _animationSpeed = 10;

    private CharacterView _view;
    private SpriteAnimatorController _spriteAnimator;
    private List<CharacterView> _coinViews;

    public CoinsManager(CharacterView view, SpriteAnimatorController spriteAnimator, List<CharacterView> coinViews)
    {
        _view = view;
        _spriteAnimator = spriteAnimator;
        _coinViews = coinViews;

        _view.OnLevelObjectContact += OnLevelObjectContact;
        foreach (var coinView in coinViews)
        {
            _spriteAnimator.StartAnimation(coinView.SpriteRenderer, Track.Idle, true, _animationSpeed);
        }
    }

    private void OnLevelObjectContact(CharacterView contactView)
    {
        if (_coinViews.Contains(contactView))
        {
            _spriteAnimator.StopAnimation(contactView.SpriteRenderer);
            GameObject.Destroy(contactView.gameObject);
        }
    }

    public void Dispose()
    {
        _view.OnLevelObjectContact -= OnLevelObjectContact;
    }
}
