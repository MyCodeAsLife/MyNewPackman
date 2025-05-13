using ObservableCollections;

// Содержит список всех ViewModel, является посредником между WorldGameplayRootBinder и реактивным списком всех ViewModel
public class WorldGameplayRootViewModel
{
    public readonly IObservableCollection<BuildingViewModel> AllBuildings;

    public WorldGameplayRootViewModel(BuildingsService buildingsService)
    {
        AllBuildings = buildingsService.AllBuildings;   // Кэшируем доступ к реактивному списку ViewModel's
    }
}
