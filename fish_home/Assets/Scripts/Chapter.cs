using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishHome.Util;

namespace FishHome
{
    public class Chapter : MonoBehaviour
    {
        [SerializeField] private ColliderInvoker _colliderInvoker = null;

        private const string PATH_CHAPTER_VIEW = "ChapterView";
        private const float SPAWN_INTERVAL = 0.05f;
        private const float SPAWN_Y_POS = 9f;

        private ChapterView _chapterView = null;
        private Action _onClear = null;
        private DateTime _lastSpawnTime = DateTime.MinValue;

        public void Init(Action onClear)
        {
            _onClear = onClear;
            _chapterView = GameObject.Instantiate(Resources.Load<ChapterView>(PATH_CHAPTER_VIEW));
            _chapterView.Init();

            _colliderInvoker.Init(OnEnterDestination);
        }

        public void Release()
        {
            LiquidPool.Instance.ReturnAllLiquids();
            _onClear = null;
            _chapterView = null;
            _colliderInvoker = null;
            GameObject.Destroy(gameObject);
        }

        void Update()
        {
            if (true == Input.GetMouseButton(0))
            {
                var pos = Input.mousePosition;

                var worldPos = Camera.main.ScreenToWorldPoint(pos);
                worldPos.z = 0f;
                worldPos.y = SPAWN_Y_POS;

                SpawnLiquid(worldPos);
            }
        }

        private void SpawnLiquid(Vector3 pos)
        {
            if (DateTime.Now < _lastSpawnTime.AddSeconds(SPAWN_INTERVAL))
                return;

            _lastSpawnTime = DateTime.Now;

            var liquid = LiquidPool.Instance.GetLiquid();
            liquid.transform.position = pos;
        }

        private void OnEnterDestination(Collider2D my, Collider2D other)
        {
            if (false == other.TryGetComponent<Fish>(out var fish))
                return;

            _onClear?.Invoke();
        }
    }
}