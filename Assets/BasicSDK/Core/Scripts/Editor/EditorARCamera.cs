using UnityEngine;
using UnityEditor;
using TesseractBasic.ThirdParty;

namespace TesseractBasic {
	[CustomEditor (typeof (ARCamera))]
	[CanEditMultipleObjects]
	public class MyScriptEditor : Editor {

		SerializedProperty APIKey;

		/// <summary>
		/// Enable this if using Third Party SDK
		/// </summary>
		///
		SerializedProperty UseThirdPartySDKProp;

		SerializedProperty SDKToUseProp;
		SerializedProperty ThirdPartyGameObjectProp;
		SerializedProperty EnableHybridTrackingProp;

		SerializedProperty UseHeadTrackingProp;

		SerializedProperty LeftCamProp, RightCamProp;
		SerializedProperty eyeSeparationProp;
		SerializedProperty recenterPoint;
		private bool foldout_Cameras;

		private void OnEnable ()
		{
			APIKey = serializedObject.FindProperty ("APIKey");
			UseThirdPartySDKProp = serializedObject.FindProperty ("UseThirdPartySDK");
			SDKToUseProp = serializedObject.FindProperty ("SDKToUse");
			ThirdPartyGameObjectProp = serializedObject.FindProperty ("ThirdPartyTrackerGameObject");
			EnableHybridTrackingProp = serializedObject.FindProperty ("EnableHybridTracking");
			UseHeadTrackingProp = serializedObject.FindProperty ("UseHeadTracking");
			LeftCamProp = serializedObject.FindProperty ("Left");
			RightCamProp = serializedObject.FindProperty ("Right");
			eyeSeparationProp = serializedObject.FindProperty ("eyeSeparation");
			recenterPoint = serializedObject.FindProperty ("RecenterPoint");
		}

		override public void OnInspectorGUI ()
		{
			serializedObject.Update ();

			ARCamera go = target as ARCamera;

			EditorGUILayout.PropertyField (APIKey, new GUIContent ("Developer API Key","Enter your developer API Key"));

			UseThirdPartySDKProp.boolValue = EditorGUILayout.Foldout (UseThirdPartySDKProp.boolValue, new GUIContent ("Use Third Party SDK", "Enable this to select Third Party SDK"));

			if (UseThirdPartySDKProp.boolValue) {

				EditorGUILayout.PropertyField (SDKToUseProp, new GUIContent ("SDK To Use", "The Third party SDK to use"));
				EditorGUILayout.PropertyField (ThirdPartyGameObjectProp, new GUIContent ("Third Party GameObject To Track", "Attach the GameObject that is tracked by the Third Party SDK to mimic its pose"));
				EditorGUILayout.PropertyField (EnableHybridTrackingProp, new GUIContent ("Enable Hybrid Tracking", "Hybrid tracking makes use of the best of both Holoboard and Third Party SDKs wherever possible (Experimental)"));

				if (SDKToUseProp.enumValueIndex == (int)ThirdPartySDK.Vuforia) {
					EditorGUILayout.LabelField ("Make sure you add the \"VuforiaHoloboardTrackableEventHandler\" script to all");
					EditorGUILayout.LabelField ("your Vuforia Targets");

					if (EnableHybridTrackingProp.boolValue)
						AddVuforiaScript (go);
					else
						RemoveVuforiaScript (go);

					Repaint ();
				} else {
					RemoveVuforiaScript (go);
				}
			}

			EditorGUILayout.Space ();

			if (!UseThirdPartySDKProp.boolValue) {
				EditorGUILayout.PropertyField (recenterPoint, new GUIContent ("Recenter Point", "The Transform to which the Camera will face when Recenter() is called"));
				EditorGUILayout.PropertyField (UseHeadTrackingProp, new GUIContent ("Use Head Tracking", "Variable to keep track of head tracking"));
				RemoveVuforiaScript (go);
			}

			foldout_Cameras = EditorGUILayout.Foldout (foldout_Cameras, new GUIContent ("Use Custom Stereo Camera Setup", "Attach custom Left And Right Camera (optional)"));

			if (foldout_Cameras) {
				EditorGUILayout.PropertyField (LeftCamProp, new GUIContent ("Left Camera"));
				EditorGUILayout.PropertyField (LeftCamProp, new GUIContent ("Right Camera"));
			}

			EditorGUILayout.PropertyField (eyeSeparationProp, new GUIContent ("Eye Separation (m)", "Eye Separation or IPD"));

			serializedObject.ApplyModifiedProperties ();
		}

		private void AddVuforiaScript (ARCamera go)
		{
			VuforiaTrackablesTracker node = go.GetComponent<VuforiaTrackablesTracker> ();

			if (node == null) {
				go.gameObject.AddComponent<VuforiaTrackablesTracker> ();
			}
		}

		private void RemoveVuforiaScript (ARCamera go)
		{
			VuforiaTrackablesTracker node = go.GetComponent<VuforiaTrackablesTracker> ();

			if (node != null) {
				DestroyImmediate (node);
			}
		}
	}
}