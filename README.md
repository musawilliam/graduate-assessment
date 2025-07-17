# King Shan's Journey Optimizer

This application helps King Shan choose the fastest route and vehicle combination to travel from Silk Dorb to Hallitharam, considering weather conditions and traffic speeds.

## Features
- Calculates optimal vehicle-orbit combination based on:
  - Weather conditions (Sunny, Rainy, Windy)
  - Orbit traffic speeds
  - Vehicle capabilities and constraints
- Handles tie-breakers according to specified priority rules
- Interactive mode for custom inputs
- Predefined test cases for verification

## Requirements
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Getting Started
1. Clone the repository
2. Navigate to project directory:
```bash
cd GraduateAssessment

## Run the application:
1. dotnet run

## Usage
Predefined Test Cases

The application automatically runs 3 test cases:

1. Sunny weather (Orbit1: 12 mm/h, Orbit2: 10 mm/h)

2. Windy weather (Orbit1: 14 mm/h, Orbit2: 20 mm/h)

4. Rainy weather (Orbit1: 15 mm/h, Orbit2: 18 mm/h)