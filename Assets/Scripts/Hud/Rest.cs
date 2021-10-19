using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public sealed class Rest : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            mGameLifetime = FindObjectOfType<GameLifetime>();
        }

        // Update is called once per frame
        private void Update()
        {
            _ScoreText.text = $"{mGameLifetime.GetRest()}";
        }

        private GameLifetime mGameLifetime;

        #region InspectorMembers
        [SerializeField] private Text _ScoreText = default;
        #endregion
    }
}
