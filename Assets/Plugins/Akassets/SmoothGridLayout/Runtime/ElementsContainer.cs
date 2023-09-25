using System;
using UnityEngine;

namespace Akassets.SmoothGridLayout
{
    public class ElementsContainer : MonoBehaviour
    {
        public event Action OnChildrenChanged;
    
        private void OnTransformChildrenChanged()
        {
            OnChildrenChanged?.Invoke();
        }
    }
}
