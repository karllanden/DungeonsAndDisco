using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotation : MonoBehaviour

{
    [SerializeField] Transform start;
    [SerializeField] Transform end;

    [SerializeField]
    public float speed = 1f;
    private float startTime;
    private float journeyLength;
    // Start is called before the first frame update
    private bool repeat;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(start.position, end.position);
        repeat = false;
    }
    void StartRepeat()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(end.position, start.position);
        repeat = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!repeat)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(start.position, end.position, fractionOfJourney);
            transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, fractionOfJourney);
            if(transform.position == end.position)
            {
                StartRepeat();
            }
        }
        if (repeat)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(end.position, start.position, fractionOfJourney);
            transform.rotation = Quaternion.Lerp(end.rotation, start.rotation, fractionOfJourney);
            if (transform.position == start.position)
            {
                Start();
            }
        }
    }
}
