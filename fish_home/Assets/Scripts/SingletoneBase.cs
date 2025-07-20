using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishHome.Util
{
    public class SingletonBase<T> where T : class, new()
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new T();

                return _instance;
            }
        }
    }
}
