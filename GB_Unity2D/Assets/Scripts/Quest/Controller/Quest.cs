using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : IQuest
{

    #region Fields

    private readonly QuestObjectView _view;
    private readonly IQuestModel _model;

    private bool _active;

    #endregion

    #region Life Cycle

    public Quest(QuestObjectView view, IQuestModel model)
    {
        _view = view != null ? view : throw new ArgumentNullException(nameof(view));
        _model = model != null ? model : throw new ArgumentNullException(nameof(model));
    }

    #endregion

    #region Methods

    private void OnContact(CharacterView other)
    {
        var completed = _model.TryComplete(other.gameObject);
        if (completed) Complete();
    }

    private void Complete()
    {
        if (!_active) return;
        _active = false;
        IsCompleted = true;
        _view.OnLevelObjectContact -= OnContact;
        _view.ProcessComplete();
        OnCompleted();
    }

    private void OnCompleted()
    {
        Completed?.Invoke(this, this);
    }

    #endregion

    #region IQuest

    public event EventHandler<IQuest> Completed;
    public bool IsCompleted { get; private set; }
    
    public void Reset()
    {
        if (_active) return;
        _active = true;
        IsCompleted = false;
        _view.OnLevelObjectContact += OnContact;
        _view.ProcessActivate();
    }

    public void Dispose()
    {
        _view.OnLevelObjectContact -= OnContact;
    }
    
    #endregion
}
