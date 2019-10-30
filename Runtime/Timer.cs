using System;
using UnityEngine;

namespace HouraiTeahouse {

/// <summary>
/// A regular timer that can fire repeatedly based on UnityEngine.Time.
/// </summary>
public sealed class Timer {

    public enum TimerType {
        ScaledTime, UnscaledTime, Realtime
    }

    public readonly TimerType Type;

    /// <summary>
    /// Check if the time since the last interval has passed.
    /// </summary>
    public bool IsCompleted => Now > Started + Interval;

    /// <summary>
    /// The time the last interval started.
    /// </summary>
    public float Started;

    /// <summary>
    /// The length of time, in seconds
    /// If UseUnscaledTime is false, this respects Time.timeScale.
    /// </summary>
    public float Interval;

    float Now {
        get {
            switch (Type) {
                case TimerType.ScaledTime:   return Time.time;
                case TimerType.UnscaledTime: return Time.unscaledTime;
                case TimerType.Realtime:     return Time.realtimeSinceStartup;
            }
            throw new InvalidOperationException();
        }
    }

    public Timer(float interval, TimerType type = TimerType.ScaledTime) {
        Type = type;
        Interval = interval;
        Reset();
    }

    /// <summary>
    /// Utility function for use in loops. Checks if the interval
    /// has completed, if it has then resets the timer.
    /// </summary>
    /// <param name="newInterval">if not null, sets the interval to a new value</param>
    /// <returns>True if the interval has passed and timer was reset, false otherwise.</returns>
    public bool CheckAndReset(float? newInterval = null) {
        var completed = IsCompleted;
        if (completed) {
            Reset();
            Interval = newInterval ?? Interval;
        }
        return completed;
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void Reset() => Started = Now;

}

}
