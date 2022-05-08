using UnityEngine;

// namespace 命名空間
namespace RED
{
    /// <summary>
    /// 控制系統 : 荒野亂鬥移動功能
    /// 虛擬搖桿控制角色移動
    /// </summary>
    public class Systemcontrol : MonoBehaviour
    {
        [SerializeField, Header("虛擬搖桿")]
        private Joystick joystick;
        [SerializeField, Header("移動速度"), Range(0, 300)]
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
		/// 取得虛擬搖桿值
		/// </summary>
		private void GetJoystickValue()
        {
            print("<color=yellow>水平 : " + joystick.Horizontal + "</color>");
        }

        /// <summary>
        /// 移動功能
        /// </summary>
        private void Move() 
        {
            // 剛體.加速度 = 三維向量(x，y，z)
            rig.velocity = new Vector3(-joystick.Horizontal, 0, -joystick.Vertical) * speed;
        }
    }

}


