using UnityEngine;
//using Vuforia; //Uncomment after Adding Vuforia

namespace TesseractBasic {

	namespace ThirdParty {
		//Comment after adding Vuforia
		public class VuforiaTrackablesTracker : MonoBehaviour {

		}

		/* Uncomment after adding Vuforia
		 * 
		 * 
		/// <summary>
		/// Class for keeping track of the states of each <see cref=" Vuforia.TrackableBehaviour"/> and its tracking state
		/// </summary>
		public class VuforiaTrackablesTracker : MonoBehaviour {

			/// <summary>
			/// Singleton Instance of VuforiaInterpretor
			/// </summary>
			public static VuforiaTrackablesTracker Instance = null;

			/// <summary>
			/// List of Trackables
			/// </summary>
			private List<TrackableAndState> mTrackableAndState = new List<TrackableAndState> ();

			/// <summary>
			/// Instance of AR Camera.
			/// </summary>
			private ARCamera mARCamera;

			/// <summary>
			/// Variable to check whether the camera pose is valid and whether it has been tracked at least once
			/// </summary>
			private bool mTrackedOnce = false;

			/// <summary>
			/// All Trackables are lost.
			/// </summary>
			private bool mAllLost = false;

			private void Awake ()
			{
				//Singeton Instance
				Instance = this;
			}

			private void Start ()
			{
				mARCamera = ARCamera.Instance;
			}

			/// <summary>
			/// Adds the trackable to the list of all trackables
			/// </summary>
			/// <param name="trackable">Trackable.</param>
			public void AddTrackable (TrackableBehaviour trackable)
			{
				mTrackableAndState.Add (new TrackableAndState (trackable, false));
			}

			/// <summary>
			/// Updates the state of the trackable.
			/// </summary>
			/// <param name="trackable">Trackable, who's state is being updated</param>
			/// <param name="state">If set to <c>true</c> state.</param>
			public void UpdateTrackableState (TrackableBehaviour trackable, bool state)
			{
				if (state) {
					mTrackedOnce = true;

					if (mAllLost) {
						mARCamera.EnableThirdPartyPose ();
					}

					mAllLost = false;
				}

				TrackableAndState trackableAndState = mTrackableAndState.Find ((TrackableAndState obj) => (obj.mTrackable == trackable));
				trackableAndState.mActive = state;

				CheckIfAllLost ();
			}

			/// <summary>
			/// Checks if all tracking are lost
			/// </summary>
			private void CheckIfAllLost ()
			{
				if (mTrackedOnce) {
					int trackableslost = 0;
					foreach (TrackableAndState trackableAndState in mTrackableAndState) {
						if (!trackableAndState.mActive)
							trackableslost++;
					}

					if (trackableslost == mTrackableAndState.Count) {
						Debug.LogError ("ALL LOST");
						if(!mAllLost)
							mARCamera.DisableThirdPartyPose ();
						mAllLost = true;
					}
				}
			}
		}

		/// <summary>
		/// Class for storing <see cref=" Vuforia.TrackableBehaviour"/> and corresponding tracking state as a boolean
		/// </summary>
		class TrackableAndState {
			public TrackableBehaviour mTrackable;
			public bool mActive;

			public TrackableAndState(TrackableBehaviour trackable, bool active){
				mActive = active;
				mTrackable = trackable;
			}
		} 
		*/
	}
}