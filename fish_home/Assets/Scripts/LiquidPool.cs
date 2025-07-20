using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FishHome.Util;

namespace FishHome
{
    public class LiquidPool : SingletonBase<LiquidPool>
    {
        private const int INITIAL_POOL_SIZE = 20;
        private Transform _poolTF = null;
        private Queue<Liquid> _liquidPool = null;
        private List<Liquid> _activeLiquids = null;
        private Liquid _liquidPrefab = null;

        public LiquidPool()
        {
            _liquidPool = new Queue<Liquid>();
            _activeLiquids = new List<Liquid>();

            _liquidPrefab = Resources.Load<Liquid>("Liquid");
            _poolTF = new GameObject("LiquidPool").transform;

            for (int i = 0; i < INITIAL_POOL_SIZE; i++)
            {
                CreateLiquid();
            }
        }

        private void CreateLiquid()
        {
            var liquid = GameObject.Instantiate<Liquid>(_liquidPrefab, _poolTF);
            liquid.Init();
            liquid.gameObject.SetActive(false);
            _liquidPool.Enqueue(liquid);
        }

        public Liquid GetLiquid()
        {
            if (_liquidPool.Count <= 0)
                CreateLiquid();

            var liquid = _liquidPool.Dequeue();

            liquid.gameObject.SetActive(true);
            _activeLiquids.Add(liquid);

            return liquid;
        }

        public void ReturnLiquid(Liquid liquid)
        {
            liquid.gameObject.SetActive(false);
            liquid.transform.SetParent(_poolTF);

            _activeLiquids.Remove(liquid);
            _liquidPool.Enqueue(liquid);
        }

        public void ReturnAllLiquids()
        {
            for (int i = _activeLiquids.Count - 1; i >= 0; i--)
            {
                ReturnLiquid(_activeLiquids[i]);
            }
        }
    }
}
