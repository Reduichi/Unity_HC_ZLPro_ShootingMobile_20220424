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
        [SerializeField, Header("�����V�ϥ�")]
        private Transform traDirectionicon;
        [SerializeField, Header("�����V�ϥܽd��"), Range(0, 5)]
        private float rangeDirectionicon = 2.5f;
        [SerializeField, Header("�������t��"), Range(0, 100)]
        private float speedturn = 1.5f;

        private Rigidbody rig;

		private void Awake()
		{
            rig = GetComponent<Rigidbody>();
		}

		private void Update()
		{
            // GetJoystickValue();
            UpdateDirectionIconPos();
            LookDirectionIcon();

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
            // ���o���V���� = �|�줸.���V����(��V�ϥ� - ����) - ��V�ϥܻP���⪺�V�q
            Quaternion look = Quaternion.LookRotation(traDirectionicon.position - transform.position);
            // ���⪺���� = �|�줸.����(���⪺���סA���V���סA����t�� * �@�����ɶ�)
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedturn * Time.deltaTime);
            // ���⪺�کԨ��� = �T���V�q(0�A�쥻���کԨ���Y�A0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }

}


