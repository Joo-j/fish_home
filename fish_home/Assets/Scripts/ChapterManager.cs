using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishHome.Util;

namespace FishHome
{
    public class ChapterManager : SingletonBase<ChapterManager>
    {
        private const string PATH_CHAPTER = "Chapter/Chapter_";
        private Chapter _chapter = null;

        private int _index = 1;

        public void Init()
        {
            CreateChapter();
        }

        public void CreateChapter()
        {
            if (_index <= 0)
                return;

            var path = $"{PATH_CHAPTER}{_index}";
            var res = Resources.Load<Chapter>(path);
            if (null == res)
            {
                --_index;
                CreateChapter();
                return;
            }

            _chapter = GameObject.Instantiate(res);
            _chapter.Init(ClearChapter);
        }

        private void ClearChapter()
        {
            _chapter.Release();
            _chapter = null;
            ++_index;

            CreateChapter();
        }
    }
}
