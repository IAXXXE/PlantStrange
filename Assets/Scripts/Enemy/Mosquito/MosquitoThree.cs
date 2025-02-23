using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MosquitoThree : Mosquito
{
    private float startTime;

    void OnEnable()
    {
        base.OnEnable();

        moveSpeed = 6f;
        MoveToRandomPosition();
        startTime = Time.time;
    }

    void Update()
    {
        if(isDragging == true) 
        {
            return;
        }

        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= 2f)
        {
            if(stat == BugsStat.Tired) return;
            startTime = Time.time;
            MoveToCursor();
        }

        if(elapsedTime >= 5f)
        {
            startTime = Time.time;
            stat = BugsStat.Normal;
        }
    }

    public override void isCaught()
    {
        if(!(stat == BugsStat.Tired)) return;
        isDragging = true;
        specificTween.Kill();
    }

    private void MoveToCursor()
    {
        var mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        if(targetPosition.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        // Vector2 pos2d = AreaManager.GetAreaPos();
        // Vector3 randomWorldPoint = new Vector3(pos2d.x, pos2d.y, 0);

        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetPosition.x, targetPosition.y));
        specificTween = transform.DOMove(targetPosition, distance/moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(stat == BugsStat.Tired) return;

        if (collider.gameObject.CompareTag("Cursor"))
        {
            KillDotween();
            var hasBug = collider.transform.GetComponent<Cursor>().LetBugsGo();
            if(hasBug) stat = BugsStat.Tired;
        }
    }

}
