using UnityEngine;

namespace RED
{
    /// <summary>
    /// ���a�����l��
    /// �����l�ܪ��a����y��
    /// </summary>
    public class PlayerUIFollow : MonoBehaviour
    {
        [SerializeField, Header("�첾")]
        private Vector3 v30ffset;
        private string namePlayer = "�Ԥh";
        private Transform traPlayer;

        private void Awake()
        {
            // ���a�ܧΤ��� = �C������.�M��(����W��).�ܧΤ���
            traPlayer = GameObject.Find(namePlayer).transform;
        }

        private void Update()
        {
            Follow();
        }

        /// <summary>
        /// �l�ܪ��a
        /// </summary>
        private void Follow()
        {
            transform.position = traPlayer.position + v30ffset;
        }

    }

}

