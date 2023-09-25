using UnityEngine;

namespace Akassets.SmoothGridLayout
{
    [RequireComponent(typeof(RectTransform))]
    public class LerpToPlaceholder : MonoBehaviour
    {
        private RectTransform _rectTransform;
        
        public SmoothGridLayoutUI smoothGridLayout;
        public Transform placeholderTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_rectTransform == null || placeholderTransform == null || smoothGridLayout == null) return;
            if (placeholderTransform.transform.position.sqrMagnitude < 1) return;
            transform.position = Vector3.Lerp(transform.position, placeholderTransform.position, Time.deltaTime * smoothGridLayout.lerpSpeed);
        }
    }
}