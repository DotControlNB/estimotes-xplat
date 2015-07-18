﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Acr;
using Acr.UserDialogs;
using Estimotes;
using Samples.Pages;


namespace Samples.ViewModels {

    public class MainViewModel : LifecycleViewModel {

        public MainViewModel() {
            this.GotoRegions = new Command(async () => {
				if (this.IsBeaconFunctionalityAvailable)
					await App.Current.MainPage.Navigation.PushAsync(new RegionListPage());
			});
        }


        public override async void OnActivate() {
			base.OnActivate();
            this.List = new List<Beacon>();

            this.IsBeaconFunctionalityAvailable = await EstimoteManager.Instance.Initialize();
            if (!this.IsBeaconFunctionalityAvailable)
				UserDialogs.Instance.Alert("Beacon functionality not enabled in app permissions");

            else {
                EstimoteManager.Instance.Ranged += this.OnRanged;
                foreach (var region in App.Regions)
                    EstimoteManager.Instance.StartRanging(region);
            }
		}


		public override void OnDeactivate() {
			base.OnDeactivate();
            if (this.IsBeaconFunctionalityAvailable) {
                EstimoteManager.Instance.Ranged -= this.OnRanged;
                foreach (var region in App.Regions)
                    EstimoteManager.Instance.StopRanging(region);
            }
		}


		private void OnRanged(object sender, IEnumerable<Beacon> beacons) {
			var list = beacons.ToList();
            this.List = list;
			this.OnPropertyChanged("List");
            //if (beacon.Proximity == Proximity.Unknown) {
            //    Debug.WriteLine("Removing " + beacon.Region.Identifier);
            //    this.List.Remove(beacon);
            //    this.OnPropertyChanged("List");
            //}
            //else {
            //    var index = this.List.IndexOf(beacon);
            //    if (index < 0)
            //        Debug.WriteLine("Adding " + beacon.Region.Identifier);
            //    else {
            //        Debug.WriteLine("Updating " + beacon.Region.Identifier);
            //        this.List.RemoveAt(index);
            //    }
            //    this.List.Add(beacon);
            //    this.OnPropertyChanged("List");
            //}
		}


		public ICommand GotoRegions { get; }
        public IList<Beacon> List { get; private set; }

        private bool isBeaconFunctionalityAvailable;
        public bool IsBeaconFunctionalityAvailable {
            get { return this.isBeaconFunctionalityAvailable; }
            private set { this.SetProperty(ref this.isBeaconFunctionalityAvailable, value); }
        }
    }
}