using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public sealed class Score : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            mGameLifetime = GameLifetime.Instance;
        }

        // Update is called once per frame
        private void Update()
        {
            _ScoreText.text = $"{mGameLifetime.GetScore()}";
        }

        private GameLifetime mGameLifetime;

        #region InspectorMembers
        [SerializeField] private Text _ScoreText = default;
        #endregion
    }
}
