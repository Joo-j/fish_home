using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishHome
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _scale;
        [SerializeField] private float _buoyancy = 10;

        private int _liquidLayer = 0;

        public void Init()
        {
            _liquidLayer = LayerMask.NameToLayer("Liquid");
            transform.localScale = Vector3.one * _scale;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            var layer = other.gameObject.layer;
            if (layer == _liquidLayer)
            {
                _rigidbody2D.AddForce(Vector2.up * _buoyancy);
            }
        }
    }
}