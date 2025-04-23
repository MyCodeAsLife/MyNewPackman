using UnityEngine;

public class PacmanFactory
{
    public Pacman CreatePacman(Vector3 position)
    {
        var pacman = Resources.Load<Pacman>("Prefabs/Pacman");
        pacman = Object.Instantiate(pacman, position, Quaternion.identity);
        pacman.gameObject.SetActive(true);
        return pacman;
    }
}
