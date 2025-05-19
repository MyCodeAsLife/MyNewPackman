public abstract class SceneEnterParams
{
    protected SceneEnterParams(string sceneName)
    {
        SceneName = sceneName;
    }

    public string SceneName { get; }    // Зачем?

    public T As<T>() where T : SceneEnterParams     // Для простоты каста в дочерний класс
    {
        return (T)this;
    }
}
