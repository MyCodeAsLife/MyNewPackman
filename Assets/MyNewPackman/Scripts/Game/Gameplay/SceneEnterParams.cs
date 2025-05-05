public abstract class SceneEnterParams
{
    protected SceneEnterParams(string sceneName)
    {
        SceneName = sceneName;
    }

    public string SceneName { get; }    // Зачем?

    public T As<T>() where T : SceneEnterParams     // Ограничиваем каст дочерних классов.
    {
        return (T)this;
    }
}
