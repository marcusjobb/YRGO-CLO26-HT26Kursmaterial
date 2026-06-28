---

title: 1. Introduktion till Agila Metoder
author: Marcus Ackre Medina
type: lecture
topic: oop
difficulty: 1
language: sql
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/3_oop_adv/lectures/08_agile_development_kanban/1_example.md"
description: "// Example 1: Agile Task Management System with MySQL Database"
tags: ["agila", "git", "metoder", "oop", "till"]
week_fit: []
---

# 1. Introduktion till Agila Metoder

🟢


Complete example:

// Example 1: Agile Task Management System with MySQL Database
// Demonstrates agile workflow tracking for software development tasks

import java.sql.*;
import java.time.LocalDateTime;
import java.util.*;

public class AgileSoftwareTaskManager {
    private Connection conn;

    public AgileSoftwareTaskManager() {
        try {
            conn = DriverManager.getConnection("jdbc:mysql://localhost:3306/agile_tasks", "user", "password");
        } catch (SQLException e) {
            throw new RuntimeException("Database connection failed", e);
        }
    }

    public void createTask(String title, String status, String priority) {
        String sql = "INSERT INTO tasks (title, status, priority, created_at) VALUES (?, ?, ?, ?)";
        try (PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setString(1, title);
            pstmt.setString(2, status);
            pstmt.setString(3, priority);
            pstmt.setTimestamp(4, Timestamp.valueOf(LocalDateTime.now()));
            pstmt.executeUpdate();
        } catch (SQLException e) {
            throw new RuntimeException("Failed to create task", e);
        }
    }

    public void moveTask(int taskId, String newStatus) {
        String sql = "UPDATE tasks SET status = ? WHERE id = ?";
        try (PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setString(1, newStatus);
            pstmt.setInt(2, taskId);
            pstmt.executeUpdate();
        } catch (SQLException e) {
            throw new RuntimeException("Failed to update task status", e);
        }
    }

    public List<Task> getTasksByStatus(String status) {
        List<Task> tasks = new ArrayList<>();
        String sql = "SELECT * FROM tasks WHERE status = ?";
        try (PreparedStatement pstmt = conn.prepareStatement(sql)) {
            pstmt.setString(1, status);
            ResultSet rs = pstmt.executeQuery();
            while (rs.next()) {
                tasks.add(new Task(
                    rs.getInt("id"),
                    rs.getString("title"),
                    rs.getString("status"),
                    rs.getString("priority")
                ));
            }
        } catch (SQLException e) {
            throw new RuntimeException("Failed to fetch tasks", e);
        }
        return tasks;
    }
}

class Task {
    private int id;
    private String title;
    private String status;
    private String priority;

    public Task(int id, String title, String status, String priority) {
        this.id = id;
        this.title = title;
        this.status = status;
        this.priority = priority;
    }

    // Getters and setters
}

// Example 2: Agile Project Resource Management with MongoDB
// Demonstrates resource allocation and tracking in an agile environment

import com.mongodb.client.*;
import org.bson.Document;
import java.util.*;

public class AgileResourceManager {
    private MongoCollection<Document> collection;

    public AgileResourceManager() {
        MongoClient mongoClient = MongoClients.create("mongodb://localhost:27017");
        MongoDatabase database = mongoClient.getDatabase("agile_resources");
        collection = database.getCollection("team_resources");
    }

    public void allocateResource(String teamMember, String project, int capacityHours) {
        Document resource = new Document()
            .append("teamMember", teamMember)
            .append("project", project)
            .append("capacityHours", capacityHours)
            .append("allocatedTasks", new ArrayList<>())
            .append("lastUpdated", new Date());
            
        collection.insertOne(resource);
    }

    public void assignTask(String teamMember, String taskName, int estimatedHours) {
        Document query = new Document("teamMember", teamMember);
        Document task = new Document()
            .append("taskName", taskName)
            .append("estimatedHours", estimatedHours)
            .append("assignedDate", new Date());
            
        Document update = new Document("$push", new Document("allocatedTasks", task))
            .append("$set", new Document("lastUpdated", new Date()));
            
        collection.updateOne(query, update);
    }

    public List<Document> getTeamMemberWorkload(String teamMember) {
        Document query = new Document("teamMember", teamMember);
        FindIterable<Document> result = collection.find(query);
        List<Document> workload = new ArrayList<>();
        
        for (Document doc : result) {
            workload.add(doc);
        }
        return workload;
    }

    public void updateCapacity(String teamMember, int newCapacity) {
        Document query = new Document("teamMember", teamMember);
        Document update = new Document("$set", new Document("capacityHours", newCapacity)
            .append("lastUpdated", new Date()));
            
        collection.updateOne(query, update);
    }
}
