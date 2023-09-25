#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Akassets.SmoothGridLayout
{
    public static class SmoothLayoutEditorCreateMenu
    {
        [MenuItem("GameObject/UI/Smooth Grid Layout", false, 10)]
        private static void CreateSmoothLayoutGrid(MenuCommand menuCommand)
        {
            var smoothLayout = new GameObject("Smooth Grid Layout").AddComponent<RectTransform>();
            var elementsContainer = new GameObject("Elements Container [Put elements here]").AddComponent<RectTransform>();
            var placeholdersContainer = new GameObject("Placeholders Container").AddComponent<RectTransform>();

            AddBackgroundImage(smoothLayout);
            
            elementsContainer.transform.SetParent(smoothLayout.transform);
            placeholdersContainer.transform.SetParent(smoothLayout.transform);
            ExpandRectTransform(smoothLayout);
            ExpandRectTransform(placeholdersContainer);
            
            var smoothLayoutComp = smoothLayout.gameObject.AddComponent<SmoothGridLayoutUI>();
            smoothLayoutComp.elementsTransform = elementsContainer;
            smoothLayoutComp.placeholdersTransform = placeholdersContainer;
            smoothLayoutComp.gridLayoutGroup = placeholdersContainer.gameObject.AddComponent<GridLayoutGroup>();
            smoothLayoutComp.elementsContainer = elementsContainer.gameObject.AddComponent<ElementsContainer>();
            
            
            GameObject parentGameObject;
            var contextGameObject = menuCommand.context as GameObject;
            if (contextGameObject == null || !contextGameObject.TryGetComponent(out RectTransform rectTransform))
            {
                EnsureEventSystemExists();
                var canvasGameObject = CreateCanvasGameObject();
                parentGameObject = canvasGameObject;
            }
            else
            {
                EnsureEventSystemExists();
                parentGameObject = contextGameObject;
            }

            GameObjectUtility.SetParentAndAlign(smoothLayout.gameObject, parentGameObject);
            Undo.RegisterCreatedObjectUndo(smoothLayout.gameObject, "Create Smooth Layout");
            Selection.activeObject = smoothLayout;
        }

        private static void AddBackgroundImage(RectTransform transform)
        {
            var sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            var image = transform.gameObject.AddComponent<Image>();
            image.type = Image.Type.Sliced;
            image.fillCenter = true;
            image.sprite = sprite;
            image.color = new Color(1, 1, 1, 0.4f);
        }

        private static void EnsureEventSystemExists()
        {
            var eventSystem = Object.FindObjectOfType<EventSystem>();
            if (eventSystem == null) CreateEventSystemGameObject();
        }

        private static GameObject CreateCanvasGameObject()
        {
            GameObject canvasGameObject = new GameObject("Canvas");
            canvasGameObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGameObject.AddComponent<CanvasScaler>();
            canvasGameObject.AddComponent<GraphicRaycaster>();
            return canvasGameObject;
        }

        private static void CreateEventSystemGameObject()
        {
            var eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        private static void ExpandRectTransform(RectTransform smoothLayout)
        {
            smoothLayout.anchorMin = new Vector2(0, 0);
            smoothLayout.anchorMax = new Vector2(1, 1);
            smoothLayout.offsetMax = new Vector2(0, 0);
            smoothLayout.offsetMin = new Vector2(0, 0);
        }
    }
}
#endif