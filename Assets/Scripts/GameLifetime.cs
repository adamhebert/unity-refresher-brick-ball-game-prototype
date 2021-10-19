using UnityEngine;

public sealed class GameLifetime : MonoBehaviour
{
    public static GameLifetime Instance => GetInstance();
    public long GetScore() => mScore;
    public void AddScore(long score) => mScore += score;
    public void AddDeath() => mRest -= 1;
    public int GetRest() => mRest;
    public bool GameIsOver() => mRest <= 0;

    public void ResetGame()
    {
        this.mScore = 0;
        this.mRest = _StartingRest;
    }

    private static GameLifetime GetInstance()
    {
        if (mInstance != null)
        {
            return mInstance;
        }

        var gameObject = Resources.Load("GameLifetime", typeof(GameObject)) as GameObject;
        var instance = Object.Instantiate(gameObject) as GameObject;

        mInstance = instance.GetComponent<GameLifetime>();
        DontDestroyOnLoad(instance);

        return mInstance;
    }


    private long mScore = 0;
    private int mRest;

    private static GameLifetime mInstance;

    #region InspectorMembers
    [SerializeField] private int _StartingRest = default;
    #endregion
}
