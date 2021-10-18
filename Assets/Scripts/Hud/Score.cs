using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public sealed class Score : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            mGameLifetime = FindObjectOfType<GameLifetime>();
        }

        // Update is called once per frame
        void Update()
        {
            mScoreText.text = $"{mGameLifetime.GetScore()}";
        }

        [SerializeField] private Text mScoreText;

        private GameLifetime mGameLifetime;
    }
}
