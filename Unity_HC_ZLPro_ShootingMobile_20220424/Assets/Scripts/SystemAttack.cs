using UnityEngine;
using UnityEngine.UI;

namespace RED
{
    /// <summary>
    /// �����t��
    /// </summary>
    public class SystemAttack : MonoBehaviour
    {
        [HideInInspector]
        public Button btnFire;
        [SerializeField, Header("�l�u")]
        private GameObject goBullet;
        [SerializeField, Header("�l�u�̤j�ƶq")]
        private int bulletCountMax = 3;
        [SerializeField, Header("�l�u�ͦ���m")]
        private Transform traFire;
        [SerializeField, Header("�l�u�o�g�t��"), Range(0, 3000)]
        private int speedFire = 500;

        private int bulletCountCurrent;

        private void Awake()
        {
            // �o�g���s.�I��.�K�[��ť��(�}�j��k) - ���U�o�g���s����}�j��k
            btnFire.onClick.AddListener(Fire);
        }

        /// <summary>
        /// �}�j
        /// </summary>
        private void Fire()
        {
            // �ͦ�(����A�y�СA����)
            Instantiate(goBullet, traFire.position, Quaternion.identity);
        }

    }
}

