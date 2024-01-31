package com.danielkreitsch.backseatgaming.services

import com.danielkreitsch.backseatgaming.model.*
import org.slf4j.LoggerFactory
import org.springframework.stereotype.Service

@Service
class PlayerInputGenerator(
    private val promptTemplateLoader: PromptTemplateLoader,
    private val openAIService: OpenAIService,
) {
  private val logger = LoggerFactory.getLogger(javaClass)

  private val promptTemplate: PromptTemplate

  init {
    promptTemplate = promptTemplateLoader.loadPromptTemplate("player-input-generator")
  }

  /**
   * Actions:
   * {left,<time>} = move left
   * {right,<time>} = move right
   * {jump,<force>} = jump
   * {wait,<time>} = wait
   * {say,<text>} = say something to the user
   * {execute,<function>} = execute a function (see below)
   *
   * Functions:
   * slowMotion() = slow down the game for 3 seconds
   * oneLeg() = lift one leg
   * twoLegs() = use two legs again
   */
  suspend fun generateInput(request: String): List<GameAction> {
    val prompt =
        promptTemplate
            .replace("request", request.replace('\n', ' '))
            .replace("facing", "left")
            .prompt
    logger.info("Generating player input\nPrompt: $prompt")
    val response =
        openAIService.generateAnswer(prompt)
            ?: throw Exception("Error generating player input (GPT response: null)")
    logger.info("Response: $response")
    try {
      return parseGameCommands(response)
    } catch (ex: Exception) {
      throw Exception(
          "Error generating player input (GPT response: " + response.replace("\n", "\\n") + ")")
    }
  }

  fun parseGameCommands(commands: String): List<GameAction> {
    val gameActions = mutableListOf<GameAction>()
    val commandPattern = Regex("\\{\\s*([^,]+?)\\s*,\\s*([^}]+?)\\s*\\}")
    val commandMatches = commandPattern.findAll(commands)
    if (commandMatches.count() == 0) {
      return listOf(GameAction(GameActionType.RESPOND, commands))
    }
    commandMatches.forEach { matchResult ->
      val (action, argument) = matchResult.destructured
      val actionType = when (action.lowercase()) {
        "left" -> GameActionType.LEFT
        "right" -> GameActionType.RIGHT
        "jump" -> GameActionType.JUMP
        "wait" -> GameActionType.WAIT
        "respond" -> GameActionType.RESPOND
        "execute" -> GameActionType.EXECUTE
        else -> GameActionType.NONE
      }
      val gameAction = GameAction(type = actionType, argument = argument.removeSurrounding("\""))
      gameActions.add(gameAction)
    }
    return gameActions
  }
}
