using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public sealed class GameOverMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            _ScoreText.text = string.Format(_ScoreText.text, GameLifetime.Instance.GetScore());
            _PressAnyKeyText.gameObject.SetActive(false);
            mCurrentTimeCounter = 0.0f;
        }

        private void Update()
        {
            if (mCurrentTimeCounter < _TimeBeforePressAnyKey)
            {
                mCurrentTimeCounter += Time.deltaTime;
                if (mCurrentTimeCounter >= _TimeBeforePressAnyKey)
                {
                    _PressAnyKeyText.gameObject.SetActive(true);
                }
            }
            else if (Input.anyKeyDown)
            {
                StartCoroutine(ChangeScene());
            }
        }

        private IEnumerator ChangeScene()
        {
            yield return SceneManager.LoadSceneAsync(_NextScene);
            yield return SceneManager.UnloadSceneAsync(this.gameObject.scene);
            yield return Resources.UnloadUnusedAssets();
        }

        private float mCurrentTimeCounter;

        #region InspectorMembers
        [SerializeField] private Text _ScoreText = default;
        [SerializeField] private Text _PressAnyKeyText = default;
        [SerializeField] private float _TimeBeforePressAnyKey = 1.0f;
        [SerializeField] private string _NextScene = string.Empty;
        #endregion
    }
}
