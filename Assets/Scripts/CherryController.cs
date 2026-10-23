using UnityEngine;
using System.Collections;

public class CherryController : MonoBehaviour
{
    [SerializeField] private GameObject cherryPrefab;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

    [SerializeField] private float speed = 5f;
    private GameObject currentCherry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CherryRoutine());
    }

    private IEnumerator CherryRoutine()
    {
        yield  return new WaitUntil(()=> RoundManager.gameRunning);
        while (true)
        {
            yield return new WaitForSeconds(5f);
            
            bool startLeft = Random.value < 0.5f;
            Vector3 start = startLeft ? leftSpawn.position : rightSpawn.position;
            
            Vector3 end = startLeft ? rightSpawn.position : leftSpawn.position;
            currentCherry = Instantiate(cherryPrefab, start, Quaternion.identity);
            float distance = Vector3.Distance(start, end);
            float duration = distance / speed;
            float elapsed = 0f;
            while (elapsed < duration && currentCherry != null)
            {
                elapsed += Time.deltaTime;
                
                float percentComplete = elapsed / duration;
                currentCherry.transform.position = Vector3.Lerp(start, end, percentComplete);
                yield return null;
            }

            if (currentCherry != null)
            {
                Destroy(currentCherry);
            }
            currentCherry = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
