using UnityEngine;
using System;

public class TankMovement : MonoBehaviour
{
    [SerializeField] public int m_PlayerNumber = 1;
    [SerializeField] private float m_Speed = 10f;
    [SerializeField] private float m_TurnSpeed = 180;
    [SerializeField] private AudioSource m_MovementAudio;
    [SerializeField] private AudioClip m_EngineIdle;
    [SerializeField] private AudioClip m_EngineDriving;
    [SerializeField] private float m_PitchRange = 0.2f;

    private Rigidbody m_Rigidbody;
    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }
    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }
    void Start()
    {
        m_MovementAxisName = "Horizontal" + m_PlayerNumber;
        m_TurnAxisName = "Vertical" + m_PlayerNumber;
        m_OriginalPitch = m_MovementAudio.pitch;

    }
    void Update()
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
      /*  m_MovementInputValue = Input.GetAxis("Horizontal");
        m_TurnInputValue = Input.GetAxis("Vertical");*/
        EngineAudio();
    }
    private void EngineAudio()
    {
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdle;
                m_MovementAudio.pitch = UnityEngine.Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdle)
            {

                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = UnityEngine.Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }
    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 Movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + Movement);
    }
    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnSpeed = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnSpeed);
    }
}
