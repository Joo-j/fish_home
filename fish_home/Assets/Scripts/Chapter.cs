using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishHome.Util;

namespace FishHome
{
    public class Chapter : MonoBehaviour
    {
        [SerializeField] private Transform _fishSpawnPoint = null;
        [SerializeField] private ColliderInvoker _colliderInvoker = null;
        [SerializeField] private int _maxLiquidCount = 1000;

        private const string PATH_CHAPTER_VIEW = "ChapterView";
        private const string PATH_FISH = "Fish";
        private const float SPAWN_INTERVAL = 0.05f;
        private const float SPAWN_Y_POS = 9f;

        private ChapterView _chapterView = null;
        private Action _onClear = null;
        private DateTime _lastSpawnTime = DateTime.MinValue;
        private int _liquidCount = 0;
        private Fish _fish = null;

        public void Init(Action onClear)
        {
            _onClear = onClear;
            _chapterView = GameObject.Instantiate(Resources.Load<ChapterView>(PATH_CHAPTER_VIEW));
            _chapterView.Init(OnClickReset);

            _colliderInvoker.Init(OnEnterDestination);

            SpawnFish();
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

        private void SpawnFish()
        {
            var fishRes = Resources.Load<Fish>(PATH_FISH);
            _fish = GameObject.Instantiate(fishRes, _fishSpawnPoint);
            _fish.Init();
        }

        private void SpawnLiquid(Vector3 pos)
        {
            if (_liquidCount >= _maxLiquidCount)
                return;

            if (DateTime.Now < _lastSpawnTime.AddSeconds(SPAWN_INTERVAL))
                return;

            _lastSpawnTime = DateTime.Now;

            var liquid = LiquidPool.Instance.GetLiquid();
            liquid.transform.position = pos;
            ++_liquidCount;

            _chapterView.UpdateProgress((float)(_maxLiquidCount - _liquidCount) / _maxLiquidCount);
        }

        private void OnEnterDestination(Collider2D my, Collider2D other)
        {
            if (false == other.TryGetComponent<Fish>(out var fish))
                return;

            _onClear?.Invoke();
        }

        private void OnClickReset()
        {
            LiquidPool.Instance.ReturnAllLiquids();
            _liquidCount = 0;
            _chapterView.UpdateProgress((float)(_maxLiquidCount - _liquidCount) / _maxLiquidCount);
            GameObject.Destroy(_fish.gameObject);
            _fish = null;
            SpawnFish();
        }
    }
}