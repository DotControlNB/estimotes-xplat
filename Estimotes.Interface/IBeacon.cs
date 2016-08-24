using System;


namespace Estimotes {

    public interface IBeacon {

		Proximity Proximity { get; }
		ushort Minor { get; }
		ushort Major { get; }
		string Uuid { get; }
		double Distance { get; set; }
    }
}

		// TODO: colour, distance, power?
        // TODO: readtemperatureasync
        // TODO: getbatterylevel and battery life remaining
