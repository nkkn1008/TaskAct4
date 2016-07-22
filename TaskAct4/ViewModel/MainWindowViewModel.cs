using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;
using System.Windows;
using System.Collections.ObjectModel;

namespace TaskAct4.ViewModel
{
    class MainWindowViewModel : ReactiveObject
    {
        private string _TicketName;
        public string TicketName
        {
            get { return _TicketName; }
            set { this.RaiseAndSetIfChanged(ref _TicketName, value); }
        }
        public ReactiveCommand<object> AddCommand { get; protected set; }

        public MainWindowViewModel()
        {
            this.WhenAnyValue(x => x.TicketName)
                .Throttle(TimeSpan.FromMilliseconds(400), RxApp.MainThreadScheduler)
                .Where(x=> !String.IsNullOrEmpty(x))
                .Select(_ => DisplayTaskName())
                .Subscribe();
            Tickets = new ObservableCollection<string>();
            var canAdd = this.WhenAny(x => x.TicketName, x => !String.IsNullOrWhiteSpace(x.Value));
            
            AddCommand = ReactiveCommand.Create(canAdd);
            AddCommand.Subscribe(_=>Tickets.Add(TicketName));           

        }

        private string _TaskNameView;
        public string TaskNameView
        {
            get { return _TaskNameView; }
            set { this.RaiseAndSetIfChanged(ref _TaskNameView, value); }
        }
        public async Task DisplayTaskName()
        {
            TaskNameView = TicketName;
        }

        public ObservableCollection<string> Tickets
        {
            get; private set;
        }

    }
}
