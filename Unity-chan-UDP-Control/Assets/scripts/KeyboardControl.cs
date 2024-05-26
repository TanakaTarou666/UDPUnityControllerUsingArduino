using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : IControlScheme
{
    public delegate void MoveDelegate(float move);
    public delegate void RotateDelegate(float rotate);

    public MoveDelegate OnSetMove;
    public RotateDelegate OnSetRotate;

    public void UpdateControl()
    {
        // キーボ�Eド�E力�E琁E        float move = Input.GetAxis("Vertical"); // 上下移勁E        float rotate = Input.GetAxis("Horizontal"); // 左右回転

        // プレイヤーめE��ブジェクト�E制御コーチE        OnSetMove?.Invoke(move);
        OnSetRotate?.Invoke(rotate);
    }
}
