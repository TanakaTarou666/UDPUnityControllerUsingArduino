//
//RandomWind.cs for unity-chan!
//
//Original Script is here:
//ricopin / RandomWind.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
//�C��2014/12/20
//���̕����ω�/�d�͉e����ǉ�.
//

using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class RandomWind : MonoBehaviour
	{
		private SpringBone[] springBones;
		public bool isWindActive = false;

		private bool isMinus = false;				//���������]�p.
		public float threshold = 0.5f;				// �����_�������臒l.
		public float interval = 5.0f;				// �����_������̃C���^�[�o��.
		public float windPower = 1.0f;				//���̋���.
		public float gravity = 0.98f;				//�d�͂̋���.
        public bool isGUI = true;


		// Use this for initialization
		void Start ()
		{
			springBones = GetComponent<SpringManager> ().springBones;
			StartCoroutine ("RandomChange");
		}





		// Update is called once per frame
		void Update ()
		{

			Vector3 force = Vector3.zero;
			if (isWindActive) {
				if(isMinus){
					force = new Vector3 (Mathf.PerlinNoise (Time.time, 0.0f) * windPower * -0.001f , gravity * -0.001f , 0);
				}else{
					force = new Vector3 (Mathf.PerlinNoise (Time.time, 0.0f) * windPower * 0.001f, gravity * -0.001f, 0);
				}

				for (int i = 0; i < springBones.Length; i++) {
					springBones [i].springForce = force;
				}
			
			}
		}

		void OnGUI ()
		{
            if (isGUI)
            {
                Rect rect1 = new Rect(10, Screen.height - 40, 400, 30);
                isWindActive = GUI.Toggle(rect1, isWindActive, "Random Wind");
            }
		}

		// �����_������p�֐�.
		IEnumerator RandomChange ()
		{
			// �������[�v�J�n.
			while (true) {
				//�����_������p�V�[�h����.
				float _seed = Random.Range (0.0f, 1.0f);

				if (_seed > threshold) {
					//_seed��threshold�ȏ�̎��A�����𔽓]����.
					isMinus = true;
				}else{
					isMinus = false;
				}

				// ���̔���܂ŃC���^�[�o����u��.
				yield return new WaitForSeconds (interval);
			}
		}


	}
}