using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum BugsStat
{
    Normal,
    Sleep,
    Tired
}

public class Mosquito : MonoBehaviour
{
    protected int maxHealth = 1;
    protected int health = 1;
    protected float moveSpeed = 1f;
    protected float suckSpeed = 1f;

    protected Rigidbody2D rb;
    protected Animator animator;

    public BugsStat stat = BugsStat.Normal;
    public bool isDragging = false;

    private Vector3 offset;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetComponentInChildren<Animator>();
    }

    protected virtual void OnEnable()
    {
        Delegation.BugsStatChange += OnBugStatChange;
    }

    protected virtual void OnDisable()
    {
        Delegation.BugsStatChange -= OnBugStatChange;
    }

    protected void OnBugStatChange(BugsStat newStat)
    {
        ChangeStat(newStat);
    }

    protected void ChangeStat(BugsStat newStat)
    {
        stat = newStat;

        if(stat == BugsStat.Sleep)
        {
            specificTween.Kill();
            animator.speed = 0;
            transform.localScale = new Vector3(1,-1,1);
            rb.gravityScale = 0.5f;
        }

    }

    protected Tween specificTween;
    public void KillDotween()
    {
        specificTween.Kill();
    }

    public virtual void isCaught()
    {
        isDragging = true;
        specificTween.Kill();
    }

    public void MoveToRandomPosition()
    {
        Vector3 randomScreenPoint = new Vector3(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(300, Screen.height), Camera.main.nearClipPlane);
        Vector3 randomWorldPoint = Camera.main.ScreenToWorldPoint(randomScreenPoint);

        if(randomWorldPoint.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        // Vector2 pos2d = AreaManager.GetAreaPos();
        // Vector3 randomWorldPoint = new Vector3(pos2d.x, pos2d.y, 0);

        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(randomWorldPoint.x, randomWorldPoint.y));
        specificTween = transform.DOMove(randomWorldPoint, distance/moveSpeed);
    }

    public void OnDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            OnDead();
        }
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }

    public Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10; 
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
   

}
