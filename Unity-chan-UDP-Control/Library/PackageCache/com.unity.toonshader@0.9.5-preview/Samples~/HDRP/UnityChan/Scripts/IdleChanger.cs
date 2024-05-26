using UnityEngine;
using System.Collections;

namespace UnityEngine.Rendering.Toon.HDRP.Samples
{
//
// �����L�[�Ń��[�v�A�j���[�V������؂�ւ���X�N���v�g�i�����_���؂�ւ��t���jVer.3
// 2014/04/03 N.Kobayashi
//

// Require these components when using this script
	[RequireComponent(typeof(Animator))]



	public class IdleChanger : MonoBehaviour
	{
	
		private Animator anim;						// Animator�ւ̎Q��
		private AnimatorStateInfo currentState;		// ���݂̃X�e�[�g��Ԃ�ۑ�����Q��
		private AnimatorStateInfo previousState;	// �ЂƂO�̃X�e�[�g��Ԃ�ۑ�����Q��
		public bool _random = false;				// �����_������X�^�[�g�X�C�b�`
		public float _threshold = 0.5f;				// �����_�������臒l
		public float _interval = 10f;               // �����_������̃C���^�[�o��
        //private float _seed = 0.0f;					// �����_������p�V�[�h
        public bool isGUI = true;
	


		// Use this for initialization
		void Start ()
		{
			// �e�Q�Ƃ̏�����
			anim = GetComponent<Animator> ();
			currentState = anim.GetCurrentAnimatorStateInfo (0);
			previousState = currentState;
			// �����_������p�֐����X�^�[�g����
			StartCoroutine ("RandomChange");
		}
	
		// Update is called once per frame
		void  Update ()
		{
			// ���L�[/�X�y�[�X�������ꂽ��A�X�e�[�g�����ɑ��鏈��
			if (Input.GetKeyDown ("up") || Input.GetButton ("Jump")) {
				// �u�[���A��Next��true�ɂ���
				anim.SetBool ("Next", true);
			}
		
			// ���L�[�������ꂽ��A�X�e�[�g��O�ɖ߂�����
			if (Input.GetKeyDown ("down")) {
				// �u�[���A��Back��true�ɂ���
				anim.SetBool ("Back", true);
			}
		
			// "Next"�t���O��true�̎��̏���
			if (anim.GetBool ("Next")) {
				// ���݂̃X�e�[�g���`�F�b�N���A�X�e�[�g��������Ă�����u�[���A����false�ɖ߂�
				currentState = anim.GetCurrentAnimatorStateInfo (0);
				if (previousState.fullPathHash != currentState.fullPathHash) {
					anim.SetBool ("Next", false);
					previousState = currentState;				
				}
			}
		
			// "Back"�t���O��true�̎��̏���
			if (anim.GetBool ("Back")) {
				// ���݂̃X�e�[�g���`�F�b�N���A�X�e�[�g��������Ă�����u�[���A����false�ɖ߂�
				currentState = anim.GetCurrentAnimatorStateInfo (0);
				if (previousState.fullPathHash != currentState.fullPathHash) {
					anim.SetBool ("Back", false);
					previousState = currentState;
				}
			}
		}

		void OnGUI ()
		{
            if (isGUI)
            {
                GUI.Box(new Rect(Screen.width - 110, 10, 100, 90), "Change Motion");
                if (GUI.Button(new Rect(Screen.width - 100, 40, 80, 20), "Next"))
                    anim.SetBool("Next", true);
                if (GUI.Button(new Rect(Screen.width - 100, 70, 80, 20), "Back"))
                    anim.SetBool("Back", true);
            }
		}


		// �����_������p�֐�
		IEnumerator RandomChange ()
		{
			// �������[�v�J�n
			while (true) {
				//�����_������X�C�b�`�I���̏ꍇ
				if (_random) {
					// �����_���V�[�h�����o���A���̑傫���ɂ���ăt���O�ݒ������
					float _seed = Random.Range (0.0f, 1.0f);
					if (_seed < _threshold) {
						anim.SetBool ("Back", true);
					} else if (_seed >= _threshold) {
						anim.SetBool ("Next", true);
					}
				}
				// ���̔���܂ŃC���^�[�o����u��
				yield return new WaitForSeconds (_interval);
			}

		}

	}
}
