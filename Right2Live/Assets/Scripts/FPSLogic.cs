using UnityEngine;

public class FPSLogic : MonoBehaviour
{
    private float _rotationX;

    private float _minX = -50;
    private float _maxX = 50;
    private bool isPaused = false;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rotationX -= Input.GetAxis("Mouse Y");
        _rotationX = Mathf.Clamp(_rotationX, _minX, _maxX);

        // transform.rotation = Quaternion.Euler(_rotationX, 0, 0);
    }

    private void LateUpdate()
    {
        if (!isPaused) {
            transform.rotation = Quaternion.Euler(_rotationX, GetComponentInParent<PlayerLogic>().getRotationY(), 0);
            GetComponentInParent<PlayerLogic>().updateX();
        }
    }

    public void setIsPaused(bool boolean) {
        isPaused = boolean;
    }
}