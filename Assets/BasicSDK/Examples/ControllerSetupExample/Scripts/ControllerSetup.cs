using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TesseractBasic;
using UnityEngine.Events;

/// <summary>
/// Example Class to show setting up process of the controller along with UI. Checkout the Controller Setup example scene
/// </summary>
public class ControllerSetup : MonoBehaviour {
	#region UIElements
	public GameObject Button, TroubleShootScreen, PairRequest, LoadingCircle, Button1, Button2;
	public Text ButtonText, InstructionsText;
	public Image LEDImage, Controller;
	public Sprite Controller_HL;
#endregion

	/// <summary>
	/// This event is triggered when the application successfully pairs with the Holoboard Controller and ready to use.
	/// </summary>
	public UnityEvent OnConnected;

	#region PrivateVariables
	private CONTROLLER_STATE controllerState = CONTROLLER_STATE.UNKNOWN;
	private bool Blinking = false, TSBlinking = false;
	private HoloboardController mHoloboardController;
#endregion

	// Use this for initialization
	void Start ()
	{
		mHoloboardController = HoloboardController.Instance;
		TroubleShootScreen.SetActive (false);
		LoadingCircle.SetActive (false);
		PairRequest.SetActive (false);
		LEDImage.enabled = false;
		Button1.SetActive (false);
		ControllerCallbacks.OnControllerStateChanged+=ControllerCallbacks_OnControllerStateChanged;
	}

	/// <summary>
	/// This function is invoked whenever you press the On Screen button on the bottom right. Based on the state of the connection with
	/// the controller, the function will take the relevant step as seen below in the code.
	/// </summary>
	public void ButtonClick(){
		if (HoloboardController.Instance.ControllerState != CONTROLLER_STATE.SUBSCRIBED)
			Connect ();
		else{
			if (OnConnected != null)
				OnConnected.Invoke ();
		}
	}

	/// <summary>
	/// This function issues the InitiateConnection command to the holoboard controller and sets up the UI to recieve state updates
	/// </summary>
	public void Connect ()
	{
		mHoloboardController.InitiateConnection ();
		Button1.SetActive (false);
		Button2.SetActive (false);
		Button.SetActive (false);
		LoadingCircle.SetActive (true);
		InstructionsText.text = "Connecting..";
	}

	/// <summary>
	/// Callback for controller state changes
	/// </summary>
	/// <param name="obj">New Controller state</param>
	void ControllerCallbacks_OnControllerStateChanged (CONTROLLER_STATE obj)
	{
		switch (obj) {
		case CONTROLLER_STATE.DEVICE_NOT_FOUND:
				TroubleShootScreen.SetActive (false);
				PairRequest.SetActive (false);
				LoadingCircle.SetActive (true);
				InstructionsText.text = "Connecting.. \n\nPlease Ensure The Blue Light Is Blinking On The Connector \n\nPlease Press The Controller Button Once Again";
				StartCoroutine (BlinkLED ());
				Button2.SetActive (true);
				Button.SetActive (false);
				break;
		case CONTROLLER_STATE.FOUND_CONTROLLER:
				TroubleShootScreen.SetActive (false);
				PairRequest.SetActive (false);
				LoadingCircle.SetActive (true);
				Blinking = false;
				Button2.SetActive (false);
				Button1.SetActive (false);
				Button.SetActive (false);
				InstructionsText.text = "Connecting.. \n\nController Found!";
				break;
		case CONTROLLER_STATE.RECONNECTING:
				TroubleShootScreen.SetActive (false);
				PairRequest.SetActive (false);
				LoadingCircle.SetActive (true);
				StartCoroutine (BlinkLED ());
				Button2.SetActive (true);
				Button.SetActive (false);
				InstructionsText.text = "Connecting.. \n\nPlease Ensure The Blue Light Is Blinking On The Connector \n\nPlease Press The Controller Button Once Again";
				break;
		case CONTROLLER_STATE.TROUBLESHOOT:
				TroubleShootScreen.SetActive (true);
				PairRequest.SetActive (false);
				LoadingCircle.SetActive (false);
				InstructionsText.text = "";
				StartCoroutine (TroubleShootBlink ());
				Blinking = false;
				Button2.SetActive (true);
				Button1.SetActive (true);
				Button.SetActive (false);
				break;
		case CONTROLLER_STATE.BLE_INACTIVE:
				Blinking = false;
			if (Application.platform == RuntimePlatform.IPhonePlayer)
				InstructionsText.text = "Please ensure Bluetooth is enabled in the Control Center \n\nPlease Press The Controller Button Once Again";
			else if(Application.platform == RuntimePlatform.Android)
				InstructionsText.text = "Please ensure Bluetooth is enabled \n\nPress Connect Once Again";
			else
				InstructionsText.text = "Please ensure Bluetooth is enabled \n\nPress Connect Once Again";

			if (Application.platform != RuntimePlatform.IPhonePlayer) {
				Button.SetActive (true);
				ButtonText.text = "Connect";
			}

			PairRequest.SetActive (false);
			Button2.SetActive (true);
			Button1.SetActive (false);
			LoadingCircle.SetActive (false);
			break;
		case CONTROLLER_STATE.PAIR_REQUEST:
			TroubleShootScreen.SetActive (false);
			PairRequest.SetActive (true);
			LoadingCircle.SetActive (true);
			StartCoroutine (BlinkLED ());
			Button2.SetActive (true);
			Button.SetActive (false);
			InstructionsText.text = "";
			break;
		case CONTROLLER_STATE.SUBSCRIBED:
			Controller.sprite = Controller_HL;
			Blinking = false;
			TSBlinking = false;
			TroubleShootScreen.SetActive (false);
			PairRequest.SetActive (false);
			LoadingCircle.SetActive (false);
			Button2.SetActive (false);
			Button1.SetActive (false);
			InstructionsText.text = "Controller Connected! \n\nPress Ready To Begin";
			ButtonText.text = "Ready";
			Button.SetActive (true);
			break;
		}
	}

	private IEnumerator BlinkLED(){
		Blinking = true;
		TSBlinking = false;
		WaitForSeconds onTime = new WaitForSeconds (0.2f);
		WaitForSeconds offTime = new WaitForSeconds (0.7f);
		while(Blinking){
			LEDImage.enabled = true;
			yield return onTime;
			if (!Blinking) {
				LEDImage.enabled = false;
				yield break;
			}
			LEDImage.enabled = false;
			yield return offTime;
		}
	}

	private IEnumerator TroubleShootBlink(){
		TSBlinking = true;
		Blinking = false;
		WaitForSeconds onTime = new WaitForSeconds (0.2f);
		WaitForSeconds offTime = new WaitForSeconds (0.4f);
		WaitForSeconds longPauseTime = new WaitForSeconds (1);
		while (TSBlinking) {
			for (int i = 0; i < 3; i++) {
				if (!TSBlinking){
					LEDImage.enabled = false;
					yield break;
				}
				LEDImage.enabled = true;
				yield return onTime;
				if (!TSBlinking) {
					LEDImage.enabled = false;
					yield break;
				}
				LEDImage.enabled = false;
				yield return offTime;

				if (!TSBlinking) {
					LEDImage.enabled = false;
					yield break;
				}
			}
			yield return longPauseTime;
		}
	}

	private void OnDestroy ()
	{
		ControllerCallbacks.OnControllerStateChanged -= ControllerCallbacks_OnControllerStateChanged;
	}
}