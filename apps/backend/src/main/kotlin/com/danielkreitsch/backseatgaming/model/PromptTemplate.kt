package com.danielkreitsch.backseatgaming.model

data class PromptTemplate(val prompt: String) {
  fun replace(name: String, value: String): PromptTemplate {
    return PromptTemplate(prompt.replace("{$name}", value))
  }
}
