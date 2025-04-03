using CommunityToolkit.Mvvm.ComponentModel;
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
            decimal? highest = _context.ManyWorkers.Max(w => w.Salary);
            decimal? lowest = _context.ManyWorkers.Min(w => w.Salary);
            ManyWorker highestWorker = _context.ManyWorkers.Where(w => w.Salary == highest).FirstOrDefault();
            ManyWorker lowestWorker = _context.ManyWorkers.Where(w => w.Salary == lowest).FirstOrDefault();
            return $"Legtöbb fizetést kapó személy: {highestWorker.Name}\nLegkevesebb fizetést kapó személy: {lowestWorker.Name}";
        }

        public decimal? AveragePay()
        {
            return _context.ManyWorkers.Average(w => w.Salary);
        }

        public List<ManyWorker> GetAllWorkers()
        {
            return _context.ManyWorkers.ToList();
        }

        public decimal? GetPayByEmail(string email)
        {
            ManyWorker worker = _context.ManyWorkers.FirstOrDefault(w => w.Email == email);
            return worker.Salary;
        }

        public void DeleteWorkerByEmailAndNoPay(string email)
        {
            ManyWorker worker = _context.ManyWorkers.FirstOrDefault(w =>w.Email == email);
            _context.ManyWorkers.Remove(worker);
        }

    }
}
