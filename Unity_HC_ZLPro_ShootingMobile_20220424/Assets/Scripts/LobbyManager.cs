using UnityEngine;

/// <summary>
/// �j�U�޲z��
/// ���a���U��ԫ��s��}�l�ǰt�ж�
/// </summary>
public class LobbyManager : MonoBehaviour
{
    // GameObject �C������ : �s�� Unity �������Ҧ�����
    // SerializeField �N�p�H�����ܦb�ݩʭ��O�W
    // Header ���D�A�b�ݩʭ��O�W��ܲ���r���D
    [SerializeField, Header("�s�u���e��")]
    private GameObject goConnetView;

    // ���� : ����
    // �����s��{�����q���y�{
    // 1. ���Ѥ��}����k public Method
    // 2. ���s�b�I�� On Click ���]�w�I�s����k

    public void StartConnect() {
        print("�}�l�s�u...");

        // �C������.�Ұʳ]�w( ���L�� ) - true ��ܡAfalse ����
        goConnetView.SetActive(true);
    }
}
