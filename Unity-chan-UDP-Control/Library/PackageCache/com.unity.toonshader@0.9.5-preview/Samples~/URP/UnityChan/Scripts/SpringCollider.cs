//
//SpringCollider for unity-chan!
//
//Original Script is here:
//ricopin / SpringCollider.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.Universal.Samples
{
	public class SpringCollider : MonoBehaviour
	{
		//���a
		public float radius = 0.5f;

		private void OnDrawGizmosSelected ()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere (transform.position, radius);
		}
	}
}