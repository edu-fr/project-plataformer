using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPlataformer
{
    public class Cam : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        public float smoothSpeed = 0.125f; // the higher this value is, the faster our camera is going to lock on our target (less time smoothing) 

        private void FixedUpdate()
        {
            var desiredPosition = new Vector3(target.position.x, 0, 0) + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}