using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestObjectView : CharacterView
{
    public int Id => _id;

    [SerializeField] private Color _completedColor;
    [SerializeField] private int _id;

    private Color _defaultColor;
    
    #region Unity methods

    private void Awake()
    {
        _defaultColor = SpriteRenderer.color;
    }

    #endregion
    
    #region Methods

    public void ProcessComplete()
    {
        SpriteRenderer.color = _completedColor;
    }
  
    public void ProcessActivate()
    {
        SpriteRenderer.color = _defaultColor;
    }

    #endregion
}
