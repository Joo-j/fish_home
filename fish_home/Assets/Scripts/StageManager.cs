using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishHome.Util;

namespace FishHome
{
    public class StageManager : SingletonBase<StageManager>
    {
        private const string PATH_STAGE = "Stage/Stage_";
        private Stage _stage = null;

        private int _index = 1;

        public void Init()
        {
            CreateStage();
        }

        public void CreateStage()
        {
            if (_index <= 0)
                return;

            var path = $"{PATH_STAGE}{_index}";
            var res = Resources.Load<Stage>(path);
            if (null == res)
            {
                --_index;
                CreateStage();
                return;
            }

            _stage = GameObject.Instantiate(res);
            _stage.Init(ClearStage);
        }

        private void ClearStage()
        {
            _stage.Release();
            _stage = null;
            ++_index;

            CreateStage();
        }
    }
}
