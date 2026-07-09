# 1. Introduktion till Docker och containrar

🟢


Complete example:

```java
// Example 1: Spring Boot Weather Service with Docker
// This example demonstrates how to containerize a Spring Boot REST API that provides
// weather information. It includes a complete Spring Boot application with a REST
// controller and a multi-stage Dockerfile following best practices.

// WeatherServiceApplication.java
package com.example.weatherservice;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class WeatherServiceApplication {
    public static void main(String[] args) {
        SpringApplication.run(WeatherServiceApplication.class, args);
    }
}

// Weather.java
package com.example.weatherservice.model;

public class Weather {
    private String city;
    private double temperature;
    private String description;

    public Weather() {}

    public Weather(String city, double temperature, String description) {
        this.city = city;
        this.temperature = temperature;
        this.description = description;
    }

    // Getters and setters
    public String getCity() { return city; }
    public void setCity(String city) { this.city = city; }

    public double getTemperature() { return temperature; }
    public void setTemperature(double temperature) { this.temperature = temperature; }

    public String getDescription() { return description; }
    public void setDescription(String description) { this.description = description; }
}

// WeatherController.java
package com.example.weatherservice.controller;

import com.example.weatherservice.model.Weather;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api/weather")
public class WeatherController {

    private final Map<String, Weather> weatherData = new HashMap<>();

    public WeatherController() {
        // Initiera with some sample data
        weatherData.put("stockholm", new Weather("Stockholm", 15.5, "Partly cloudy"));
        weatherData.put("gothenburg", new Weather("Gothenburg", 14.2, "Rainy"));
        weatherData.put("malmo", new Weather("Malmö", 16.8, "Sunny"));
    }

    @GetMapping("/{city}")
    public Weather getWeather(@PathVariable String city) {
        return weatherData.getOrDefault(city.toLowerCase(),
               new Weather(city, 0, "Data not available"));
    }
}
```

```xml
// pom.xml
<?xml version="1.0" encoding="UTF-8"?>
<project xmlns="http://maven.apache.org/POM/4.0.0"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
         xsi:schemaLocation="http://maven.apache.org/POM/4.0.0
                            https://maven.apache.org/xsd/maven-4.0.0.xsd">
    <modelVersion>4.0.0</modelVersion>

    <parent>
        <groupId>org.springframework.boot</groupId>
        <artifactId>spring-boot-starter-parent</artifactId>
        <version>3.1.0</version>
    </parent>

    <groupId>com.example</groupId>
    <artifactId>weather-service</artifactId>
    <version>1.0.0</version>
    <name>Weather Service</name>
    <description>Spring Boot Weather Service API</description>

    <properties>
        <java.version>17</java.version>
    </properties>

    <dependencies>
        <dependency>
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-starter-web</artifactId>
        </dependency>
        <dependency>
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-starter-actuator</artifactId>
        </dependency>
        <dependency>
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-starter-test</artifactId>
            <scope>test</scope>
        </dependency>
    </dependencies>

    <build>
        <plugins>
            <plugin>
                <groupId>org.springframework.boot</groupId>
                <artifactId>spring-boot-maven-plugin</artifactId>
            </plugin>
        </plugins>
    </build>
</project>
```

```dockerfile
// Dockerfile

## Multi-stage Dockerfile for Spring Boot Weather Service

## Build stage

FROM maven:3.8.4-openjdk-17 AS build
WORKDIR /app
COPY pom.xml .

## Download dependencies separately to leverage Docker cache

RUN mvn dependency:go-offline -B
COPY src src
RUN mvn package -DskipTests

## Run stage

FROM amazon-corretto:17-alpine
WORKDIR /app

## Create a non-root user to run the application

RUN addgroup -S appgroup && adduser -S appuser -G appgroup
USER appuser

## Copy the JAR from the build stage

COPY --from=build /app/target/*.jar app.jar

## Add health check

HEALTHCHECK --interval=30s --timeout=3s --retries=3 \
  CMD wget -q --spider <http://localhost:8080/actuator/health> || exit 1

## Expose the application port

EXPOSE 8080

## Set JVM options for containers

ENTRYPOINT ["java", "-XX:+UseContainerSupport", "-XX:MaxRAMPercentage=75.0", "-jar", "app.jar"]
```

```python
# Example 2: Document Processing Microservice with Docker
# This example demonstrates a containerized Python microservice that processes
# document files, demonstrating Docker's versatility beyond Java applications.

# app.py
from flask import Flask, request, jsonify
import os
import uuid
from werkzeug.utils import secure_filename
import pandas as pd
import time

app = Flask(__name__)

## Configure upload folder

UPLOAD_FOLDER = '/app/uploads'
PROCESSED_FOLDER = '/app/processed'
ALLOWED_EXTENSIONS = {'csv', 'xlsx', 'xls'}

os.makedirs(UPLOAD_FOLDER, exist_ok=True)
os.makedirs(PROCESSED_FOLDER, exist_ok=True)

app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER
app.config['MAX_CONTENT_LENGTH'] = 16 *1024* 1024  # 16MB max upload

def allowed_file(filename):
    return '.' in filename and \
           filename.rsplit['.', 1](1).lower() in ALLOWED_EXTENSIONS

@app.route('/health')
def health_check():
    return jsonify({"status": "healthy"}), 200

@app.route('/api/documents/upload', methods=['POST'])
def upload_document():
    if 'file' not in request.files:
        return jsonify({"error": "No file part"}), 400

    file = request.files['file']

    if file.filename == '':
        return jsonify({"error": "No selected file"}), 400

    if file and allowed_file(file.filename):
        filename = secure_filename(file.filename)
        unique_filename = f"{uuid.uuid4()}_{filename}"
        file_path = os.path.join(app.config['UPLOAD_FOLDER'], unique_filename)
        file.save(file_path)

        # Submit for processing (in real app, this would be asynchronous)
        process_id = str(uuid.uuid4())

        return jsonify({
            "message": "File uploaded successfully",
            "processId": process_id,
            "filename": filename
        }), 202

    return jsonify({"error": "File type not allowed"}), 400

@app.route('/api/documents/process/<process_id>', methods=['GET'])
def get_processing_status(process_id):
    # Simulate processing status
    # In a real app, this would check an actual status
    statuses = ["PENDING", "PROCESSING", "COMPLETED", "FAILED"]
    import random
    status = statuses[random.randint(0, 3)]

    return jsonify({
        "processId": process_id,
        "status": status,
        "progress": random.randint(0, 100) if status == "PROCESSING" else
                    100 if status == "COMPLETED" else 0
    })

@app.route('/api/documents/statistics', methods=['GET'])
def get_statistics():
    # Count files in both directories
    upload_count = len([f for f in os.listdir(UPLOAD_FOLDER) if os.path.isfile(os.path.join(UPLOAD_FOLDER, f))])
    processed_count = len([f for f in os.listdir(PROCESSED_FOLDER) if os.path.isfile(os.path.join(PROCESSED_FOLDER, f))])

    return jsonify({
        "uploadedDocuments": upload_count,
        "processedDocuments": processed_count,
        "serverTime": time.time()
    })

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
```

```txt
# requirements.txt
flask==2.0.1
pandas==1.3.3
openpyxl==3.0.9
werkzeug==2.0.1
numpy==1.21.2
gunicorn==20.1.0
```

```dockerfile
# Dockerfile

## Multi-stage Dockerfile for Python Document Processing Service

## Build stage

FROM python:3.9-slim AS build

WORKDIR /app

## Install build dependencies

RUN apt-get update && \
    apt-get install -y --no-install-recommends gcc python3-dev && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

## Install Python dependencies

COPY requirements.txt .
RUN pip wheel --no-cache-dir --wheel-dir /app/wheels -r requirements.txt

## Final stage

FROM python:3.9-slim

WORKDIR /app

## Create non-root user

RUN groupadd -r appuser && useradd -r -g appuser appuser

## Copy wheels from build stage

COPY --from=build /app/wheels /wheels
RUN pip install --no-cache-dir /wheels/* && \
    rm -rf /wheels

## Copy application code

COPY app.py .

## Create directories and set permissions

RUN mkdir -p /app/uploads /app/processed && \
    chown -R appuser:appuser /app

## Switch to non-root user

USER appuser

## Add health check

HEALTHCHECK --interval=30s --timeout=3s --retries=3 \
  CMD curl -f <http://localhost:5000/health> || exit 1

## Expose port

EXPOSE 5000

## Run with Gunicorn

CMD ["gunicorn", "--bind", "0.0.0.0:5000", "app:app"]
```
