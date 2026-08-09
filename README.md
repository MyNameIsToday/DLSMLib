# DLSMLib

DLSMLib(Digital Logic Sim Modding Library) is a currently in-progress tool for making BepnInEx mods for Digital Logic Sim and Digital Logic Sim: Community Edition.

The project currently includes a method to add new chips, and some more convenient methods for pin creation.



### Planned Features:
-Helper methods for adding display chips.

-UI helper methods.

-Better access to systems such as wire drawing.

-Adding chips to collections


## Creating A Chip
Chip creation is actually quite simple. You just call this method:

```
ChipRegistry.AddChip(
  "NEW CHIP TEST",

  "new_chip_test",

  Color.white,

  new ModdedPinIdentity[]
  {
      Pins.AddPin("IN TEST", 0, PinType.Input, 1),

      Pins.AddPin("IN TEST", 1, PinType.Input, 8),

      Pins.AddPin("IN TEST", 2, PinType.Input, 8),

      Pins.AddPin("OUT TEST", 3, PinType.Output, 8)
  },

  chip =>
  {
      var value = new PinStateValue();

      if (chip.InputPins[0].FirstBitHigh)
      {
            chip.OutputPins[0].State = chip.InputPins[1].State;
      }
      else
      {
          chip.OutputPins[0].State = chip.InputPins[2].State;
      }
    }
);
```

And a chip should be created!
