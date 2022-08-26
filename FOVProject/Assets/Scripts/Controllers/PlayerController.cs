using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Vector3 = UnityEngine.Vector3;

namespace fov
{
    public class PlayerController : MonoBehaviour
    {
        private RaycastHit _hit;
        private Vector3 _mousePos;
        private Vector3 _direction;

        private void Update()
        {
            PlayerLook();
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
    }
}