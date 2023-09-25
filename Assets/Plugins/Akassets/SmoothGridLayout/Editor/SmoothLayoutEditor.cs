#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Akassets.SmoothGridLayout
{
    [CustomEditor(typeof(SmoothGridLayoutUI))]
    public class SmoothLayoutEditor : Editor
    {
        private const string UndoKey = "Fix Smooth Layout";
        private const string Tooltip = "Will try to fix the problem, you may need to delete unnecessary object after fix";
        private GUIStyle _gridSettingsBackgroundStyle;
        private GUIStyle _labelStyle;
        private SmoothGridLayoutUI _target;
        private Editor _gridEditor;

        private void OnEnable()
        {
            _target = (SmoothGridLayoutUI) target;
            TryCreateGridLayoutEditor();
        }

        private void TryCreateGridLayoutEditor()
        {
            if (_target.gridLayoutGroup != null)
            {
                _gridEditor = CreateEditor(_target.gridLayoutGroup);
            }
        }

        private void OnDisable()
        {
            if (_gridEditor != null)
                DestroyImmediate(_gridEditor);
        }

        public override void OnInspectorGUI()
        {
            _gridSettingsBackgroundStyle = new GUIStyle("helpBox");
            _labelStyle = new GUIStyle("helpBox") {fontSize = 12, alignment = TextAnchor.MiddleCenter};
            
            var needFix = NeedFix();
            if (needFix)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.HelpBox("Something's wrong! Need to fix.", MessageType.Error);
                if (GUILayout.Button(new GUIContent("Autofix", Tooltip), GUILayout.ExpandHeight(true)))
                {
                    AutoFix(); 
                }
                EditorGUILayout.EndHorizontal();
            }
            DrawDefaultInspector();
            GUI.enabled = true;
            DrawGridLayoutGroupInspector();
        }

        private void DrawGridLayoutGroupInspector()
        {
            if (_gridEditor == null || _target.gridLayoutGroup == null)
            {
                TryCreateGridLayoutEditor();
                DrawGridIsNotSelectedWarning();
                return;
            }
            
            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical(_gridSettingsBackgroundStyle);
            EditorGUILayout.LabelField(new GUIContent("Grid settings", "Just a convinient way to display grid layout settings here!"), _labelStyle);
            if(_gridEditor != null) 
                _gridEditor.OnInspectorGUI();
            EditorGUILayout.EndVertical();
        }

        private void DrawGridIsNotSelectedWarning()
        {
            EditorGUILayout.BeginVertical(_gridSettingsBackgroundStyle);
            EditorGUILayout.LabelField(new GUIContent("Grid settings", "Just a convinient way to display grid layout settings here!"), _labelStyle);
            EditorGUILayout.LabelField(new GUIContent("Select \'Grid Layout Group\' to display its settings"));
            EditorGUILayout.EndVertical();
        }

        private void AutoFix()
        {
            if (_target.placeholdersTransform == null)
                FixPlaceholdersTransform();
            if (_target.elementsTransform == null)
                FixElementsTransform();
            if (_target.elementsContainer == null)
                FixElementsContainer();
            if (_target.gridLayoutGroup == null)
                FixGridLayoutGroup();
        }

        private void FixPlaceholdersTransform()
        {
            var placeholdersContainer = new GameObject("New Placeholders Container").AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(placeholdersContainer.gameObject, _target.gameObject);
            ExpandRectTransform(placeholdersContainer);
            Undo.RecordObject(_target, UndoKey);
            _target.placeholdersTransform = placeholdersContainer;
            Undo.RecordObject(_target, UndoKey);
            _target.gridLayoutGroup = placeholdersContainer.gameObject.AddComponent<GridLayoutGroup>();
            Undo.RegisterCreatedObjectUndo(placeholdersContainer.gameObject, UndoKey);
        }

        private void FixElementsTransform()
        {
            var elementsContainer = new GameObject("New Elements Container [Put elements here]").AddComponent<RectTransform>();
            GameObjectUtility.SetParentAndAlign(elementsContainer.gameObject, _target.gameObject);
            Undo.RecordObject(_target, UndoKey);
            _target.elementsTransform = elementsContainer;
            Undo.RecordObject(_target, UndoKey);
            _target.elementsContainer = elementsContainer.gameObject.AddComponent<ElementsContainer>();
            Undo.RegisterCreatedObjectUndo(elementsContainer.gameObject, UndoKey);
        }

        private void FixElementsContainer()
        {
            if (_target.elementsContainer == null)
            {
                var elementsContainer = _target.GetComponentInChildren<ElementsContainer>();
                if (elementsContainer != null)
                {
                    Undo.RecordObject(_target, UndoKey);
                    _target.elementsContainer = elementsContainer;
                }
                else
                {
                    FixElementsTransform();
                }
            }
        }

        private void FixGridLayoutGroup()
        {
            if (_target.gridLayoutGroup == null)
            {
                var gridLayoutGroup = _target.GetComponentInChildren<GridLayoutGroup>();
                if (gridLayoutGroup != null)
                {
                    Undo.RecordObject(_target, UndoKey);
                    _target.gridLayoutGroup = gridLayoutGroup;
                }
                else
                {
                    FixPlaceholdersTransform();
                }
            }
        }

        private static void ExpandRectTransform(RectTransform smoothLayout)
        {
            smoothLayout.anchorMin = new Vector2(0, 0);
            smoothLayout.anchorMax = new Vector2(1, 1);
            smoothLayout.offsetMax = new Vector2(0, 0);
            smoothLayout.offsetMin = new Vector2(0, 0);
        }

        private bool NeedFix()
        {
            return _target.placeholdersTransform == null ||
                   _target.elementsTransform == null ||
                   _target.elementsContainer == null ||
                   _target.gridLayoutGroup == null;
        }
    }
}

#endif