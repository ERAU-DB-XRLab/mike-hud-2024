using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MIKEInteractionManager : MonoBehaviour
{

    public static MIKEInteractionManager Main { get; private set; }

    [HideInInspector]
    public UnityEvent PrimaryInteract, SecondaryInteract, Speak;
    [HideInInspector]
    public UnityEvent<MoveDirection> Move;

    [SerializeField] private MIKENavigationInjector nav;

    // Start is called before the first frame update
    void Awake()
    {
        Main = this;
        Move.AddListener(OnMove);
        PrimaryInteract.AddListener(OnPrimaryInteract);
    }

    public void OnMove(MoveDirection dir)
    {
        nav.UIMove(dir);
    }

    public void OnPrimaryInteract()
    {

    }

}
