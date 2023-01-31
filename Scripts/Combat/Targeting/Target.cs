using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> onDestory;
        private void OnDestroy()
        {
            onDestory?.Invoke(this);
        }
    }
}

