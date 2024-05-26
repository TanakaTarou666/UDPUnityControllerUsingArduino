Shader "Unlit/FaceOrientation"
{
    Properties
    {
        _ColorFront ("Front Color", Color) = (1,0.7,0.7,1)
        _ColorBack ("Back Color", Color) = (0.7,1,0.7,1)
    }
    SubShader
    {
        Pass
        {
            Cull Off // č£åććEć«ćŖć³ć°ććŖćć«ćć¾ćE
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }

            fixed4 _ColorFront;
            fixed4 _ColorBack;

            fixed4 frag (fixed facing : VFACE) : SV_Target
            {
                // VFACE å„åćEę­£é¢åćć§ćÆč² ć®å¤ćE                // č£åćć§ćÆč² ć®å¤ć§ćććć®å¤ć«ćć£ć¦ 
                // 2 č²ć®ćE”ć® 1 ć¤ćåEåćć¾ććE                return facing > 0 ? _ColorFront : _ColorBack;
            }
            ENDCG
        }
    }
}