using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerProject.Models;


namespace WorkerProject.ViewModels
{
    public partial class ManyWorkerViewModel : ObservableObject
    {
        DatabaseContext _context = new DatabaseContext();

        [ObservableProperty]
        private int allWorkerCount;

        [ObservableProperty]
        private string paidAndNonPaidWorker;

        [ObservableProperty]
        private string highestAndLowestPaidWorker;

        [ObservableProperty]
        private ManyWorker _selectedWorker = new ManyWorker("teszt@", "nnnnn");

        [ObservableProperty]
        private int _newSalary = 0;

        public ManyWorkerViewModel()
        {
            _selectedWorker = new ManyWorker("teszt@", "nnnnn");
            LoadData();
        }

        [ObservableProperty]
        private double average;

        [ObservableProperty]
        private List<ManyWorker> allWorker;

        [ObservableProperty]
        private int payByEmail;


        private void LoadData()
        {
            AllWorkerCount = GetAllWorkersCount();
            PaidAndNonPaidWorker = GetPaidandNonPaidWorkers();
            HighestAndLowestPaidWorker = HighestAndLowestPaidWorkers();
            Average = AveragePay();
            AllWorker = GetAllWorkers();
        }

        [RelayCommand]
        public void GetPayByEmail()
        {
            SelectedWorker.Salary = NewSalary;
        }

        public int GetAllWorkersCount()
        {
            return _context.ManyWorkers.Count();
        }
        public string GetPaidandNonPaidWorkers()
        {
            int all = _context.ManyWorkers.Count();
            int Paid = _context.ManyWorkers.Where(w =>w.Salary > 0).Count();
            return ($"Fizetett dolgozók száma: {Paid}\nNem fizetett dolgozók száma: {all - Paid}");
        }

        public string HighestAndLowestPaidWorkers()
        {
            int highest = _context.ManyWorkers.Max(w => w.Salary);
            int lowest = _context.ManyWorkers.Min(w => w.Salary);
            ManyWorker highestWorker = _context.ManyWorkers.Where(w => w.Salary == highest).FirstOrDefault();
            ManyWorker lowestWorker = _context.ManyWorkers.Where(w => w.Salary == lowest).FirstOrDefault();
            return $"Legtöbb fizetést kapó személy: {highestWorker.Name}\nLegkevesebb fizetést kapó személy: {lowestWorker.Name}";
        }

        public double AveragePay()
        {
            return _context.ManyWorkers.Average(w => w.Salary);
        }

        public List<ManyWorker> GetAllWorkers()
        {
            return _context.ManyWorkers.ToList();
        }


        [RelayCommand]
        public void DeleteWorkerByEmailAndNoPay(string email)
        {
            ManyWorker worker = _context.ManyWorkers.FirstOrDefault(w =>w.Email == email);
            _context.ManyWorkers.Remove(worker);
        }

    }
}
