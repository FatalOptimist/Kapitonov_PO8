using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace UniversitySchedule;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ScheduleViewModel();
    }
}

public class ScheduleViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ScheduleItem> Schedule { get; set; } = new ObservableCollection<ScheduleItem>();
    public string NewSubject { get; set; }
    public string NewTime { get; set; }
    public string NewLecturer { get; set; }

    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }

    public ScheduleViewModel()
    {
        AddCommand = new Command(AddScheduleItem);
        DeleteCommand = new Command<ScheduleItem>(DeleteScheduleItem);

        // Пример данных
        Schedule.Add(new ScheduleItem { Subject = "Математика", Time = "10:00 - 11:30", Lecturer = "Иванов И.И." });
        Schedule.Add(new ScheduleItem { Subject = "Физика", Time = "12:00 - 13:30", Lecturer = "Петров П.П." });
    }

    private void AddScheduleItem()
    {
        if (!string.IsNullOrWhiteSpace(NewSubject) &&
            !string.IsNullOrWhiteSpace(NewTime) &&
            !string.IsNullOrWhiteSpace(NewLecturer))
        {
            Schedule.Add(new ScheduleItem { Subject = NewSubject, Time = NewTime, Lecturer = NewLecturer });
            NewSubject = NewTime = NewLecturer = string.Empty;
            OnPropertyChanged(nameof(NewSubject));
            OnPropertyChanged(nameof(NewTime));
            OnPropertyChanged(nameof(NewLecturer));
        }
    }

    private void DeleteScheduleItem(ScheduleItem item)
    {
        if (Schedule.Contains(item))
        {
            Schedule.Remove(item);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class ScheduleItem
{
    public string Subject { get; set; }
    public string Time { get; set; }
    public string Lecturer { get; set; }
}
