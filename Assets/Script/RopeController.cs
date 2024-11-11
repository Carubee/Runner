using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public Transform anchorPoint; // จุดที่เชือกยึดอยู่ เช่น เพดาน
    public Rigidbody playerRigidbody; // Rigidbody ของตัวผู้เล่น
    public float springStrength = 50f; // ความแข็งของสปริง
    public float springDamper = 5f; // ตัวหน่วงของสปริง
    public float maxDistance = 10f; // ระยะทางสูงสุดที่เชือกจะยืดได้

    private SpringJoint springJoint;

    void Start()
    {
        // เพิ่ม Spring Joint ให้กับผู้เล่น
        springJoint = playerRigidbody.gameObject.AddComponent<SpringJoint>();

        // เชื่อมต่อกับ anchorPoint
        springJoint.connectedAnchor = anchorPoint.position;

        // ตั้งค่าความแข็งและแรงหน่วงของสปริง
        springJoint.spring = springStrength;
        springJoint.damper = springDamper;

        // กำหนดระยะทางสูงสุดที่เชือกจะยืด
        springJoint.maxDistance = maxDistance;

        // ใช้ gravity ให้ผู้เล่นตกลงมาอย่างสมจริง
        playerRigidbody.useGravity = true;
    }

    void Update()
    {
        // อัปเดตตำแหน่งของ anchorPoint หากมีการเคลื่อนที่
        if (springJoint != null)
        {
            springJoint.connectedAnchor = anchorPoint.position;
        }
    }
}
