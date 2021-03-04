using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteManager : IDisposable
{
    private Vector3 _startPosition;
    private CharacterView _view;
    private List<CharacterView> _deathZones;
    private List<CharacterView> _winZones;
    private GameObject _victoryPanel;

    public LevelCompleteManager(CharacterView view, List<CharacterView> deathZones, List<CharacterView> winZones, GameObject victoryPanel)
    {
        _startPosition = view.Transform.position;
        view.OnLevelObjectContact += OnLevelObjectContact;
        
        _view = view;
        _deathZones = deathZones;
        _winZones = winZones;
        _victoryPanel = victoryPanel;
    }

    private void OnLevelObjectContact(CharacterView contactView)
    {
        if (_deathZones.Contains(contactView))
        {
            _view.Transform.position = _startPosition;
        }
        else if (_winZones.Contains(contactView))
        {
            Time.timeScale = 0.0f;
            _victoryPanel.SetActive(true);   
        }
    }

    public void Dispose()
    {
        _view.OnLevelObjectContact -= OnLevelObjectContact;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
