import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

plugins {
  id("org.springframework.boot") version "3.1.5"
  id("io.spring.dependency-management") version "1.1.3"
  kotlin("jvm") version "1.8.22"
  kotlin("plugin.spring") version "1.8.22"
  id("maven-publish")
}

group = "com.danielkreitsch"

version = "0.0.1-SNAPSHOT"

java { sourceCompatibility = JavaVersion.VERSION_17 }

repositories { mavenCentral() }

dependencies {
  implementation("org.springframework.boot:spring-boot-starter-web")
  implementation("com.fasterxml.jackson.module:jackson-module-kotlin")
  implementation("org.jetbrains.kotlin:kotlin-reflect")
  implementation("org.jetbrains.kotlin:kotlin-stdlib-jdk8")
  implementation("org.jetbrains.kotlinx:kotlinx-coroutines-core:1.3.9")
  implementation("com.discord4j:discord4j-core:3.2.3")
  implementation("com.aallam.openai:openai-client:3.5.1")
  implementation("io.ktor:ktor-client-okhttp:2.0.3")
  testImplementation("org.springframework.boot:spring-boot-starter-test")
}

tasks {
  withType<KotlinCompile> {
    kotlinOptions {
      freeCompilerArgs += "-Xjsr305=strict"
      jvmTarget = "17"
    }
  }
  withType<Test> { useJUnitPlatform() }
  jar { enabled = false }
  named("build") { finalizedBy("copyFiles") }
  register<Copy>("copyFiles") {
    from("Dockerfile")
    from("build/libs")
    into("../../dist/apps/backend")
  }
}

springBoot {
  buildInfo()
  mainClass = "com.danielkreitsch.globalgamejam2024.BackendApplicationKt"
}

publishing {
  publications { create<MavenPublication>("mavenJava") { artifact(tasks.getByName("bootJar")) } }
}
