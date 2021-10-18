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
            _InitInternals();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void _InitInternals()
    {
        this.mScore = 0;
        this.mRest = mStartingRest;
    }

    public long GetScore() => mScore;
    public void AddScore(long score) => mScore += score;
    public void AddDeath() => mRest -= 1;
    public int GetRest() => mRest;

    [SerializeField] private int mStartingRest;

    private long mScore = 0;
    private int mRest;

    private static GameLifetime mInstance;
}
