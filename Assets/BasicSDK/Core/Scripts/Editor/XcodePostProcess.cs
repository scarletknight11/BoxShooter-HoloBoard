#if UNITY_IOS
using UnityEngine;
using UnityEditor.iOS.Xcode;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class XcodePostProcess : MonoBehaviour {

	[PostProcessBuild]
	public static void OnPostprocessBuild (BuildTarget buildTarget, string path)
	{
		if (buildTarget == BuildTarget.iOS) {
			string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

			PBXProject proj = new PBXProject ();
			proj.ReadFromString (File.ReadAllText (projPath));

			string target = proj.TargetGuidByName ("Unity-iPhone");
			proj.SetBuildProperty (target, "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
			proj.SetBuildProperty (target, "SWIFT_VERSION", "4.0");
			proj.SetBuildProperty (target, "ENABLE_BITCODE", "NO");
			File.WriteAllText (projPath, proj.WriteToString ());
		}
	}
}
#endif