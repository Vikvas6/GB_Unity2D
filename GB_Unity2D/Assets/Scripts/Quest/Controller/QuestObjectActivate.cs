using UnityEngine;


public class QuestObjectActivate : IQuestObject
{
    private GameObject _gameObject;

    public QuestObjectActivate(GameObject gameObject)
    {
        _gameObject = gameObject;
    }
    
    public void Interact()
    {
        _gameObject.SetActive(true);
    }
}
