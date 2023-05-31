using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] protected ColorType m_MainColor;
    [SerializeField] protected CharacterAnim m_CharacterAnim;
    [SerializeField] protected SkinnedMeshRenderer m_SkinnedMeshRenderer;
    [SerializeField] protected ColorData m_ColorData;
    [SerializeField] protected BrickPlayer m_BrickPlayer;
    protected Floor m_CurrentFloor;
    protected List<Brick> m_Bricks = new List<Brick>();
    protected bool m_fistSetUp;
    protected Brick m_CurrentBrick;
    public int m_BrickCount = 0;
    protected virtual void Reset()
    {
        m_CharacterAnim = GetComponentInChildren<CharacterAnim>();
    }
    protected virtual void Start()
    {
        m_SkinnedMeshRenderer.material = m_ColorData.GetMaterial(m_MainColor);
        m_CurrentFloor = IsCollisionWithFloor();
    }
    void Update()
    {
        if (!m_CurrentFloor.IsSpawnComplete()) return;
        LoadFloor();
        ValidateBricks();
        UpdateMore();
    }
    protected virtual void UpdateMore()
    {
        if (IsCollisionWithStair() != null)
        {
            Stair stair = IsCollisionWithStair();
            OnTriggerStair(stair);
        }
    }
    protected void LoadFloor()
    {
        if (IsCollisionWithFloor() == null) return;
        m_CurrentFloor = IsCollisionWithFloor();
        m_CurrentFloor.InstantiateBrick(m_MainColor);
    }
    void ValidateBricks()
    {
        m_Bricks = m_CurrentFloor.GetListBrickByType(m_MainColor);
        m_Bricks = SortBrickByDistance(m_Bricks); 
    }
    List<Brick> SortBrickByDistance(List<Brick> bricks)
    {
        bricks.Sort((t1,t2)=>
        {
            float dist1 = Vector3.Distance(t1.transform.position, transform.position);
            float dist2 = Vector3.Distance(t2.transform.position, transform.position);
            return dist1.CompareTo(dist2);
        });
        return bricks;
    }
    public void ReLoadCurrentBrick()
    {
        if (m_Bricks.Count > 0)
        {
            m_CurrentBrick = m_Bricks[0];
        }
    }
    public void OnTriggerBrick(Brick a_brick)
    {
        OnTriggerBrickMore(a_brick);
        if (a_brick != null && a_brick.GetColor().Equals(m_MainColor) && !a_brick.IsStair())
        {
            m_CurrentFloor.DeSpawn(a_brick.gameObject);
            m_BrickCount++;
            m_BrickPlayer.Spawn(m_BrickCount - 1, m_MainColor);
        }
    }
    protected virtual void OnTriggerBrickMore(Brick a_brick)
    { }
    public virtual void OnTriggerStair(Stair a_stair)
    {
        if (a_stair == null || m_BrickCount <= 0) return;
        Brick brick = a_stair.GetBrick();
        if (!brick.GetColor().Equals(m_MainColor))
        {
            a_stair.ChangeColor(m_MainColor);
            m_BrickCount--;
            m_BrickPlayer.DeSpawnLast();
        }
        TriggerNextStair();
    }
    public void TriggerNextStair()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position + transform.up + transform.forward * -0.5f, transform.up * -1f, 3f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Stair stair = hits[i].transform.GetComponent<Stair>();
                if (stair != null)
                {
                    Brick brick = stair.GetBrick();
                    if (brick.GetColor().Equals(m_MainColor)) return;
                    BoxCollider box = stair.GetComponent<BoxCollider>();
                    if (m_BrickCount <= 0)
                    {
                        box.isTrigger = false;
                        box.center = Vector3.zero;
                    }
                    else
                    {
                        box.isTrigger = true;
                        box.center = new Vector3(0, -0.5f, 0);
                    }
                }
            }
        }
    }
    protected Floor IsCollisionWithFloor()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.up * -2f, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<Floor>())
                    return hits[i].transform.GetComponent<Floor>();
            }
            return null;
        }
        else
            return null;
    }
    protected Stair IsCollisionWithStair()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.up * -2f, 1f);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<Stair>())
                    return hits[i].transform.GetComponent<Stair>();
            }
            return null;
        }
        else
            return null;
    }
    public CharacterAnim GetCharacterAnim()
    {
        return m_CharacterAnim;
    }
    public ColorType GetColor()
    {
        return m_MainColor;
    }
}
