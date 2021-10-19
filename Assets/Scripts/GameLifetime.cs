using UnityEngine;

public sealed class GameLifetime : MonoBehaviour
{
    private void Awake()
    {
        if (mInstance != null && mInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            mInstance = this;
            InitInternals();
        }
    }

    private void InitInternals()
    {
        this.mScore = 0;
        this.mRest = _StartingRest;
    }

    public long GetScore() => mScore;
    public void AddScore(long score) => mScore += score;
    public void AddDeath() => mRest -= 1;
    public int GetRest() => mRest;


    private long mScore = 0;
    private int mRest;

    private static GameLifetime mInstance;

    #region InspectorMembers
    [SerializeField] private int _StartingRest = default;
    #endregion
}
