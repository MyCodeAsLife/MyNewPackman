using UnityEngine;

public class BuildingBinder : MonoBehaviour
{
    public void Bind(BuildingViewModel viewModel)
    {
        var position2D = viewModel.Position.CurrentValue;
        transform.position = new Vector3(position2D.x, position2D.y);
    }
}