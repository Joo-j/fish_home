using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishHome
{
    public class Liquid : MonoBehaviour
    {
        private static readonly Vector3 LIQUID_SCALE = new Vector3(0.3f, 0.3f, 1f);
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public void Init()
        {
            _rigidbody2D.mass = UnityEngine.Random.Range(1f, 2f);
            transform.localScale = LIQUID_SCALE;
        }
    }
}
