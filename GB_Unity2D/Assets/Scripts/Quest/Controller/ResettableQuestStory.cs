using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ResettableQuestStory : IQuestStory
{
    #region Fields
    
    public event Action Completed;
    private readonly List<IQuest> _questsCollection;
    private readonly List<IQuestObject> _questObjects;
    private int _currentIndex;

    #endregion

    #region Life Cycle

    public ResettableQuestStory(List<IQuest> questsCollection, List<IQuestObject> questObjects)
    {
        _questsCollection = questsCollection ?? throw new ArgumentNullException(nameof(questsCollection));
        _questObjects = questObjects ?? throw new ArgumentNullException(nameof(questObjects));
        Subscribe();
        ResetQuests();
    }

    #endregion

    #region Methods

    private void Subscribe()
    {
        foreach (var quest in _questsCollection)
        {
            quest.Completed += OnQuestCompleted;
        }

        foreach (var questObject in _questObjects)
        {
            Completed += questObject.Interact;
        }
    }

    private void Unsubscribe()
    {
        foreach (var quest in _questsCollection)
        {
            quest.Completed -= OnQuestCompleted;
        }
    }

    private void OnQuestCompleted(object sender, IQuest quest)
    {
        var index = _questsCollection.IndexOf(quest);
        if (_currentIndex == index)
        {
            _currentIndex++;
            if (IsDone)
            {
                Debug.Log("Story done!");
                OnCompleted();
            }
        }
        else
        {
            ResetQuests();
        }
    }

    private void ResetQuests()
    {
        _currentIndex = 0;
        foreach (var quest in _questsCollection)
        {
            quest.Reset();
        }
    }
    
    private void OnCompleted()
    {
        Completed?.Invoke();
    }

    #endregion

    #region IQuestStory

    public bool IsDone => _questsCollection.All(value => value.IsCompleted);
  
    public void Dispose()
    {
        Unsubscribe();
        foreach (var quest in _questsCollection)
        {
            quest.Dispose();
        }
    }


    #endregion
}
