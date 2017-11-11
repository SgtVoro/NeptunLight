﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Xml.Linq;
using NeptunLight.DataAccess;
using NeptunLight.Models;
using NeptunLight.Services;
using ReactiveUI;

namespace NeptunLight.ViewModels
{
    public class MenuPageViewModel : PageViewModel
    {
        public MenuPageViewModel(IDataStorage storage, INeptunInterface client, INavigator navigator)
        {
            EnsureDataAccessible = ReactiveCommand.CreateFromTask(async () =>
            {
                if (storage.CurrentData == null)
                    await storage.LoadDataAsync();
                // see if there is saved data available
                if (storage.CurrentData == null)
                {
                    // need to refresh
                    navigator.NavigateTo<InitialSyncPageViewModel>(false);
                }
            });

            IObservable<bool> menuAvailable = EnsureDataAccessible.IsExecuting.Select(x => !x);
            GoToMessages = ReactiveCommand.Create(() => navigator.NavigateTo<MessagesPageViewModel>(), menuAvailable);
            GoToCalendar = ReactiveCommand.Create(() => navigator.NavigateTo<CalendarPageViewModel>(), menuAvailable);
            GoToCourses = ReactiveCommand.Create(() => navigator.NavigateTo<CoursesPageViewModel>(), menuAvailable);
            GoToExams = ReactiveCommand.Create(() => navigator.NavigateTo<ExamsPageViewModel>(), menuAvailable);
            GoToSemesters = ReactiveCommand.Create(() => navigator.NavigateTo<SemestersPageViewModel>(), menuAvailable);
            GoToPeriods = ReactiveCommand.Create(() => navigator.NavigateTo<PeriodsPageViewModel>(), menuAvailable);

            storage.WhenAnyValue(x => x.CurrentData.BasicData.Name).ToProperty(this, x => x.Name, out _name);
            storage.WhenAnyValue(x => x.CurrentData.BasicData.NeptunCode).ToProperty(this, x => x.InfoLine, out _infoLine);
        }

        private readonly ObservableAsPropertyHelper<string> _name;
        public string Name => _name.Value;

        private readonly ObservableAsPropertyHelper<string> _infoLine;
        public string InfoLine => _infoLine.Value;

        public ReactiveCommand EnsureDataAccessible { get; }

        public ReactiveCommand GoToMessages { get; }
        public ReactiveCommand GoToCalendar { get; }
        public ReactiveCommand GoToCourses { get; }
        public ReactiveCommand GoToExams { get; }
        public ReactiveCommand GoToSemesters { get; }
        public ReactiveCommand GoToPeriods { get; }

        public override string Title { get; } = "Neptun Lite";
    }
}