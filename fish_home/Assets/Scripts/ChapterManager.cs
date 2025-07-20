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

        public void Init()
        {
            var path = $"{PATH_CHAPTER}1";
            _chapter = GameObject.Instantiate(Resources.Load<Chapter>(path));
            _chapter.Init();
        }
    }
}
