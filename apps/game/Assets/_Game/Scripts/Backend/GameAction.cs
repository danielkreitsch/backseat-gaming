using System;

namespace Project
{
  public enum GameActionType { None, Left, Right, Jump, Wait, Respond, Execute }

  [Serializable]
  public class GameAction {
    public GameActionType type;
    public string argument;
  }
}
