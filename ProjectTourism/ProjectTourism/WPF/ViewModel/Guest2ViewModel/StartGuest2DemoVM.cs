using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using ProjectTourism.Utilities;
using ProjectTourism.DTO;
using System;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ProjectTourism.WPF.ViewModel.Guest2ViewModel
{
    public class StartGuest2DemoVM : ViewModelBase
    {
        // Contructors
        #region
        public StartGuest2DemoVM() { }
        public StartGuest2DemoVM(Guest2DTO guest2, NavigationVM navigationVM) 
        {
            DemoOn = true; 
            Guest2 = guest2;
            NavigationVM = navigationVM;
            StartCommand = new RelayCommand(StartDemo);
            StopCommand = new RelayCommand(StopDemo);

            try
            {
                DemoIsOn(cts.Token);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Demo error!");
            }
        }
        #endregion
        private async Task DemoIsOn(CancellationToken ct)
        {
            do  // until you StopDemo
            {   // TO DO -> display all
                ct.ThrowIfCancellationRequested();

                //// FEATURE 1 -> TOUR REQUESTS
                NavigationVM.ShowPopupMessage("DEMO: Started! First feature - Tour Requests!", 3000);
                await Task.Delay(1000, ct);
                // FEATURE 1A -> TourRequests Statistics
                NavigationVM.CurrentView = tourRequestsVM;
                await Task.Delay(1000, ct);
                TourRequestStatisticsVM tourRequestStatisticsVM = new TourRequestStatisticsVM(Guest2);
                NavigationVM.CurrentView = tourRequestStatisticsVM;
                await Task.Delay(1000, ct);
                tourRequestStatisticsVM.SelectedYear = tourRequestStatisticsVM.Years[0];
                tourRequestStatisticsVM.YearSelectionChangedCommand.Execute(ct);
                // TO DO -> display year in ComboBox
                await Task.Delay(2000, ct);
                NavigationVM.ShowPopupMessage("DEMO: Tour Requests Statistics \nfiltered by year!", 3000);
                await Task.Delay(2000, ct);

                // FEATURE 1B -> CreateTourRequest
                NavigationVM.CurrentView = tourRequestsVM;
                await Task.Delay(1000, ct);
                CreateTourRequestVM createTourRequestVM = new CreateTourRequestVM(Guest2);
                NavigationVM.CurrentView = createTourRequestVM;
                await Task.Delay(1000, ct);

                // display invalid tourRequest create 
                NavigationVM.ShowPopupMessage("DEMO: Tour Request can't be made because \nthe data were not entered correctly.", 2000);
                await Task.Delay(1000, ct);
                createTourRequestVM.NewLocation.Country = "Rusija";
                await Task.Delay(1000, ct);
                createTourRequestVM.NewLocation.City = "Moskva";
                await Task.Delay(1000, ct);
                createTourRequestVM.TourRequest.Language = "Srpski";
                await Task.Delay(1000, ct);
                createTourRequestVM.TourRequest.NumberOfGuests = 20;
                await Task.Delay(1000, ct);
                createTourRequestVM.TourRequest.Description = "Caroban obilazak Moskve, setnja Crvenim trgom!";
                await Task.Delay(1000, ct);
                createTourRequestVM.StartDate = DateTime.Now.AddDays(10);
                createTourRequestVM.StartDateChangedCommand.Execute(ct);
                await Task.Delay(1000, ct);
                //// display invalid tourRequest create 
                //createTourRequestVM.EndDate = DateTime.Now.AddDays(5);
                ////createTourRequestVM.EndDateChangedCommand.Execute(ct);
                //NavigationVM.ShowPopupMessage("DEMO: Invalid start and end date!", 2000);
                //await Task.Delay(2000, ct);
                createTourRequestVM.EndDate = DateTime.Now.AddDays(45);
                createTourRequestVM.EndDateChangedCommand.Execute(ct);
                await Task.Delay(1000, ct);
                //TO DO -> dont execute the commands, data will be saved
                //TO DO -> create tour request, display it and delete it
                //createTourRequestVM.CreateTourRequestCommand.Execute(null);
                NavigationVM.ShowPopupMessage("DEMO: Tour Request successfully created!", 2000);
                NavigationVM.CurrentView = tourRequestsVM;
                await Task.Delay(1000, ct);

                // FEATURE 2 -> COMPLEX TOURS
                NavigationVM.ShowPopupMessage("Second feature - Complex Tour Requests!", 3000);
                await Task.Delay(2000, ct);
                NavigationVM.CurrentView = complexToursVM;
                await Task.Delay(1000, ct);
                // FEATURE 2A -> CreateComplexTourRequest
                CreateComplexTourRequestVM createComplexTourRequestVM = new CreateComplexTourRequestVM(Guest2);
                NavigationVM.CurrentView = createComplexTourRequestVM;
                await Task.Delay(1000, ct);
                // part 1
                CreateComplexTourPartVM createComplexTourPartVM = (CreateComplexTourPartVM)createComplexTourRequestVM.PartsFormContent;
                createComplexTourPartVM.NewLocation.Country = "Rusija";
                await Task.Delay(1000, ct);
                createComplexTourPartVM.NewLocation.City = "Petrograd";
                await Task.Delay(1000, ct);
                createComplexTourPartVM.NewTourRequestPart.Language = "English";
                await Task.Delay(1000, ct);
                createComplexTourPartVM.NewTourRequestPart.NumberOfGuests = 50;
                await Task.Delay(1000, ct);
                createComplexTourPartVM.NewTourRequestPart.Description = "Caroban obilazak Petrograda! Mora da se obidje Petrov dvorac!";
                await Task.Delay(1000, ct);
                createComplexTourPartVM.StartDate = DateTime.Now.AddDays(10);
                createComplexTourPartVM.StartDateChangedCommand.Execute(ct);
                await Task.Delay(1000, ct);
                createComplexTourPartVM.EndDate = DateTime.Now.AddDays(45);
                createComplexTourPartVM.EndDateChangedCommand.Execute(ct);
                await Task.Delay(1000, ct);
                NavigationVM.ShowPopupMessage("DEMO: Complex Tour Request Part successfully created!", 2000);
                createComplexTourPartVM.AddTourRequestCommand.Execute(ct);
                ComplexTourPartsVM complexTourPartsVM = (ComplexTourPartsVM)createComplexTourRequestVM.PartsContent;
                await Task.Delay(3000, ct);
                NavigationVM.ShowPopupMessage("DEMO: Complex Tour Request \nmust have at least 2 parts!", 2000);
                // part 2
                createComplexTourPartVM.NewLocation.Country = "Rusija";
                await Task.Delay(1000, ct);
                createComplexTourPartVM.NewLocation.City = "Moskva";
                await Task.Delay(1000, ct);
                createComplexTourPartVM.NewTourRequestPart.Language = "Srpski";
                await Task.Delay(1000, ct);
                createComplexTourPartVM.NewTourRequestPart.NumberOfGuests = 20;
                await Task.Delay(1000, ct);
                createComplexTourPartVM.NewTourRequestPart.Description = "Caroban obilazak Moskve!";
                await Task.Delay(1000, ct);
                createComplexTourPartVM.StartDate = DateTime.Now.AddDays(10);
                createComplexTourPartVM.StartDateChangedCommand.Execute(ct);
                await Task.Delay(1000, ct);
                createComplexTourPartVM.EndDate = DateTime.Now.AddDays(45);
                createComplexTourPartVM.EndDateChangedCommand.Execute(ct);
                await Task.Delay(1000, ct);
                NavigationVM.ShowPopupMessage("DEMO: Complex Tour Request Part \nsuccessfully created!", 2000);
                createComplexTourPartVM.AddTourRequestCommand.Execute(ct);
                await Task.Delay(3000, ct);
                // TO DO -> create complex Tour, display it and delete it
                NavigationVM.ShowPopupMessage("DEMO: Complex Tour Request created!", 2000);
                await Task.Delay(1000, ct);
                NavigationVM.CurrentView = complexToursVM;
                await Task.Delay(3000, ct);

                //NavigationVM.ShowPopupMessage("DEMO: Finished demo! It will start again!\nIf you wish to stop demo, click F!", 6000);
                NavigationVM.ShowPopupMessage("DEMO: Finished demo! It will start again!\nIf you wish to stop demo, \nclick stop demo button!", 6000);
                await Task.Delay(3000, ct);
            } while (DemoOn);
        }

        
        // Attributes
        #region
        public Guest2DTO Guest2 { get; set; }
        private CancellationTokenSource cts = new CancellationTokenSource();
        private bool _DemoOn;
        public bool DemoOn
        {
            get { return _DemoOn; }
            set
            {
                _DemoOn = value;
                OnPropertyChanged();
            }
        }
        
        public NavigationVM NavigationVM { get; set; }
        //private HomeVM homeVM => new HomeVM(Guest2);
        //private TicketsVM ticketsVM => new TicketsVM(Guest2);
        //private VouchersVM vouchersVM => new VouchersVM(Guest2);
        //private ProfileVM profileVM => new ProfileVM(Guest2);
        private TourRequestsVM tourRequestsVM => new TourRequestsVM(Guest2);
        private ComplexToursVM complexToursVM => new ComplexToursVM(Guest2);
        #endregion

        // Commands
        #region
        public ICommand StartCommand { get; set; }
        private void StartDemo(object obj)
        {
            DemoOn = true;
        }
        public ICommand StopCommand { get; set; }
        private void StopDemo(object obj)
        {
            DemoOn = false;
            OnPropertyChanged(nameof(DemoOn));
            cts.Cancel();
        }
        #endregion


        // TO DO -> make new class
        // OneAtTime // displays one character per 1 sec (or 0.5sec)
        #region
        //public String demoWord { get; set; }
        //public int i { get; set; }
        //private String _OneAtTime;
        //public String OneAtTime
        //{
        //    get { return _OneAtTime; }
        //    set { _OneAtTime = value; OnPropertyChanged(); }
        //}

        //private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        //private void StartTimer(bool IsStarted)
        //{
        //    if (IsStarted)
        //    {
        //        // write one character each second
        //        dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        //        dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        //        dispatcherTimer.Start();
        //    }
        //}
        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    if (i == demoWord.Length - 1)
        //    {
        //        // StopTimer
        //    }
        //    else
        //    {
        //        OneAtTime += demoWord[i];
        //        OnPropertyChanged(nameof(OneAtTime));
        //    }


        //    // Forcing the CommandManager to raise the RequerySuggested event
        //    CommandManager.InvalidateRequerySuggested();
        //}
        ////private String OneAtTime(String demoWord, int waiting)
        ////{
        ////    string oneAtTime = "";
        ////    StartTimer(true);
        ////    for (int i = 0; i <= demoWord.Length - 1; i++)
        ////    {
        ////        oneAtTime += demoWord[i];
        ////        //await Task.Delay(waiting, ct);

        ////    }
        ////    return oneAtTime;
        ////}
        #endregion
    }
}
