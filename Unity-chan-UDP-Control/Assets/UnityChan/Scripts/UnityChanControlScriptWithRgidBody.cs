//
// Mecanim�̃A�j���[�V�����f�[�^���A���_�ňړ����Ȃ��ꍇ�� Rigidbody�t���R���g���[��
// �T���v��
// 2014/03/13 N.Kobyasahi
// 2015/03/11 Revised for Unity5 (only)
//
using UnityEngine;
using System.Collections;

namespace UnityChan
{
// �K�v�ȃR���|�[�l���g�̗�L
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]

	public class UnityChanControlScriptWithRgidBody : MonoBehaviour
	{

		public float animSpeed = 1.5f;				// �A�j���[�V�����Đ����x�ݒ�
		public float lookSmoother = 3.0f;			// a smoothing setting for camera motion
		public bool useCurves = true;				// Mecanim�ŃJ�[�u�������g�����ݒ肷��
		// ���̃X�C�b�`�������Ă��Ȃ��ƃJ�[�u�͎g���Ȃ�
		public float useCurvesHeight = 0.5f;		// �J�[�u�␳�̗L�������i�n�ʂ����蔲���₷�����ɂ͑傫������j

		// �ȉ��L�����N�^�[�R���g���[���p�p�����^
		// �O�i���x
		public float forwardSpeed = 7.0f;
		// ��ޑ��x
		public float backwardSpeed = 2.0f;
		// ���񑬓x
		public float rotateSpeed = 2.0f;
		// �W�����v�З�
		public float jumpPower = 3.0f; 
		// �L�����N�^�[�R���g���[���i�J�v�Z���R���C�_�j�̎Q��
		private CapsuleCollider col;
		private Rigidbody rb;
		// �L�����N�^�[�R���g���[���i�J�v�Z���R���C�_�j�̈ړ���
		private Vector3 velocity;
		// CapsuleCollider�Őݒ肳��Ă���R���C�_��Heiht�ACenter�̏����l�����߂�ϐ�
		private float orgColHight;
		private Vector3 orgVectColCenter;
		private Animator anim;							// �L�����ɃA�^�b�`�����A�j���[�^�[�ւ̎Q��
		private AnimatorStateInfo currentBaseState;			// base layer�Ŏg����A�A�j���[�^�[�̌��݂̏�Ԃ̎Q��

		private GameObject cameraObject;	// ���C���J�����ւ̎Q��
		
		// �A�j���[�^�[�e�X�e�[�g�ւ̎Q��
		static int idleState = Animator.StringToHash ("Base Layer.Idle");
		static int locoState = Animator.StringToHash ("Base Layer.Locomotion");
		static int jumpState = Animator.StringToHash ("Base Layer.Jump");
		static int restState = Animator.StringToHash ("Base Layer.Rest");

		// ������
		void Start ()
		{
			// Animator�R���|�[�l���g���擾����
			anim = GetComponent<Animator> ();
			// CapsuleCollider�R���|�[�l���g���擾����i�J�v�Z���^�R���W�����j
			col = GetComponent<CapsuleCollider> ();
			rb = GetComponent<Rigidbody> ();
			//���C���J�������擾����
			cameraObject = GameObject.FindWithTag ("MainCamera");
			// CapsuleCollider�R���|�[�l���g��Height�ACenter�̏����l��ۑ�����
			orgColHight = col.height;
			orgVectColCenter = col.center;
		}
	
	
		// �ȉ��A���C������.���W�b�h�{�f�B�Ɨ��߂�̂ŁAFixedUpdate���ŏ������s��.
		void FixedUpdate ()
		{
			float h = Input.GetAxis ("Horizontal");				// ���̓f�o�C�X�̐�������h�Œ�`
			float v = Input.GetAxis ("Vertical");				// ���̓f�o�C�X�̐�������v�Œ�`
			anim.SetFloat ("Speed", v);							// Animator���Őݒ肵�Ă���"Speed"�p�����^��v��n��
			anim.SetFloat ("Direction", h); 						// Animator���Őݒ肵�Ă���"Direction"�p�����^��h��n��
			anim.speed = animSpeed;								// Animator�̃��[�V�����Đ����x�� animSpeed��ݒ肷��
			currentBaseState = anim.GetCurrentAnimatorStateInfo (0);	// �Q�Ɨp�̃X�e�[�g�ϐ���Base Layer (0)�̌��݂̃X�e�[�g��ݒ肷��
			rb.useGravity = true;//�W�����v���ɏd�͂�؂�̂ŁA����ȊO�͏d�͂̉e�����󂯂�悤�ɂ���
		
		
		
			// �ȉ��A�L�����N�^�[�̈ړ�����
			velocity = new Vector3 (0, 0, v);		// �㉺�̃L�[���͂���Z�������̈ړ��ʂ��擾
			// �L�����N�^�[�̃��[�J����Ԃł̕����ɕϊ�
			velocity = transform.TransformDirection (velocity);
			//�ȉ���v��臒l�́AMecanim���̃g�����W�V�����ƈꏏ�ɒ�������
			if (v > 0.1) {
				velocity *= forwardSpeed;		// �ړ����x���|����
			} else if (v < -0.1) {
				velocity *= backwardSpeed;	// �ړ����x���|����
			}
		
			if (Input.GetButtonDown ("Jump")) {	// �X�y�[�X�L�[����͂�����

				//�A�j���[�V�����̃X�e�[�g��Locomotion�̍Œ��̂݃W�����v�ł���
				if (currentBaseState.fullPathHash == locoState) {
					//�X�e�[�g�J�ڒ��łȂ�������W�����v�ł���
					if (!anim.IsInTransition (0)) {
						rb.AddForce (Vector3.up * jumpPower, ForceMode.VelocityChange);
						anim.SetBool ("Jump", true);		// Animator�ɃW�����v�ɐ؂�ւ���t���O�𑗂�
					}
				}
			}
		

			// �㉺�̃L�[���͂ŃL�����N�^�[���ړ�������
			transform.localPosition += velocity * Time.fixedDeltaTime;

			// ���E�̃L�[���͂ŃL�����N�^��Y���Ő��񂳂���
			transform.Rotate (0, h * rotateSpeed, 0);	
	

			// �ȉ��AAnimator�̊e�X�e�[�g���ł̏���
			// Locomotion��
			// ���݂̃x�[�X���C���[��locoState�̎�
			if (currentBaseState.fullPathHash == locoState) {
				//�J�[�u�ŃR���C�_���������Ă��鎞�́A�O�̂��߂Ƀ��Z�b�g����
				if (useCurves) {
					resetCollider ();
				}
			}
		// JUMP���̏���
		// ���݂̃x�[�X���C���[��jumpState�̎�
		else if (currentBaseState.fullPathHash == jumpState) {
				cameraObject.SendMessage ("setCameraPositionJumpView");	// �W�����v���̃J�����ɕύX
				// �X�e�[�g���g�����W�V�������łȂ��ꍇ
				if (!anim.IsInTransition (0)) {
				
					// �ȉ��A�J�[�u����������ꍇ�̏���
					if (useCurves) {
						// �ȉ�JUMP00�A�j���[�V�����ɂ��Ă���J�[�uJumpHeight��GravityControl
						// JumpHeight:JUMP00�ł̃W�����v�̍����i0?1�j
						// GravityControl:1�˃W�����v���i�d�͖����j�A0�ˏd�͗L��
						float jumpHeight = anim.GetFloat ("JumpHeight");
						float gravityControl = anim.GetFloat ("GravityControl"); 
						if (gravityControl > 0)
							rb.useGravity = false;	//�W�����v���̏d�͂̉e����؂�
										
						// ���C�L���X�g���L�����N�^�[�̃Z���^�[���痎�Ƃ�
						Ray ray = new Ray (transform.position + Vector3.up, -Vector3.up);
						RaycastHit hitInfo = new RaycastHit ();
						// ������ useCurvesHeight �ȏ゠�鎞�̂݁A�R���C�_�[�̍����ƒ��S��JUMP00�A�j���[�V�����ɂ��Ă���J�[�u�Œ�������
						if (Physics.Raycast (ray, out hitInfo)) {
							if (hitInfo.distance > useCurvesHeight) {
								col.height = orgColHight - jumpHeight;			// �������ꂽ�R���C�_�[�̍���
								float adjCenterY = orgVectColCenter.y + jumpHeight;
								col.center = new Vector3 (0, adjCenterY, 0);	// �������ꂽ�R���C�_�[�̃Z���^�[
							} else {
								// 臒l�����Ⴂ���ɂ͏����l�ɖ߂��i�O�̂��߁j					
								resetCollider ();
							}
						}
					}
					// Jump bool�l�����Z�b�g����i���[�v���Ȃ��悤�ɂ���j				
					anim.SetBool ("Jump", false);
				}
			}
		// IDLE���̏���
		// ���݂̃x�[�X���C���[��idleState�̎�
		else if (currentBaseState.fullPathHash == idleState) {
				//�J�[�u�ŃR���C�_���������Ă��鎞�́A�O�̂��߂Ƀ��Z�b�g����
				if (useCurves) {
					resetCollider ();
				}
				// �X�y�[�X�L�[����͂�����Rest��ԂɂȂ�
				if (Input.GetButtonDown ("Jump")) {
					anim.SetBool ("Rest", true);
				}
			}
		// REST���̏���
		// ���݂̃x�[�X���C���[��restState�̎�
		else if (currentBaseState.fullPathHash == restState) {
				//cameraObject.SendMessage("setCameraPositionFrontView");		// �J�����𐳖ʂɐ؂�ւ���
				// �X�e�[�g���J�ڒ��łȂ��ꍇ�ARest bool�l�����Z�b�g����i���[�v���Ȃ��悤�ɂ���j
				if (!anim.IsInTransition (0)) {
					anim.SetBool ("Rest", false);
				}
			}
		}

		void OnGUI ()
		{
			GUI.Box (new Rect (Screen.width - 260, 10, 250, 150), "Interaction");
			GUI.Label (new Rect (Screen.width - 245, 30, 250, 30), "Up/Down Arrow : Go Forwald/Go Back");
			GUI.Label (new Rect (Screen.width - 245, 50, 250, 30), "Left/Right Arrow : Turn Left/Turn Right");
			GUI.Label (new Rect (Screen.width - 245, 70, 250, 30), "Hit Space key while Running : Jump");
			GUI.Label (new Rect (Screen.width - 245, 90, 250, 30), "Hit Spase key while Stopping : Rest");
			GUI.Label (new Rect (Screen.width - 245, 110, 250, 30), "Left Control : Front Camera");
			GUI.Label (new Rect (Screen.width - 245, 130, 250, 30), "Alt : LookAt Camera");
		}


		// �L�����N�^�[�̃R���C�_�[�T�C�Y�̃��Z�b�g�֐�
		void resetCollider ()
		{
			// �R���|�[�l���g��Height�ACenter�̏����l��߂�
			col.height = orgColHight;
			col.center = orgVectColCenter;
		}
	}
}