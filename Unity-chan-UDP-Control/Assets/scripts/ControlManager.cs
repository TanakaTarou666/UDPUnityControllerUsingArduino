using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    private IControlScheme currentControlScheme;
    private KeyboardControl keyboardControl;
    private UDPControl udpControl;
    

    [SerializeField]
    private TextAsset configFile; // TextAssetとしてJSONファイルを指宁E
    private CustomCharacterController characterController;

    public Camera firstCamera;
    public Camera secondCamera;

    void Start()
    {
        ControlConfig config = ControlConfigLoader.LoadConfig(configFile);

        keyboardControl = new KeyboardControl();
        udpControl = new UDPControl(config.udpSettings.ipAddress, config.udpSettings.port);

        // キャラクターコントローラーにmovementDataを設宁E        characterController = GetComponent<CustomCharacterController>();
        if (characterController != null)
        {
            characterController.InitMovementData(config.movementData);
            keyboardControl.OnSetMove += characterController.SetMove;
            keyboardControl.OnSetRotate += characterController.SetRotate;
            udpControl.OnSetMove += characterController.SetMove;
            udpControl.OnSetRotate += characterController.SetRotate;

            Debug.Log("Delegates assigned");
        }
        else
        {
            Debug.LogError("CustomCharacterController not found on GameObject");
        }

        // 初期制御方法を設定ファイルに基づぁE��設宁E        switch (config.controlType.ToLower())
        {
            case "keyboard":
                currentControlScheme = keyboardControl;
                Debug.Log("input mode : Keyboard");
                break;
            case "udp":
                currentControlScheme = udpControl;
                Debug.Log("input mode : UDP");
                break;
            default:
                Debug.LogError("Unknown control type in config");
                break;
        }

        // 最初�Eカメラを有効にする
        firstCamera.gameObject.SetActive(true);
        // 二つ目のカメラを無効にする
        secondCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        // 現在の制御方法を更新
        currentControlScheme.UpdateControl();

        // 任意�Eキーで制御方法を刁E��替ぁE        if (Input.GetKeyDown(KeyCode.U))
        {
            if (characterController.IsUDPControlActive)
            {
                currentControlScheme = keyboardControl;
                characterController.SetMove(0.0f);
                Debug.Log("input mode : Keyboard");
            }
            else
            {
                currentControlScheme = udpControl;
                characterController.SetMove(1.0f);

                Debug.Log("input mode : UDP");
            }
            characterController.IsUDPControlActive = !characterController.IsUDPControlActive;
                               
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            firstCamera.gameObject.SetActive(!firstCamera.gameObject.activeSelf);
            secondCamera.gameObject.SetActive(!secondCamera.gameObject.activeSelf);
        }
    }
}
