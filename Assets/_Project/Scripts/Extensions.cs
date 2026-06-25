using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    static class Extensions
    {
        public static TComponent[] Extract<TComponent>(this IEnumerable<GameObject> source) where TComponent : Component
        {
            return source.Select(go => go != null ? go.GetComponent<TComponent>() : null).ToArray();
        }
        public static TInterface[] ExtractInterface<TInterface>(this IEnumerable<GameObject> source) where TInterface
            : class
        {
            if (source == null) return Array.Empty<TInterface>();
            var gameObjects = source.ToList();
            
            TInterface[] result = new TInterface[gameObjects.Count()];

            for (var i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i] == null)
                {
                    result[i] = null;
                    continue;
                }
                result[i] = gameObjects[i].GetComponent<TInterface>();
            }
            return result;
        } 
    }
}