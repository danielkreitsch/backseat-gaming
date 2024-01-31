package com.danielkreitsch.backseatgaming.controllers

import com.danielkreitsch.backseatgaming.model.GameAction
import com.danielkreitsch.backseatgaming.services.PlayerInputGenerator
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
