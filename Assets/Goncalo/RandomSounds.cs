using UnityEngine;
using System.Collections;

public class RandomSounds : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private AudioClip[] soundClips;
    [SerializeField] private AudioSource audioSource;
    
    [Header("Position Settings")]
    [SerializeField] private float radius = 10f;
    
    [Header("Timing Settings")]
    [SerializeField] private float minTimeBetweenSounds = 2f;
    [SerializeField] private float maxTimeBetweenSounds = 8f;
    
    private Vector3 originPosition;
    
    void Start()
    {
        // Store the origin position
        originPosition = transform.position;
        
        // Get or add AudioSource component
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // Configure AudioSource for spatial audio
        audioSource.spatialBlend = 1f; // 3D sound
        audioSource.playOnAwake = false;
        
        // Start the random sound coroutine
        StartCoroutine(PlayRandomSoundsRoutine());
    }
    
    private IEnumerator PlayRandomSoundsRoutine()
    {
        while (true)
        {
            // Wait for a random amount of time
            float waitTime = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
            yield return new WaitForSeconds(waitTime);
            
            // Check if we have sounds to play
            if (soundClips != null && soundClips.Length > 0)
            {
                // Move to random position within radius
                Vector3 randomPosition = GetRandomPositionInRadius();
                transform.position = randomPosition;
                
                // Select and play a random sound
                AudioClip randomClip = soundClips[Random.Range(0, soundClips.Length)];
                audioSource.clip = randomClip;
                audioSource.Play();
                
                // Wait for the sound to finish playing
                yield return new WaitForSeconds(randomClip.length);
                
                // Return to origin position
                transform.position = originPosition;
            }
        }
    }
    
    private Vector3 GetRandomPositionInRadius()
    {
        // Generate a random point within a circle (2D) and apply it to the XZ plane
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        return originPosition + new Vector3(randomCircle.x, 0, randomCircle.y);
    }
    
    // Optional: Visualize the radius in the editor
    private void OnDrawGizmosSelected()
    {
        Vector3 center = Application.isPlaying ? originPosition : transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, radius);
    }
}
