using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;
using System.Windows;

namespace TaskAct4.ViewModel
{
    class MainWindowViewModel : ReactiveObject
    {
        private string _TaskName;
        public string TaskName
        {
            get { return _TaskName; }
            set { this.RaiseAndSetIfChanged(ref _TaskName, value); }
        }
        public ReactiveCommand<object> AddCommand { get; protected set; }

        public MainWindowViewModel()
        {
            this.WhenAnyValue(x => x.TaskName)
                .Throttle(TimeSpan.FromMilliseconds(800), RxApp.MainThreadScheduler)
                .Where(x=> !String.IsNullOrEmpty(x))
                .Select(_ => DisplayTaskName())
                .Subscribe();

            var canAdd = this.WhenAny(x => x.TaskName, x => !String.IsNullOrWhiteSpace(x.Value));
            //var AddCommand = ReactiveCommand.CreateAsyncTask(canAdd, x => doSthAsync(x));
            //AddCommand = ReactiveCommand.Create(canAdd, x => doSthAsync(x));
            AddCommand = ReactiveCommand.Create(this.WhenAny(x => x.TaskName, x => !string.IsNullOrEmpty(x.Value)));
            AddCommand.Subscribe(_ => MessageBox.Show("You clicked on DisplayCommand: Name is " + TaskName));
        }

        private Task doSthAsync(object parameter)
        {
            return Task.FromResult(true);
        }

        private string _TaskNameView;
        public string TaskNameView
        {
            get { return _TaskNameView; }
            set { this.RaiseAndSetIfChanged(ref _TaskNameView, value); }
        }
        public async Task DisplayTaskName()
        {
            TaskNameView = TaskName;
        }

        
    }
}
