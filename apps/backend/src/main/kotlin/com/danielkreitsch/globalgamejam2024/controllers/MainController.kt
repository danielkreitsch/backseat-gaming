package com.danielkreitsch.globalgamejam2024.controllers

import com.danielkreitsch.globalgamejam2024.model.GameAction
import com.danielkreitsch.globalgamejam2024.services.PlayerInputGenerator
import kotlinx.coroutines.runBlocking
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/api")
class MainController
@Autowired
constructor(private val playerInputGenerator: PlayerInputGenerator) {

  @GetMapping("/generatePlayerInput")
  fun getGeneratedInput(@RequestParam request: String): ResponseEntity<List<GameAction>> =
      runBlocking {
        val response = playerInputGenerator.generateInput(request)
        ResponseEntity.ok(response)
      }
}
