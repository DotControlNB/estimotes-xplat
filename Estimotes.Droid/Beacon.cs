using System;


namespace Estimotes {

    public class Beacon : IBeacon {
        readonly EstimoteSdk.Beacon beacon;


        public Beacon(EstimoteSdk.Beacon beacon) {
            this.beacon = beacon;
            this.Proximity = beacon.GetProximity();
			this.Distance = EstimoteManager.Instance.GetDistanceForBeacon(beacon.Name,
				beacon.ProximityUUID.ToString(),
				beacon.Major,
				beacon.Minor,
				beacon.MacAddress,
				beacon.MeasuredPower,
				beacon.Rssi);
        }


        public string Uuid => this.beacon.ProximityUUID.ToString();
        public ushort Major => (ushort)this.beacon.Major;
        public ushort Minor => (ushort)this.beacon.Minor;
		public Proximity Proximity { get; internal set; }
		internal DateTime LastPing { get; set; }

		public double Distance { get; set; }
	}
}