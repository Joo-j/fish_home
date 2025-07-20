using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishHome
{
    public class Chapter : MonoBehaviour
    {
        private const string PATH_CHAPTER_VIEW = "ChapterView";

        private ChapterView _chapterView = null;

        public void Init()
        {
            _chapterView = GameObject.Instantiate(Resources.Load<ChapterView>(PATH_CHAPTER_VIEW));
            _chapterView.Init();
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var pos = Input.mousePosition;

                var worldPos = Camera.main.ScreenToWorldPoint(pos);
                worldPos.z = 0f;

                var liquid = LiquidPool.Instance.GetLiquid();
                liquid.transform.position = worldPos;
            }
        }
    }

}