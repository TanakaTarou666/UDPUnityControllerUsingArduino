//
//AutoBlink.cs
//�I�[�g�ڃp�`�X�N���v�g
//2014/06/23 N.Kobayashi
//
using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.HDRP.Samples
{
	public class AutoBlink : MonoBehaviour
	{

		public bool isActive = true;				//�I�[�g�ڃp�`�L��
		public SkinnedMeshRenderer ref_SMR_EYE_DEF;	//EYE_DEF�ւ̎Q��
		public SkinnedMeshRenderer ref_SMR_EL_DEF;	//EL_DEF�ւ̎Q��
		public float ratio_Close = 85.0f;			//���ڃu�����h�V�F�C�v�䗦
		public float ratio_HalfClose = 20.0f;		//�����ڃu�����h�V�F�C�v�䗦
		[HideInInspector]
		public float
			ratio_Open = 0.0f;
		private bool timerStarted = false;			//�^�C�}�[�X�^�[�g�Ǘ��p
		private bool isBlink = false;				//�ڃp�`�Ǘ��p

		public float timeBlink = 0.4f;				//�ڃp�`�̎���
		private float timeRemining = 0.0f;			//�^�C�}�[�c�莞��

		public float threshold = 0.3f;				// �����_�������臒l
		public float interval = 3.0f;				// �����_������̃C���^�[�o��



		enum Status
		{
			Close,
			HalfClose,
			Open	//�ڃp�`�̏��
		}


		private Status eyeStatus;	//���݂̖ڃp�`�X�e�[�^�X

		void Awake ()
		{
			//ref_SMR_EYE_DEF = GameObject.Find("EYE_DEF").GetComponent<SkinnedMeshRenderer>();
			//ref_SMR_EL_DEF = GameObject.Find("EL_DEF").GetComponent<SkinnedMeshRenderer>();
		}



		// Use this for initialization
		void Start ()
		{
			ResetTimer ();
			// �����_������p�֐����X�^�[�g����
			StartCoroutine ("RandomChange");
		}

		//�^�C�}�[���Z�b�g
		void ResetTimer ()
		{
			timeRemining = timeBlink;
			timerStarted = false;
		}

		// Update is called once per frame
		void Update ()
		{
			if (!timerStarted) {
				eyeStatus = Status.Close;
				timerStarted = true;
			}
			if (timerStarted) {
				timeRemining -= Time.deltaTime;
				if (timeRemining <= 0.0f) {
					eyeStatus = Status.Open;
					ResetTimer ();
				} else if (timeRemining <= timeBlink * 0.3f) {
					eyeStatus = Status.HalfClose;
				}
			}
		}

		void LateUpdate ()
		{
			if (isActive) {
				if (isBlink) {
					switch (eyeStatus) {
					case Status.Close:
						SetCloseEyes ();
						break;
					case Status.HalfClose:
						SetHalfCloseEyes ();
						break;
					case Status.Open:
						SetOpenEyes ();
						isBlink = false;
						break;
					}
					//Debug.Log(eyeStatus);
				}
			}
		}

		void SetCloseEyes ()
		{
			ref_SMR_EYE_DEF.SetBlendShapeWeight (6, ratio_Close);
			ref_SMR_EL_DEF.SetBlendShapeWeight (6, ratio_Close);
		}

		void SetHalfCloseEyes ()
		{
			ref_SMR_EYE_DEF.SetBlendShapeWeight (6, ratio_HalfClose);
			ref_SMR_EL_DEF.SetBlendShapeWeight (6, ratio_HalfClose);
		}

		void SetOpenEyes ()
		{
			ref_SMR_EYE_DEF.SetBlendShapeWeight (6, ratio_Open);
			ref_SMR_EL_DEF.SetBlendShapeWeight (6, ratio_Open);
		}
		
		// �����_������p�֐�
		IEnumerator RandomChange ()
		{
			// �������[�v�J�n
			while (true) {
				//�����_������p�V�[�h����
				float _seed = Random.Range (0.0f, 1.0f);
				if (!isBlink) {
					if (_seed > threshold) {
						isBlink = true;
					}
				}
				// ���̔���܂ŃC���^�[�o����u��
				yield return new WaitForSeconds (interval);
			}
		}
	}
}