using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    [Header("Hitboxes")]
    public GameObject[] HitBoxes = new GameObject[3];
    [Header("Variables Related to Attacking")]
    public bool isAttacking = false;
    public float afterAtkDelayTimer = 0;
    public float finalAtkDelayTimer = 0;
    public string[] GroundAttTriggerName = new string[3];
    public string[] BowAttTriggerName = new string[2];
    public int wepTypeID = 0;
    public string AttButtonName = null;
    public float enemyProx = 0;
    public enum AtkType { dagger, bow, wand, sword, spear, axe};
    public AtkType AT;
    public float AttackDamage = 0;
    public int[] PrevHitBoxID = new int[2];
    [Header("Variables related to wand projectile type attacks")]
    public Transform m_projOrigin = null;
    public GameObject m_Projectile = null;
    public float projSpeed = 0;
    public float projDuration = 0;
    [Header("Variables related to Bow type attacks, note for Reference to Obj only")]
    public GameObject[] bowAttacks = new GameObject[4];
    [Header("Variables Related to Movement")]
    public float GHorizontalSpeed = 0;
    public float AHoriSpdMulti = 0;
    public bool facingRight = true;
    [Header("Variables Related To Jumping")]
    public bool m_jumpPressed;
    public bool m_canJump;
    public float VerticalSpeed = 0;
    [Header("Variables Related To Grounded Dash")]
    public float m_GDCD = 0;
    public float m_GDSpd = 0;
    public float m_GDTime = 0;
    public bool m_canGDash = true;
    public bool m_isDashing = false;
    [Header("Variables Related to Aerial Dash")]
    public int m_DashMax = 0;
    public float m_ADSpd = 0;
    public float m_ADTime = 0;
    public bool m_canADash = true;
    public bool m_isADashing = false;
    [Header("Variables Related to Blink Teleport")]
    public bool m_canBTP = true;
    public float m_maxTeleDist = 0;
    public string m_teleTargetTag = null;
    public float m_btTime = 0;
    public float m_btCD = 0;
    public bool m_isTPing = false;
    public Vector3[] m_offSet = new Vector3[2];
    public float[] m_randNoise = new float[2];
    [Header("Variables related to getting hit")]
    public float hitStunDuration = 0;
    public bool canAerialRecover = false;
}
