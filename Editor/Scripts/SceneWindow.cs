// Copyright (c) Sperid4815Studio and Contributors
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SperidMobileFramework.Editor
{
    public class SceneWindow : EditorWindow
    {
        private const string StartScenePath = "SceneWindow_StartScenePath";

        [MenuItem("SperidMobileFramework/SceneWindow")]
        public static void Open()
        {
            var window = (SceneWindow)GetWindow(typeof(SceneWindow), false, "SceneWindow");
        }

        private void OnEnable()
        {
            var startScenePath = EditorUserSettings.GetConfigValue(StartScenePath);

            if (string.IsNullOrEmpty(startScenePath))
            {
                return;
            }

            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(startScenePath);
            if (sceneAsset == null)
            {
                Debug.LogWarning($"{startScenePath} is not found");
            }
            else
            {
                EditorSceneManager.playModeStartScene = sceneAsset;
            }
        }

        private void OnGUI()
        {
            var beforeScenePath = string.Empty;
            var afterScenePath = string.Empty;

            if (EditorSceneManager.playModeStartScene != null)
            {
                beforeScenePath = AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene);
            }

            EditorSceneManager.playModeStartScene = (SceneAsset)EditorGUILayout.ObjectField(new GUIContent("Start Scene"), EditorSceneManager.playModeStartScene, typeof(SceneAsset), false);

            if (GUILayout.Button("Play"))
            {
                EditorApplication.ExecuteMenuItem("Edit/Play");
            }

            if (GUILayout.Button("Play Current Scene"))
            {
                EditorSceneManager.playModeStartScene = null;
                EditorApplication.ExecuteMenuItem("Edit/Play");
                return;
            }

            if (EditorSceneManager.playModeStartScene != null)
            {
                afterScenePath = AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene);
            }

            if (beforeScenePath != afterScenePath)
            {
                EditorUserSettings.SetConfigValue(StartScenePath, afterScenePath);
            }
        }
    }

}