using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GamePhysics
{
    public class LedgeDetector : MonoBehaviour
    {
        public event Action<Vector3,Vector3> OnLedgeDetect;  
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Ledge detected");

            HangingLine line;
            Vector3 closestPoint = other.ClosestPointOnBounds(transform.position);
            Vector3 playerForward = other.transform.forward;

            if(other.TryGetComponent<HangingLine>(out line))
            {
                CalculateClosestPoint(line.points, transform.position, out playerForward, out closestPoint);
            }

            OnLedgeDetect?.Invoke(playerForward, closestPoint);
            Debug.DrawLine(Vector3.zero,closestPoint,Color.blue,5);
            Debug.DrawRay(transform.position, playerForward, Color.red, 5);
        }
        private void CalculateClosestPoint(List<Transform> linePoints, Vector3 point,out Vector3 forward,out Vector3 closestPoint)
        {
            Vector3 closestPointToCompare = Vector3.positiveInfinity;
            Vector3 finalForward = Vector3.positiveInfinity;
            for (int i = 0; i < (linePoints.Count - 1); i++)
            {
                Vector3 startPoint = linePoints[i].position;
                Vector3 endPoint = linePoints[i + 1].position;

                Vector3 lineDirection = endPoint - startPoint;
                Vector3 slopeDirection = point - startPoint;

                float distance = (Vector3.Cross(lineDirection, endPoint - point)).magnitude / lineDirection.magnitude;
                float angle = Vector3.Angle(lineDirection, slopeDirection);
                float extraLength = distance / Mathf.Tan(angle * Mathf.Deg2Rad);

                Vector3 localClosestPoint = startPoint + lineDirection.normalized * extraLength;

                if ((localClosestPoint - startPoint).magnitude > lineDirection.magnitude)
                {
                    localClosestPoint = endPoint;
                }
                if ((localClosestPoint - endPoint).magnitude > lineDirection.magnitude)
                {
                    localClosestPoint = startPoint;
                }

                if (Vector3.Distance(point, localClosestPoint) < Vector3.Distance(point, closestPointToCompare))
                {
                    closestPointToCompare = localClosestPoint;

                    lineDirection.Normalize();
                    finalForward = new Vector3(-lineDirection.z,0,lineDirection.x);
                }
            }
            closestPoint = closestPointToCompare;
            forward = finalForward;
        }
    }
}

