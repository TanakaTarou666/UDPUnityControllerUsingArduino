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
        // 繧ｭ繝ｼ繝懊・繝牙・蜉帛・逅・        float move = Input.GetAxis("Vertical"); // 荳贋ｸ狗ｧｻ蜍・        float rotate = Input.GetAxis("Horizontal"); // 蟾ｦ蜿ｳ蝗櫁ｻ｢

        // 繝励Ξ繧､繝､繝ｼ繧・が繝悶ず繧ｧ繧ｯ繝医・蛻ｶ蠕｡繧ｳ繝ｼ繝・        OnSetMove?.Invoke(move);
        OnSetRotate?.Invoke(rotate);
    }
}
