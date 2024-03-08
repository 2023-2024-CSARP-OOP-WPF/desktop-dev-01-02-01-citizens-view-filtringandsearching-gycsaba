using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MenuProject.Repos;
using MenuProject.SchoolCitizens;
using MenuProject.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace MenuProject.ViewModels.SchoolCitizens
{
    public partial class StudentViewModel : BaseViewModel
    {
        private readonly EducationLevelsRepo _educationLevelsRepo = new();
        private readonly StudentRepo _studentRepo = new();

        [ObservableProperty]
        private ObservableCollection<string> _educationLevels = new();

        [ObservableProperty]
        private ObservableCollection<Student> _students = new();

        [ObservableProperty]
        private Student _selectedStudent;  
        
        public string SearchedName { get; set; } = string.Empty;
        public bool IsGenderSearchingEnabled { get; set; } = false;
        public bool IsWoman {  get; set; } = false;
        
        public StudentViewModel()
        {
            _selectedStudent = new Student();
            UpdateView();
        }

        [RelayCommand]
        public void DoSave(Student student)
        {
            if (student.HasId)
                _studentRepo.Update(student);
            else
                _studentRepo.Insert(student);
            UpdateView();
        }

        [RelayCommand]
        void DoNewStudent()
        {
            SelectedStudent = new Student();
        }

        [RelayCommand]
        public void DoRemove(Student studentToDelete)
        {
            _studentRepo.Delete(studentToDelete);
            UpdateView();
        }

        [RelayCommand]
        public void Filtering()
        {
            List<Student> result = _studentRepo.Filtering(SearchedName);
            Students = new ObservableCollection<Student>(result);
        }

        private void UpdateView()
        {
            EducationLevels = new ObservableCollection<string>(_educationLevelsRepo.FindAll());
            Students = new ObservableCollection<Student>(_studentRepo.FindAll());
        }
    }
}
