using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace RPG.Combat
{
    public class Targeter : MonoBehaviour
    {
        [SerializeField] private CinemachineTargetGroup targetGroup;
        private Camera mainCamera;
        private List<Target> targets = new List<Target>();
        public Target currentTarget { get; private set; }
        // Start is called before the first frame update
        private void Start()
        {
            mainCamera = Camera.main;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Target>(out Target target))
            {
                targets.Add(target);
                target.onDestory += RemoveTarget;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Target>(out Target target))
            {
                RemoveTarget(target);
            }
        }
        public bool SelectTarget()
        {
            if(targets.Count == 0) { return false; }

            Target closestTarget = null;
            float closestTargetDistance = Mathf.Infinity;

            foreach(Target target in targets)
            {
                Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

                if(target.GetComponentInChildren<Renderer>().isVisible)
                {
                    Vector2 toCenter = viewPos - new Vector2(0.5f,0.5f);
                    if(toCenter.sqrMagnitude < closestTargetDistance)
                    {
                        closestTarget = target;
                        closestTargetDistance = toCenter.sqrMagnitude;
                    }
                }
                else
                {
                    continue;
                }
            }

            if(closestTarget == null) { return false; }

            currentTarget = closestTarget;
            targetGroup.AddMember(currentTarget.transform,1f,2f);

            return true;
        }
        public void Unlock()
        {
            if(currentTarget == null) { return; }
            targetGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
        }
        private void RemoveTarget(Target target)
        {
            if(currentTarget == target)
            {
                targetGroup.RemoveMember(currentTarget.transform);
                currentTarget = null;
            }
            target.onDestory -= RemoveTarget;
            targets.Remove(target);
        }
    }
}

