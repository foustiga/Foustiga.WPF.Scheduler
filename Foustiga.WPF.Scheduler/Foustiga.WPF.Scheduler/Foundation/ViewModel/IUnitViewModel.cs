namespace Foustiga.WPF.Scheduler.Foundation.ViewModel
{
    public interface IUnitViewModel<DataModelType>
    {
        DataModelType DataModel { get; set; }
    }
}