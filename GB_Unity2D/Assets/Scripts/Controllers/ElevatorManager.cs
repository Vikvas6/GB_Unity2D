using System;
using System.Collections.Generic;
using UnityEngine;


public class ElevatorManager : IDisposable
{
    private List<CharacterView> _elevatorViews;
    private List<CharacterView> _limitViews;
    
    public ElevatorManager(List<CharacterView> elevatorViews, List<CharacterView> limitViews)
    {
        _elevatorViews = elevatorViews;
        _limitViews = limitViews;

        for (int i = 0; i < _limitViews.Count; i++)
        {
            _limitViews[i].OnLevelObjectContact += TurnElevator;
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < _limitViews.Count; i++)
        {
            _limitViews[i].OnLevelObjectContact -= TurnElevator;
        }
    }

    private void TurnElevator(CharacterView other)
    {
        if (_elevatorViews.Contains(other))
        {
            var motor = other.GetComponent<SliderJoint2D>().motor;
            motor.motorSpeed = -motor.motorSpeed;
            other.GetComponent<SliderJoint2D>().motor = motor;
        }
    }
}
