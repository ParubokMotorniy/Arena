using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GamePhysics 
{
    public class HangingLine : MonoBehaviour
    {
        [field: SerializeField] public List<Transform> points { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if(points.Count >= 2)
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    Gizmos.DrawLine(points[i].position, points[i + 1].position);
                }
            }
        }
    }

}

