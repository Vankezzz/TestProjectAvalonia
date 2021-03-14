using Avalonia.Controls;
using DynamicData;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectAvalonia.Models;
using TestProjectAvalonia.Services;

namespace TestProjectAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public TodoListViewModel List { get; }
        public string Greeting => "Welcome to Avalonia!";
        [Reactive] public string Data { get; set; }
        [Reactive] public ViewModelBase Content { get; set; }
        

        public MainWindowViewModel(Database db, string data = "Nill")
        {
            Data = data;
            Content = List = new TodoListViewModel(db.GetItems());
        }

        public void AddItem()
        {
            var vm = new AddItemViewModel();

            Observable.Merge(
                vm.Ok,
                vm.Cancel.Select(_ => (TodoItem)null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        List.Items.Add(model);
                    }

                    Content = List;
                });

            Content = vm;
        }

        public async Task Open()
        {
            var dialog = new OpenFileDialog();
            string[] result = null;
            dialog.Filters.Add(new FileDialogFilter() { Name = "Text", Extensions = { "txt" } });
            result = await dialog.ShowAsync(new Window());
            if (result != null)
            {
                Data = File.ReadAllText(result.First());
            }
        }
        public async Task Save()
        {
            var dialog = new SaveFileDialog();
            dialog.Filters.Add(new FileDialogFilter()

            { Name = "Text", Extensions = { "txt" } });
            var result = await dialog.ShowAsync(new Window());
            if (result != null)
            {
                File.WriteAllText(result, Data);
            }
        }
    }
}
