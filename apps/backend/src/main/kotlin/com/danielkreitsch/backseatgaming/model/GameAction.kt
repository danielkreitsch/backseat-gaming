package com.danielkreitsch.backseatgaming.model

import com.fasterxml.jackson.annotation.JsonInclude

enum class GameActionType {
  NONE,
  LEFT,
  RIGHT,
  JUMP,
  WAIT,
  RESPOND,
  EXECUTE
}

@JsonInclude(JsonInclude.Include.NON_NULL)
data class GameAction(val type: GameActionType, val argument: String? = null)
