using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;

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

        public MainWindowViewModel()
        {
            this.WhenAnyValue(x => x.TaskName)
                .Throttle(TimeSpan.FromMilliseconds(800), RxApp.MainThreadScheduler)
                .Where(x=> !String.IsNullOrEmpty(x))
                .Select(_ => DisplayTaskName())
                .Subscribe();
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
