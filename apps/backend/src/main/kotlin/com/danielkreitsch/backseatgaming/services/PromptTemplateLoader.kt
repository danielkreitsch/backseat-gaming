package com.danielkreitsch.backseatgaming.services

import com.danielkreitsch.backseatgaming.model.PromptTemplate
import org.springframework.core.io.*
import org.springframework.stereotype.Service
import java.io.*
import java.nio.charset.StandardCharsets
import java.util.stream.Collectors

@Service
class PromptTemplateLoader(
  private val resourceLoader: ResourceLoader
) {
  fun loadPromptTemplate(name: String): PromptTemplate {
    var content = ""

    val resource: Resource = resourceLoader.getResource("classpath:prompts/$name.txt")
    resource.inputStream.use { inputStream ->
      BufferedReader(InputStreamReader(inputStream, StandardCharsets.UTF_8)).use { reader ->
        content = reader.lines().collect(Collectors.joining("\n"))
      }
    }
    content = content.replace("\r", "")
    if (content.endsWith("\n")) {
      content = content.substring(0, content.length - 1)
    }

    return PromptTemplate(content)
  }
}
