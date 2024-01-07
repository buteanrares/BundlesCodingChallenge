# Rares Butean - Solution for the Bundles Coding Challenge

## Problem rationalization:

To get the maximum numbers of top-level bundles (Bikes, in this example) that can be constructed, one must find the most scarce resource from the inventory according to the ratio. So, in this scenario:
- 1 Tube per Wheel, 2 Wheels per Bike => 2 Tubes per Bike (2:1 ratio)
- 35 Tubes in inventory
- => 35/2 = 17 Bikes

 ## Code solution:

The solution method `BundleService.MaxBundles` creates a `Stack` of the top-level bundle and traverses it, converting other bundles such as Wheel in spare parts. When it finds a leaf (spare part), it returns 0 if no parts are found, meaning that no top-level bundles can be constructed, or calculates the minimum of spare parts.

## Generic aspect

The solution is flexible. For example, we can add a `Handlebar` to the bundle structure and inventory in this way:
```
        // Inventory of parts
        var inventory = new List<Bundle>
        {
            new() { Name = "Seat", Count = 50 },
            new() { Name = "Pedal", Count = 60 },
            new() { Name = "Frame", Count = 60 },
            new() { Name = "Tube", Count = 35 },
            new() { Name = "Handlebar", Count = 5 } // <------- HERE
        };

        // Top-level bundle structure
        var bundle = new Bundle
        {
            Name = "MainBundle",
            Count = 1,
            Parts =
            [
                new Bundle { Name = "Seat", Count = 1 },
                new Bundle { Name = "Pedal", Count = 2 },
                new Bundle
                {
                    Name = "Wheel",
                    Count = 2,
                    Parts =
                    [
                        new Bundle { Name = "Frame", Count = 1 },
                        new Bundle { Name = "Tube", Count = 1 }
                    ]
                },
                new Bundle() { Name = "Handlebar", Count = 1 } // <------- HERE
            ]
        };
```
The output would be 5/1=5 Bikes, since we only have 5 Handlebars in a 1:1 ratio.

If we remove the Handlebars and increase the number of Frames per Wheel to 2, so:
```
                new Bundle
                {
                    Name = "Wheel",
                    Count = 2,
                    Parts =
                    [
                        new Bundle { Name = "Frame", Count = 2 }, // <------- HERE
                        new Bundle { Name = "Tube", Count = 1 }
                    ]
                },
```
The output would be 35/4=8, since the ratio of Frames per Bike is 4:1.

## EFCore configuration

The EFCore configuration can be found in `Data.BundleDbContext`. The configuration is straight forward.
I chose not to overcomplicate the solution by separating the model into BundleEntity (subtrees) and PartsEntity (leafs/spare parts) since no additional value would be gained.

## Diagram

With the model in mind, here is the database diagram:

![image](https://github.com/buteanrares/BundlesCodingChallenge/assets/59267118/94a22d80-e447-4f1d-8ace-cb8714ebd01f)

It has a `*...1` reference to itself (a bundle is composed of 1 or more other bundles).
Of course, the navigation properties (`Parts`, `ParentBundle`) are omitted.
