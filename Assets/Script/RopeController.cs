using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public Transform anchorPoint; // �ش�����͡�ִ���� �� ྴҹ
    public Rigidbody playerRigidbody; // Rigidbody �ͧ��Ǽ�����
    public float springStrength = 50f; // �����秢ͧʻ�ԧ
    public float springDamper = 5f; // ���˹�ǧ�ͧʻ�ԧ
    public float maxDistance = 10f; // ���зҧ�٧�ش�����͡���״��

    private SpringJoint springJoint;

    void Start()
    {
        // ���� Spring Joint ���Ѻ������
        springJoint = playerRigidbody.gameObject.AddComponent<SpringJoint>();

        // �������͡Ѻ anchorPoint
        springJoint.connectedAnchor = anchorPoint.position;

        // ��駤�Ҥ���������ç˹�ǧ�ͧʻ�ԧ
        springJoint.spring = springStrength;
        springJoint.damper = springDamper;

        // ��˹����зҧ�٧�ش�����͡���״
        springJoint.maxDistance = maxDistance;

        // �� gravity �������蹵�ŧ�����ҧ����ԧ
        playerRigidbody.useGravity = true;
    }

    void Update()
    {
        // �ѻവ���˹觢ͧ anchorPoint �ҡ�ա������͹���
        if (springJoint != null)
        {
            springJoint.connectedAnchor = anchorPoint.position;
        }
    }
}
