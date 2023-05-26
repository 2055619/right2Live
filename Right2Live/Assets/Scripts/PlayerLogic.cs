using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 10;
    [SerializeField] private float rotationSpeed = 2;
    [SerializeField] private float jumpingSpeed = 10;
    [SerializeField] private float gravity = -20;
    [SerializeField] private float currentJumpingSpeed;
    [SerializeField] private float doorXOffset = 1.5f;
    [SerializeField] private float doorZOffset = 1.5f;
    [SerializeField] private float playerHp = 100;
    [SerializeField] private TextMeshProUGUI hpUGUIText;
    [SerializeField] private Slider hpUGUISlider;

    CharacterController _characterController;

    private float _rotationY;
    private float _jumpY;
    private bool _isInAir;

    private Transform _orientation;
    private GameObject _selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _characterController = GetComponent<CharacterController>();
        _jumpY = 0.0f;
        _isInAir = false;

        hpUGUIText.text = playerHp.ToString();
        hpUGUISlider.value = playerHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f")) {
            interactDoor();
        }
        MoveCharacter();
        manageJump();
        checkHpSlider();
    }

    private void checkHpSlider() {
        if (hpUGUISlider.value != playerHp) {
            float damageToTake = hpUGUISlider.value - playerHp;
            Mathf.Clamp(damageToTake, 0, 100);
            if (damageToTake <= 0.2) {
                hpUGUISlider.value = playerHp;
                return;
            }
            hpUGUISlider.value = hpUGUISlider.value - (damageToTake * 5 * Time.deltaTime);
        }
    }

    private void MoveCharacter()
    {
        if(_characterController != null)
        {
            Vector3 inputVector = new Vector3(
                Input.GetAxis("Horizontal") * movingSpeed * Time.deltaTime, 
                _jumpY, 
                Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime);
            
            _characterController.Move(transform.TransformDirection(inputVector));
        }
    }

    public float getRotationY()
    {
        _rotationY += Input.GetAxis("Mouse X") * rotationSpeed;
        return _rotationY;
    }
    
    private void manageJump()
    {
        if (Input.GetButtonDown("Jump") && !_isInAir)
        {
            _isInAir = true;
            currentJumpingSpeed = jumpingSpeed;
        }
        
        _jumpY = (currentJumpingSpeed * Time.deltaTime)/2;
        currentJumpingSpeed += gravity * Time.deltaTime;
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((_characterController.collisionFlags & CollisionFlags.Below) !=0 )
        {
            _isInAir = false;
            currentJumpingSpeed = 0;
        }
    }
    public void updateX() {
        transform.rotation = Quaternion.Euler(0, getRotationY(), 0);
    }
    private void interactDoor() {
        RaycastHit hit = CastRay();
        if (hit.collider != null) {
            if (hit.collider.CompareTag("DoorClosed")) {
                _selectedObject = hit.collider.gameObject;
                _selectedObject.transform.position = new Vector3(_selectedObject.transform.position.x - doorXOffset, _selectedObject.transform.position.y, _selectedObject.transform.position.z - doorZOffset);
                _selectedObject.transform.rotation = Quaternion.Euler(_selectedObject.transform.rotation.x, _selectedObject.transform.rotation.y, _selectedObject.transform.rotation.z);
                _selectedObject.tag = "DoorOpened";
            }
            else if (hit.collider.CompareTag("DoorOpened")) {
                _selectedObject = hit.collider.gameObject;
                _selectedObject.transform.position = new Vector3(_selectedObject.transform.position.x + doorXOffset, _selectedObject.transform.position.y, _selectedObject.transform.position.z + doorZOffset);
                _selectedObject.transform.rotation = Quaternion.Euler(_selectedObject.transform.rotation.x, _selectedObject.transform.rotation.y - 90, _selectedObject.transform.rotation.z);
                _selectedObject.tag = "DoorClosed";
            }
        }
    }

    private RaycastHit CastRay() {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        return hit;
    }

    // Le joueur prend des dégats
    // Si le joueur n'a plus de vie, on charge la scène de fin de jeu en lui indiquant qu'il a perdu
    public void DamagePlayer(float damage)
    {
        playerHp -= damage;
        hpUGUIText.text = playerHp.ToString();
        if (playerHp <= 0)
        {
            EndGameManager.isWin = false;
            SceneManager.LoadScene("EndGame");
        }
    }  
}