using UnityEngine;
using Photon.Pun;

namespace RED
{
    /// <summary>
    /// �R������
    /// </summary>
    public class DestroySelf : MonoBehaviourPun
    {
        [SerializeField, Header("�R���ɶ�"), Range(0, 10)]
        private float timeDestroy = 5;
        [SerializeField, Header("�O�_�ݭn�I����R��")]
        private bool collisionDestroy;

        private void Awake()
        {
            Invoke("DestroyDelay", timeDestroy);
        }

        /// <summary>
        /// ����R����k
        /// </summary>
        private void DestroyDelay()
        {
            // �s�u.�R��(�C������) - �R�����A����������
            PhotonNetwork.Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // �p�G �ݭn�I����R�� �N �s�u.�R��
            if (collisionDestroy)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}


