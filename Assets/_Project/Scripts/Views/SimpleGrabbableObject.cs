using System;
using _Project.Scripts.Views.Interface;
using UnityEngine;

namespace _Project.Scripts.Views
{
    public class SimpleGrabbableObject : MonoBehaviour, ICanBeDetected
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public event Action OnDestroy;
        public Vector3 Position =>  transform.position;
        public void ReactToDetection()
        {
            Debug.Log("ReactToDetection");
        }
    }
}
