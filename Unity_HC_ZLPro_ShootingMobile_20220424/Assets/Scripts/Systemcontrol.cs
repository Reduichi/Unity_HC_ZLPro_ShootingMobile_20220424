using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

// namespace �R�W�Ŷ�
namespace RED
{
    /// <summary>
    /// ����t�� : ��ð����ʥ\��
    /// �����n�챱��Ⲿ��
    /// </summary>
    public class Systemcontrol : MonoBehaviourPun
    {
        [SerializeField, Header("���ʳt��"), Range(0, 300)]
        private float speed = 10.5f;
        [SerializeField, Header("�����V�ϥܽd��"), Range(0, 5)]
        private float rangeDirectionicon = 2.5f;
        [SerializeField, Header("�������t��"), Range(0, 100)]
        private float speedturn = 1.5f;
        [SerializeField, Header("�ʵe�Ѽƶ]�B")]
        private string parameterWalk = "�}���]�B";
        [SerializeField, Header("�e��")]
        private GameObject goCanvas;
        [SerializeField, Header("�e�����a��T")]
        private GameObject goCanvasPlayerInfo;
        [SerializeField, Header("�����V�ϥ�")]
        private GameObject goDirection;

        private Rigidbody rig;
        private Animator ani;
        private Joystick joystick;
        private Transform traDirectionicon;
        private CinemachineVirtualCamera cvc;
        private SystemAttack systemAttack;
        private DamageManager damageManager;

        private void Awake()
		{
            rig = GetComponent<Rigidbody>();
            ani = GetComponent<Animator>();
            systemAttack = GetComponent<SystemAttack>();
            damageManager = GetComponent<DamageManager>();

            // �p�G�O�s�u�i�J�����a �N�ͦ����a�ݭn������
            if (photonView.IsMine)
            {
                PlayerUIFollow follow = Instantiate(goCanvasPlayerInfo).GetComponent<PlayerUIFollow>();
                follow.traPlayer = transform;
                traDirectionicon = Instantiate(goDirection).transform;      // ���o�����V�ϥ�

                // transform.Finf(�l����W��) - �z�L�W�ٷj�M�l����
                GameObject tempCanvas = Instantiate(goCanvas);
                joystick = tempCanvas.transform.Find("Dynamic Joystick").GetComponent<Joystick>();       // ���o�e�����������n��
                systemAttack.btnFire = tempCanvas.transform.Find("�o�g").GetComponent<Button>();

                cvc = GameObject.Find("CM �޲z��").GetComponent<CinemachineVirtualCamera>();                        // ���o��v�� CM �޲z��
                cvc.Follow = transform;                                                                             // ���w�l�ܪ���

                damageManager.imgHp = GameObject.Find("�Ϥ���q").GetComponent<Image>();
                damageManager.textHp = GameObject.Find("��r��q").GetComponent<TextMeshProUGUI>();
            }
            // �_�h ���O�i�J�����a �N��������t�ΡA�קK�����h�Ӫ���
            else
            {
                enabled = false;
            }
		}

		private void Update()
		{
            // GetJoystickValue();
            UpdateDirectionIconPos();
            LookDirectionIcon();
            UpdateAnimation();

        }

		private void FixedUpdate()
		{
            Move();
		}

		/// <summary>
		/// ���o�����n���
		/// </summary>
		private void GetJoystickValue()
        {
            print("<color=yellow>���� : " + joystick.Horizontal + "</color>");
        }

        /// <summary>
        /// ���ʥ\��
        /// </summary>
        private void Move() 
        {
            // ����.�[�t�� = �T���V�q(x�Ay�Az)
            rig.velocity = new Vector3(-joystick.Horizontal, 0, -joystick.Vertical) * speed;
        }

        /// <summary>
        /// �ܧ󨤦�ϥܪ��y��
        /// </summary>
        private void UpdateDirectionIconPos()
        {
            // �s�y�� = ���⪺�y�� + �T���V�q(�����n�쪺�����P����) * ��V�ϥܪ��d��
            Vector3 pos = transform.position + new Vector3(-joystick.Horizontal, 0.5f, -joystick.Vertical) * rangeDirectionicon;
            // ��s��V�ϥܪ��y�� = �s�y��
            traDirectionicon.position = pos;
        }

        /// <summary>
        /// ���V��V�ϥ�
        /// </summary>
        private void LookDirectionIcon()
        {
            // �p�G ��������� �p�� 0.1 �åB ��������� �p�� 0.1 �N ���B�z���V
            if (Mathf.Abs(joystick.Vertical) < 0.1f && Mathf.Abs(joystick.Horizontal) < 0.1f) return;

            // ���o���V���� = �|�줸.���V����(��V�ϥ� - ����) - ��V�ϥܻP���⪺�V�q
            Quaternion look = Quaternion.LookRotation(traDirectionicon.position - transform.position);
            // ���⪺���� = �|�줸.����(���⪺���סA���V���סA����t�� * �@�����ɶ�)
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedturn * Time.deltaTime);
            // ���⪺�کԨ��� = �T���V�q(0�A�쥻���کԨ���Y�A0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        /// <summary>
        /// ��s�ʵe
        /// </summary>
        private void UpdateAnimation()
        {
            bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
            ani.SetBool(parameterWalk, run);
        }
    }

}


