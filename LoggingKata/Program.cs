using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            string[] lines = File.ReadAllLines(csvPath);


            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            ITrackable[] locations = lines.Select(parser.Parse).ToArray();
            //SAME AS:    = lines.Select(x => parser.Parse(x)).ToArray();
            //SAME AS:    = lines.Select(line => parser.Parse(line)); //type is IEnumerable
            //SAME AS:
            //var tacoList = new List<ITrackable>();

            ////foreach (var line in lines)
            //   {
            //    tacoList.Add(parser.Parse(line));
            //   }

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;

            double distance = 0;

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;` <--google what properties there are

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            // Create a new corA Coordinate with your locA's lat and long
           
                
            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
            for (int i = 0; i < locations.Length; i++)
            {
                var locA = locations[i];
                GeoCoordinate corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude) ;

                for (int j = 0; j < locations.Length; j++)
                {
            // Create a new Coordinate with your locB's lat and long
                    var locB = locations[j];
                    GeoCoordinate corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude) ;

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above
                    
                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }

                }
            }
            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
            Console.WriteLine($"{tacoBell1.Name} and {tacoBell2.Name} are farthest apart.");
            Console.WriteLine($"The distance is: {distance} meters"); //in meters
            var metersToMiles = distance * .00062167;
            Console.WriteLine(Math.Round(metersToMiles, 2)); //rounds to two decimal points


            


        }
    }
}
