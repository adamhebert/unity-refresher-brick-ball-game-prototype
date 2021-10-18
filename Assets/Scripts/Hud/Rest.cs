using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public sealed class Rest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            mGameLifetime = FindObjectOfType<GameLifetime>();
        }

        // Update is called once per frame
        void Update()
        {
            mScoreText.text = $"{mGameLifetime.GetRest()}";
        }

        [SerializeField] private Text mScoreText;

        private GameLifetime mGameLifetime;
    }
}
