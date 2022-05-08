using UnityEngine;

public static class Difficulty
{
    static float _secondsToMaxDifficulty = 60;

    public static float SecondsToMaxDifficulty{ get => _secondsToMaxDifficulty; }

    public static float GetDifficultyPercent()
    {
        if (Time.time >= _secondsToMaxDifficulty)
            return 1;

        return Mathf.Clamp01(Time.time / _secondsToMaxDifficulty);
    }
}
