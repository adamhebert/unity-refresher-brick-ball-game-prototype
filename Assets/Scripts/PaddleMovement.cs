using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        static bool isMoving(MovementType movementType) =>
            mMovementToKeyCodesTempMappingSystem.TryGetValue(movementType, out KeyCode[] codes) && codes.Any(Input.GetKey);

        // Assumption is a normalized vector here for our purposes. But scaling the vector may be desired. Will have to see where game options take me.
        void translateAlongVector(Vector3 vector) =>
            this.transform.Translate(vector * Time.deltaTime * Velocity, Camera.main.transform); // Consider making some other kind of system to move things based on the main camera. This feels dangerous if left to each script/component.

        // TODO: Build an input system of some kind.
        // TODO: Allow the player to map whatever keys that want to specific things.
        if (isMoving(MovementType.Left))
        {
            // Move the paddle left.
            translateAlongVector(Vector3.left);

        }
        else if (isMoving(MovementType.Right))
        {
            // Move the paddle right.
            translateAlongVector(Vector3.right);
        }
    }

    public float Velocity = 1.0f;

    private enum MovementType
    {
        Left,
        Right,
    }

    private static readonly Dictionary<MovementType, KeyCode[]> mMovementToKeyCodesTempMappingSystem =
        new Dictionary<MovementType, KeyCode[]>
        {
            { MovementType.Left, new KeyCode[] { KeyCode.LeftArrow, KeyCode.A } },
            { MovementType.Right, new KeyCode[] { KeyCode.RightArrow, KeyCode.D } }
        };
}
