using ObservableCollections;

// �������� ������ ���� ViewModel, �������� ����������� ����� WorldGameplayRootBinder � ���������� ������� ���� ViewModel
public class WorldGameplayRootViewModel
{
    public readonly IObservableCollection<BuildingViewModel> AllBuildings;

    public WorldGameplayRootViewModel(BuildingsService buildingsService)
    {
        AllBuildings = buildingsService.AllBuildings;   // �������� ������ � ����������� ������ ViewModel's
    }
}
