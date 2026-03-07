using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;

    private void Awake()
    {
        instance = this;
    }

    public void AddPoints(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}