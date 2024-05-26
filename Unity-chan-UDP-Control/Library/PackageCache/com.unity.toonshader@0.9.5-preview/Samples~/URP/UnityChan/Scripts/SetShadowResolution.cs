//
// SetShadowResolution.cs
// シャドウマップにカスタムレゾリューションを設定するスクリプト.
// メインライトにアタチE��すること. 
//
using UnityEngine;

namespace UnityEngine.Rendering.Toon.Universal.Samples
{
	public class SetShadowResolution : MonoBehaviour {
		public int resolution;
		
		void Update () {
				GetComponent<Light>().shadowCustomResolution = resolution;
		}
	}
}