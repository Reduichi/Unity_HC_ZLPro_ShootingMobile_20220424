using UnityEngine;

// namespace �R�W�Ŷ�
namespace RED
{
    /// <summary>
    /// ����t�� : ��ð����ʥ\��
    /// �����n�챱��Ⲿ��
    /// </summary>
    public class Systemcontrol : MonoBehaviour
    {
        [SerializeField, Header("�����n��")]
        private Joystick joystick;
        [SerializeField, Header("���ʳt��"), Range(0, 300)]
        private float speed = 10.5f;

        private Rigidbody rig;

		private void Awake()
		{
            rig = GetComponent<Rigidbody>();
		}

		private void Update()
		{
            // GetJoystickValue();
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
    }

}


