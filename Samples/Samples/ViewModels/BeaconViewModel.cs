using System;
using Acr;
using Estimotes;


namespace Samples.ViewModels {

	public class BeaconViewModel : ViewModel {

		public BeaconViewModel(IBeacon beacon) {
			this.Information = $"{beacon.Proximity}";
			//			this.Information = $"{beacon.Proximity} {beacon.Identifier}";

			//$"Major: {beacon.Major} - Minor: {beacon.Minor} \n- ID: {beacon.Uuid}
			this.Details = $"- Distance: {beacon.Distance}";
		}


		public string Information { get; }
		public string Details { get; }
	}
}