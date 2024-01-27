package com.danielkreitsch.globalgamejam2024.configs

import org.springframework.boot.context.properties.ConfigurationProperties
import org.springframework.context.annotation.Configuration

@Configuration
@ConfigurationProperties(prefix = "openai")
class OpenAIConfig
{
  var token: String = ""
  var prompts: Prompts = Prompts()

  class Prompts
  {
    var playerInputGenerator: String = ""
  }
}
