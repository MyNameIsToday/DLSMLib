# DLSMLib

DLSMLib(Digital Logic Sim Modding Library) is a currently in-progress library for making BepnInEx mods for Digital Logic Sim and Digital Logic Sim: Community Edition. (It works on both!)

The project currently includes a method to add new chips, and some more convenient methods for pin creation and state modification.

### ⚠️ DLSMLib requires Bleeding Edge builds of BepInEx (Il2Cpp)

<br>

If you'd like to make a pull request, **push it towards the dev branch** rather than main.

<br>
<br>

### Planned Features:
- Helper methods for adding display chips.

- UI helper methods.

- Better access to systems such as wire drawing.

- Adding chips to collections


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


## Bugs
- When removing a chip or changing the name of one, the old version of the chip will still show up in the library. <br>
- Pin state changes can be stubborn sometimes.


###### Shhhh.... I forgot to remove the hard-coded chip size!
