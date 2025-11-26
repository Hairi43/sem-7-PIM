using System;

public static class ScoreManager {
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHighScoreChanged;
    public static event Func<int> OnGetScore;
    
    public static void ScoreChanged(int value)
    {
        OnScoreChanged?.Invoke(value);
    }

    public static void HighScoreChanged(int value) {
        OnHighScoreChanged?.Invoke(value);
    }

    public static int GetScore()
    {
        return OnGetScore?.Invoke() ?? 0;
    }
}
