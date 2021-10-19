using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public sealed class MainMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            mLerpColors = new LinkedList<Color>(_LerpColors);
            if (!mLerpColors.Any())
            {
                mLerpColors.AddFirst(Color.white);
            }

            mCurrentColor = mLerpColors.First;
            mNextColor = GetNextNode(mCurrentColor);
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                GameLifetime.Instance.ResetGame();

                // TODO: Some kind of loading scene, maybe with different menu options...but we're in demo mode here :)
                StartCoroutine(ChangeScene());
            }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            const float maxLerp = 1.0f;

            _Title.color = Color.Lerp(mCurrentColor.Value, mNextColor.Value, mLerpTime);
            mLerpTime += Time.fixedDeltaTime * _LerpSpeed;

            if (mLerpTime >= maxLerp)
            {
                mLerpTime = mLerpTime - maxLerp;
                mCurrentColor = mNextColor;
                mNextColor = GetNextNode(mNextColor);
            }
        }

        private IEnumerator ChangeScene()
        {
            yield return SceneManager.LoadSceneAsync(_NextScene);
            yield return SceneManager.UnloadSceneAsync(this.gameObject.scene);
            yield return Resources.UnloadUnusedAssets();
        }

        private static LinkedListNode<T> GetNextNode<T>(LinkedListNode<T> node) =>
            node.Next ?? node.List.First;

        private float mLerpTime = 0.0f;
        private LinkedList<Color> mLerpColors;
        private LinkedListNode<Color> mCurrentColor;
        private LinkedListNode<Color> mNextColor;

        #region InspectorMembers
        [SerializeField] private Text _Title = default;
        [SerializeField] private float _LerpSpeed = 1.0f;
        [SerializeField] private Color[] _LerpColors = default;
        [SerializeField] private string _NextScene = string.Empty;
        #endregion
    }
}
