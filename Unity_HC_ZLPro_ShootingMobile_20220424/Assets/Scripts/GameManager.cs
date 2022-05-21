using UnityEngine;
using Photon.Pun;
using System.Linq;                  // �ޥ� �t�άd�߻y�� (��Ƶ��c�ഫ API)
using System.Collections.Generic;   // �ޥ� �t�ΤΩM�@�� (��Ƶ��c�AList, ArrayList...)

namespace RED
{
    /// <summary>
    /// �C���޲z��
    /// �P�_�p�G�O�s�u�i�J�����a
    /// �N�ͦ����⪫�� (�Ԥh)
    /// </summary>
    public class GameManager : MonoBehaviourPun
    {
        [SerializeField, Header("���⪫��")]
        private GameObject goCharacter;
        [SerializeField, Header("�ͦ��y�Ъ���")]
        private Transform[] traSpawnPoint;

        /// <summary>
        /// �x�s�ͦ��y�вM��
        /// </summary>
        [SerializeField]
        private List<Transform> traSpawnPointList;

        private void Awake()
        {
            traSpawnPointList = new List<Transform>();      // �s�W �M�檫��
            traSpawnPointList = traSpawnPoint.ToList();     // �}�C�ର�M���Ƶ��c

            // �p�G�O�s�u�i�J�����a�N�b���A���ͦ����⪫��
            // if (photonView.IsMine)
            // {
                int indexRandom = Random.Range(0, traSpawnPointList.Count);     // ���o�H���M��(0�A�M�檺����)
                Transform tra = traSpawnPointList[indexRandom];                 // ���o�H���y��

                // Photon ���A��.�ͦ�(����.�W�١A�y�СA����)
                PhotonNetwork.Instantiate(goCharacter.name, tra.position, tra.rotation);

                traSpawnPointList.RemoveAt(indexRandom);
           // }
        }
    }

}
