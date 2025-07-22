using System;
using System.Collections.Generic;
using System.Linq;

namespace GraduateAssessment
{
    // Enums for better type safety
    public enum WeatherCondition
    {
        Sunny,
        Rainy,
        Windy
    }

    public enum VehicleType
    {
        Bike,
        TukTuk,
        Car
    }

    // Vehicle class representing each vehicle option
    public class Vehicle
    {
        public VehicleType Type { get; }
        public int MaxSpeed { get; }
        public int CraterCrossingTime { get; }

        public Vehicle(VehicleType type, int maxSpeed, int craterCrossingTime)
        {
            Type = type;
            MaxSpeed = maxSpeed;
            CraterCrossingTime = craterCrossingTime;
        }

        public bool CanOperateInWeather(WeatherCondition weather)
        {
            return weather switch
            {
                WeatherCondition.Sunny => true,
                WeatherCondition.Rainy => Type == VehicleType.Car || Type == VehicleType.TukTuk,
                WeatherCondition.Windy => true,
                _ => false
            };
        }
    }

    // Orbit class representing each orbit option
    public class Orbit
    {
        public int Number { get; }
        public int Distance { get; }
        public int BaseCraters { get; }

        public Orbit(int number, int distance, int baseCraters)
        {
            Number = number;
            Distance = distance;
            BaseCraters = baseCraters;
        }

        public int GetAdjustedCraters(WeatherCondition weather)
        {
            return weather switch
            {
                WeatherCondition.Sunny => (int)(BaseCraters * 0.9),
                WeatherCondition.Rainy => (int)(BaseCraters * 1.2),
                WeatherCondition.Windy => BaseCraters,
                _ => BaseCraters
            };
        }
    }

    // Journey result class
    public class JourneyResult
    {
        public Vehicle Vehicle { get; }
        public Orbit Orbit { get; }
        public double TotalTimeMinutes { get; }

        public JourneyResult(Vehicle vehicle, Orbit orbit, double totalTimeMinutes)
        {
            Vehicle = vehicle;
            Orbit = orbit;
            TotalTimeMinutes = totalTimeMinutes;
        }

        public override string ToString()
        {
            return $"Vehicle {Vehicle.Type} on Orbit{Orbit.Number}";
        }
    }

    // Main journey calculator class
    public class JourneyCalculator
    {
        private readonly List<Vehicle> _vehicles;
        private readonly List<Orbit> _orbits;

        public JourneyCalculator()
        {
            _vehicles = new List<Vehicle>
            {
                new Vehicle(VehicleType.Bike, 10, 2),
                new Vehicle(VehicleType.TukTuk, 12, 1),
                new Vehicle(VehicleType.Car, 20, 3)
            };

            _orbits = new List<Orbit>
            {
                new Orbit(1, 18, 20),
                new Orbit(2, 20, 10)
            };
        }

        public double CalculateJourneyTime(Vehicle vehicle, Orbit orbit, WeatherCondition weather, int trafficSpeed)
        {
            // Vehicle cannot exceed traffic speed
            int effectiveSpeed = Math.Min(vehicle.MaxSpeed, trafficSpeed);

            // Calculate travel time in minutes
            double travelTimeMinutes = (double)orbit.Distance / effectiveSpeed * 60;

            // Calculate crater crossing time
            int adjustedCraters = orbit.GetAdjustedCraters(weather);
            double craterTimeMinutes = adjustedCraters * vehicle.CraterCrossingTime;

            return travelTimeMinutes + craterTimeMinutes;
        }

        public JourneyResult FindOptimalJourney(WeatherCondition weather, int orbit1TrafficSpeed, int orbit2TrafficSpeed)
        {
            var trafficSpeeds = new Dictionary<int, int>
            {
                { 1, orbit1TrafficSpeed },
                { 2, orbit2TrafficSpeed }
            };

            var validCombinations = new List<JourneyResult>();

            foreach (var orbit in _orbits)
            {
                foreach (var vehicle in _vehicles)
                {
                    // Check if vehicle can operate in current weather
                    if (!vehicle.CanOperateInWeather(weather))
                        continue;

                    var trafficSpeed = trafficSpeeds[orbit.Number];
                    var totalTime = CalculateJourneyTime(vehicle, orbit, weather, trafficSpeed);

                    validCombinations.Add(new JourneyResult(vehicle, orbit, totalTime));
                }
            }

            // Find minimum time
            var minTime = validCombinations.Min(c => c.TotalTimeMinutes);

            // Get all combinations with minimum time
            var optimalCombinations = validCombinations
                .Where(c => Math.Abs(c.TotalTimeMinutes - minTime) < 0.001)
                .ToList();

            // If tie, prioritize: bike, tuktuk, car
            var priorityOrder = new[] { VehicleType.Bike, VehicleType.TukTuk, VehicleType.Car };

            foreach (var vehicleType in priorityOrder)
            {
                var result = optimalCombinations.FirstOrDefault(c => c.Vehicle.Type == vehicleType);
                if (result != null)
                    return result;
            }

            return optimalCombinations.First();
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new JourneyCalculator();

            // Test Case 1: Sunny weather
            Console.WriteLine("=== Test Case 1: Sunny Weather ===");
            Console.WriteLine("Input: Weather is Sunny");
            Console.WriteLine("Input: Orbit1 traffic speed is 12 megamiles/hour");
            Console.WriteLine("Input: Orbit2 traffic speed is 10 megamiles/hour");

            var result1 = calculator.FindOptimalJourney(WeatherCondition.Sunny, 12, 10);
            Console.WriteLine($"Expected Output: {result1}");
            Console.WriteLine($"Total time: {result1.TotalTimeMinutes:F2} minutes");
            Console.WriteLine();

            // Test Case 2: Windy weather
            Console.WriteLine("=== Test Case 2: Windy Weather ===");
            Console.WriteLine("Input: Weather is Windy");
            Console.WriteLine("Input: Orbit1 traffic speed is 14 megamiles/hour");
            Console.WriteLine("Input: Orbit2 traffic speed is 20 megamiles/hour");

            var result2 = calculator.FindOptimalJourney(WeatherCondition.Windy, 14, 20);
            Console.WriteLine($"Expected Output: {result2}");
            Console.WriteLine($"Total time: {result2.TotalTimeMinutes:F2} minutes");
            Console.WriteLine();

            // Additional test case: Rainy weather
            Console.WriteLine("=== Test Case 3: Rainy Weather ===");
            Console.WriteLine("Input: Weather is Rainy");
            Console.WriteLine("Input: Orbit1 traffic speed is 15 megamiles/hour");
            Console.WriteLine("Input: Orbit2 traffic speed is 18 megamiles/hour");

            var result3 = calculator.FindOptimalJourney(WeatherCondition.Rainy, 15, 18);
            Console.WriteLine($"Expected Output: {result3}");
            Console.WriteLine($"Total time: {result3.TotalTimeMinutes:F2} minutes");
            Console.WriteLine();

            // Interactive mode
            Console.WriteLine("=== Interactive Mode ===");
            RunInteractiveMode(calculator);
        }

        static void RunInteractiveMode(JourneyCalculator calculator)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter weather condition (Sunny/Rainy/Windy) or 'quit' to exit:");
                    var weatherInput = Console.ReadLine()?.Trim();

                    if (weatherInput?.ToLower() == "quit")
                        break;

                    if (!Enum.TryParse<WeatherCondition>(weatherInput, true, out var weather))
                    {
                        Console.WriteLine("Invalid weather condition. Please enter Sunny, Rainy, or Windy.");
                        continue;
                    }

                    Console.WriteLine("Enter Orbit1 traffic speed (megamiles/hour):");
                    if (!int.TryParse(Console.ReadLine(), out var orbit1Speed) || orbit1Speed <= 0)
                    {
                        Console.WriteLine("Invalid speed. Please enter a positive integer.");
                        continue;
                    }

                    Console.WriteLine("Enter Orbit2 traffic speed (megamiles/hour):");
                    if (!int.TryParse(Console.ReadLine(), out var orbit2Speed) || orbit2Speed <= 0)
                    {
                        Console.WriteLine("Invalid speed. Please enter a positive integer.");
                        continue;
                    }

                    var result = calculator.FindOptimalJourney(weather, orbit1Speed, orbit2Speed);
                    Console.WriteLine($"\nOptimal choice: {result}");
                    Console.WriteLine($"Total journey time: {result.TotalTimeMinutes:F2} minutes");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}