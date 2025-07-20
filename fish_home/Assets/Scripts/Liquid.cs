using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishHome
{
    public class Liquid : MonoBehaviour
    {
        [SerializeField] private float _scale = 0.2f;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _index = 0;

        public void Init(int index)
        {
            _index = index;
            transform.localScale = Vector3.one * _scale;

            var value = 0.01f * _index;
            _rigidbody2D.mass += value;

            _spriteRenderer.color = Color.Lerp(Color.red, Color.black, value);
        }
    }
}
