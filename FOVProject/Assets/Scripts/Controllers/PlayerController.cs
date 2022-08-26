using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace fov
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector3 lineVector1;
        [SerializeField] private Vector3 lineVector2;
        [Range(-1f, 0)]
        [SerializeField] private float fovValue = -0.41f;
        
        [SerializeField] private float playerRadius;
        
        private RaycastHit _hit;
        private Vector3 _mousePos;
        private Vector3 _direction;

        // private TargetController[] _targets;

        private void Update()
        {
            PlayerLook();
            ViewAngle();
        }

        private void PlayerLook()
        {
            _mousePos = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(_mousePos);

            if (Physics.Raycast(ray, out _hit))
            {
                Debug.DrawLine(Camera.main.transform.position, _hit.point, Color.red);
                
                this.transform.LookAt(_hit.point);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }

        private void FindTargets()
        {
            
        }

        private void ViewAngle()
        {
        }

#if UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            lineVector1.x = fovValue;
            lineVector2.x = -fovValue;
            Handles.DrawWireDisc(transform.position, transform.up, playerRadius);

            var playerPos = transform.localPosition;
            
            var linePos = lineVector1 * playerRadius;
            Handles.color = Color.magenta;
            var forwardVector = (playerPos + linePos);
            Handles.DrawLine(playerPos, forwardVector, 5.0f);
            float angle = Mathf.Atan2(linePos.y, linePos.x) * Mathf.Rad2Deg;
            Debug.Log("Angle: " + angle);
            lineVector1.z = Mathf.Sqrt(Mathf.Pow(playerRadius, 2) - Mathf.Pow(linePos.x, 2)) / playerRadius;
            
            var linePos2 = lineVector2 * playerRadius;
            Handles.color = Color.blue;
            var forwardVector2 = (playerPos + linePos2);
            Handles.DrawLine(playerPos, forwardVector2, 5.0f);
            float angle2 = Mathf.Atan2(linePos2.y, linePos2.x) * Mathf.Rad2Deg;
            Debug.Log("Angle: " + angle2);
            lineVector2.z = Mathf.Sqrt(Mathf.Pow(playerRadius, 2) - Mathf.Pow(linePos2.x, 2)) / playerRadius;

            var dotProduct = Vector3.Dot(lineVector1, lineVector2);

            float angleRadian = Mathf.Acos(dotProduct);

            Handles.DrawWireArc(transform.position, Vector3.up, lineVector1, angleRadian * Mathf.Rad2Deg, 1.0f, 2.0f);

            Handles.color = Color.white;
        }
#endif
        private void CalculateLine(Vector3 line)
        {
            var playerPos = transform.localPosition;
            
            var linePos = line * playerRadius;
            
            Handles.color = Color.magenta; 
            
            var forwardVector = (playerPos + linePos);
            
            Handles.DrawLine(playerPos, forwardVector, 5.0f);
            
            float angle = Mathf.Atan2(linePos.y, linePos.x) * Mathf.Rad2Deg;
            Debug.Log("Angle: " + angle);
            
            line.z = Mathf.Sqrt(Mathf.Pow(playerRadius, 2) - Mathf.Pow(linePos.x, 2)) / playerRadius;
        }
    }
}