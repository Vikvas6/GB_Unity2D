using System;


public interface IQuestStory : IDisposable
{
    bool IsDone { get; }
    event Action Completed;
}
