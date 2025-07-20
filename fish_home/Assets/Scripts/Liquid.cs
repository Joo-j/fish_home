using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishHome
{
    public class Liquid : MonoBehaviour
    {
        [SerializeField] private float _scale = 0.2f;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public void Init()
        {
            transform.localScale = Vector3.one * _scale;
            _rigidbody2D.mass = UnityEngine.Random.Range(1f, 2f);
        }
    }
}
