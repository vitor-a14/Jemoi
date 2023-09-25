using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Akassets.SmoothGridLayout
{
    public class SmoothGridLayoutUI : MonoBehaviour
    {
        [Range(0f, 1000f)]
        public float lerpSpeed = 10;
        public RectTransform placeholdersTransform;
        public RectTransform elementsTransform;
        public ElementsContainer elementsContainer;
        public GridLayoutGroup gridLayoutGroup;

        private void Awake()
        {
            if(placeholdersTransform == null)
                Debug.LogError("<color=lightblue><b>Smooth Grid Layout</b></color> ► <color=red>\"Placeholders Transform\" is null.</color> Assign field in the inspector or use autofix.", this);
            if(elementsTransform == null)
                Debug.LogError("<color=lightblue><b>Smooth Grid Layout</b></color> ► <color=red>\"Elements Transform\" is null.</color> Assign field in the inspector or use autofix.", this);
            if(elementsContainer == null)
                Debug.LogError("<color=lightblue><b>Smooth Grid Layout</b></color> ► <color=red>\"Elements Container\" is null.</color> Assign field in the inspector or use autofix.", this);
            if(gridLayoutGroup == null)
                Debug.LogError("<color=lightblue><b>Smooth Grid Layout</b></color> ► <color=red>\"Elements Transform\" is null.</color> Assign field in the inspector or use autofix.", this);

            elementsContainer.OnChildrenChanged += RebuildChildren;
        }

        private void Start()
        {
            RebuildChildren();
        }

        private void OnDestroy()
        {
            elementsContainer.OnChildrenChanged -= RebuildChildren;
        }

        private void RebuildChildren()
        {
            var childTransforms = placeholdersTransform.Cast<Transform>().ToList();
            foreach (var child in childTransforms)
                DestroyImmediate(child.gameObject);
            
            foreach (Transform element in elementsTransform)
            {
                if (element.TryGetComponent(out RectTransform rectTransform))
                {
                    AddElement(rectTransform);
                }
                else
                {
                    var rect = element.gameObject.AddComponent<RectTransform>();
                    AddElement(rect);
                }
            }
        }

        private void AddElement(RectTransform element)
        {
            var placeholder = new GameObject($"{element.name} placeholder");
            var placeholderRect = placeholder.AddComponent<RectTransform>();
            element.anchorMax = new Vector2(0.5f, 0.5f);
            element.anchorMin = new Vector2(0.5f, 0.5f);
            element.sizeDelta = gridLayoutGroup.cellSize;
            placeholderRect.sizeDelta = gridLayoutGroup.cellSize;
            placeholder.transform.SetParent(placeholdersTransform);

            if (element.gameObject.TryGetComponent(out LerpToPlaceholder lerpToPlaceholder))
            {
                lerpToPlaceholder.placeholderTransform = placeholderRect;
                lerpToPlaceholder.smoothGridLayout = this;
                return;
            }

            lerpToPlaceholder = element.gameObject.AddComponent<LerpToPlaceholder>();
            lerpToPlaceholder.placeholderTransform = placeholderRect;
            lerpToPlaceholder.smoothGridLayout = this;
        }
    }
}