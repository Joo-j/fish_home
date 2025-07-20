using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishHome.Util
{
    public class ColliderInvoker : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider = null;

        private Action<Collider2D, Collider2D> _onTriggerEnter = null;

        public void Init(Action<Collider2D, Collider2D> onTriggerEnter)
        {
            _onTriggerEnter = onTriggerEnter;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _onTriggerEnter?.Invoke(_collider, collision);
        }
    }
}
