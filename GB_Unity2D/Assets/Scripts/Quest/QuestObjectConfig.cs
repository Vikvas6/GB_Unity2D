using UnityEngine;


[CreateAssetMenu(menuName = "Quest/Object", fileName = "QuestObjectConfig", order = 0)]
public class QuestObjectConfig : ScriptableObject
{
    public int id;
    public QuestObjectType QuestObjectType;
}

public enum QuestObjectType
{
    Activator
}