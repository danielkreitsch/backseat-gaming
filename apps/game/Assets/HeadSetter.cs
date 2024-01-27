using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeadSetter : MonoBehaviour
{
  // Enums for each option
  public enum HatOptions
  {
    None,
    Beanie,
    Flowers,
    Ribbon,
    Crown,
    Hat,
    Chicken
  }

  public enum BlindfoldOptions
  {
    Blindfold,
    Pants
  }

  public enum MouthOptions
  {
    None,
    Mustache
  }

  // Public GameObject references for each option
  public GameObject HatNone, HatBeanie, HatFlowers, HatRibbon, HatCrown, HatHat, HatChicken;
  public GameObject BlindfoldBlindfold, BlindfoldPants;
  public GameObject MouthNone, MouthMustache;

  // Selected options
  private HatOptions selectedHat;
  private BlindfoldOptions selectedBlindfold;
  private MouthOptions selectedMouth;

  // Public properties to retrieve the selected options
  public HatOptions SelectedHat => selectedHat;
  public BlindfoldOptions SelectedBlindfold => selectedBlindfold;
  public MouthOptions SelectedMouth => selectedMouth;

  // Public method to set all layers
  public void SetRandomCustomization()
  {
    Debug.Log("Setting random customization");

    selectedBlindfold = RandomizeOption<BlindfoldOptions>(new[] { 11, 1 });
    selectedHat = selectedBlindfold == BlindfoldOptions.Pants
      ? HatOptions.None
      : RandomizeOption<HatOptions>(new[] { 12, 12, 6, 6, 5, 3, 1 });
    selectedMouth = RandomizeOption<MouthOptions>(new[] { 5, 1 });

    UpdateCustomization();

    Debug.Log("Selected hat: " + selectedHat + ", selected blindfold: " + selectedBlindfold + ", selected mouth: " + selectedMouth);
  }

  // Method to update the game objects based on selected options
  void UpdateCustomization()
  {
    // Deactivate all game objects
    DeactivateAll();

    // Activate selected options
    ActivateOption(new GameObject[] { HatNone, HatBeanie, HatFlowers, HatRibbon, HatCrown, HatHat, HatChicken }, (int)selectedHat);
    ActivateOption(new GameObject[] { BlindfoldBlindfold, BlindfoldPants }, (int)selectedBlindfold);
    ActivateOption(new GameObject[] { MouthNone, MouthMustache }, (int)selectedMouth);
  }

  // Method to deactivate all game objects
  void DeactivateAll()
  {
    GameObject[] allOptions = { HatNone, HatBeanie, HatFlowers, HatRibbon, HatCrown, HatHat, HatChicken, BlindfoldBlindfold, BlindfoldPants, MouthNone, MouthMustache };
    foreach (var obj in allOptions)
    {
      if (obj != null)
        obj.SetActive(false);
    }
  }

  // Method to activate the selected game object
  void ActivateOption(GameObject[] options, int selectedIndex)
  {
    if (selectedIndex >= 0 && selectedIndex < options.Length)
      if (options[selectedIndex] != null)
        options[selectedIndex].SetActive(true);
  }

  // Method to randomize options based on given rarities
  private T RandomizeOption<T>(int[] rarities) where T : Enum
  {
    int total = 0;
    foreach (int rarity in rarities)
    {
      total += rarity;
    }

    int randomValue = Random.Range(0, total);
    T[] values = (T[])System.Enum.GetValues(typeof(T));

    foreach (T value in values)
    {
      int rarity = rarities[(int)(object)value];
      if (randomValue < rarity)
        return value;
      randomValue -= rarity;
    }

    return default(T);
  }
}
