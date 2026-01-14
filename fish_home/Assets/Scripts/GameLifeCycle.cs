using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishHome
{
    public class GameLifeCycle : MonoBehaviour
    {
        void Start()
        {
            StageManager.Instance.Init();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
