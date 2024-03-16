package com.danielkreitsch.backseatgaming.services

import com.aallam.openai.api.chat.*
import com.aallam.openai.api.http.Timeout
import com.aallam.openai.api.model.ModelId
import com.aallam.openai.client.OpenAI
import com.danielkreitsch.backseatgaming.configs.OpenAIConfig
import kotlin.time.Duration.Companion.seconds
import org.slf4j.LoggerFactory
import org.springframework.stereotype.Service

@Service
class OpenAIService(private val openAIConfig: OpenAIConfig) {
  private val logger = LoggerFactory.getLogger(javaClass)

  private val openAI: OpenAI by lazy {
    OpenAI(token = this.openAIConfig.token, timeout = Timeout(socket = 60.seconds))
  }

  private val model = ModelId("gpt-3.5-turbo")

  suspend fun generateAnswer(systemPrompt: String): String? {
    val messages = mutableListOf(ChatMessage(role = ChatRole.System, content = systemPrompt))
    val request = ChatCompletionRequest(model = model, messages = messages, maxTokens = 1000)
    val completion: ChatCompletion = openAI.chatCompletion(request)
    val response = completion.choices[0].message.content

    // Log token usage
    this.logger.info(
        "Token usage: ${completion.usage?.totalTokens} (Prompt: ${completion.usage?.promptTokens}, Completion: ${completion.usage?.completionTokens})")

    return response
  }
}
