# King Shan's Journey Optimizer

## Overview
**King Shan's Journey Optimizer** is a .NET console application that calculates the fastest route for King Shan to travel from Silk Dorb to Hallitharam, considering different weather conditions, traffic speed, and vehicle types. The program calculates and outputs the optimal vehicle-orbit combination for each journey based on input criteria.

## Features
- **Weather-Based Adjustments**: Adjusts the number of craters on each orbit based on the weather (Sunny, Rainy, Windy).
- **Optimal Journey Calculation**: Determines the best vehicle-orbit combination for the fastest journey considering traffic speed, vehicle speed, and crater-crossing time.
- **Interactive Mode**: Allows users to input custom weather and traffic speeds to calculate the optimal journey.
- **Predefined Test Cases**: Automatically runs three test cases with predefined weather and traffic speeds to verify correct functionality.

## Weather Conditions
- **Sunny**: Reduces craters by 10% (All vehicles are allowed).
- **Rainy**: Increases craters by 20% (Car and TukTuk only).
- **Windy**: No change to the craters (All vehicles are allowed).

## Vehicle Types
- **Bike**: 10 megamiles/hour, 2 minutes per crater.
- **TukTuk**: 12 megamiles/hour, 1 minute per crater.
- **Car**: 20 megamiles/hour, 3 minutes per crater.

## Installation

### Requirements
- .NET 9.0 SDK [Download .NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Getting Started
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/King-Shans-Journey-Optimizer.git
