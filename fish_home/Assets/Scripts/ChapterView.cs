using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FishHome
{
    public class ChapterView : MonoBehaviour
    {
        [SerializeField] private RectTransform _progressBar;
        [SerializeField] private Image _progress;

        private Vector3 _progressBarSize;
        private Action _onReset = null;

        public void Init(Action onReset)
        {
            _progressBarSize = _progressBar.sizeDelta;
            _onReset = onReset;
        }

        public void UpdateProgress(float progress)
        {
            var width = Mathf.Lerp(0, _progressBarSize.x, progress);
            _progress.rectTransform.sizeDelta = new Vector2(width, _progressBarSize.y);
        }

        public void OnClickReset()
        {
            _onReset?.Invoke();
        }
    }
}
