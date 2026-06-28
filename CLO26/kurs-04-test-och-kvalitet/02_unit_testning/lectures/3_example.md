---

title: 3. SOLID-principerna och testbarhet
author: Marcus Ackre Medina
type: lecture
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/4_test/lectures/03_tdd/3_example.md"
description: "// Example 1: EmailService with SOLID principles and TDD"
tags: ["csharp", "solid-principerna", "testbarhet", "testing"]
week_fit: []
---

# 3. SOLID-principerna och testbarhet

🟢


Complete example:

```csharp
// Example 1: EmailService with SOLID principles and TDD
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;

public class EmailServiceTest {
    @Test
    void shouldSendEmail() {
        EmailValidator validator = mock(EmailValidator.class);
        EmailSender sender = mock(EmailSender.class);
        EmailService emailService = new EmailService(validator, sender);

        Email email = new Email("test@example.com", "Test Subject", "Test Body");
        when(validator.isValid(email)).thenReturn(true);
        when(sender.send(email)).thenReturn(true);

        assertTrue(emailService.sendEmail(email));
        verify(validator).isValid(email);
        verify(sender).send(email);
    }
}

public class EmailService {
    private EmailValidator validator;
    private EmailSender sender;

    public EmailService(EmailValidator validator, EmailSender sender) {
        this.validator = validator;
        this.sender = sender;
    }

    public boolean sendEmail(Email email) {
        if (validator.isValid(email)) {
            return sender.send(email);
        }
        return false;
    }
}

public interface EmailValidator {
    boolean isValid(Email email);
}

public interface EmailSender {
    boolean send(Email email);
}

public class Email {
    private String to;
    private String subject;
    private String body;

    public Email(String to, String subject, String body) {
        this.to = to;
        this.subject = subject;
        this.body = body;
    }
}
```

```csharp
// Example 2: TaskManager with SOLID principles and TDD
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;
import java.util.List;
import java.util.ArrayList;

public class TaskManagerTest {
    @Test
    void shouldAddTask() {
        TaskRepository repository = mock(TaskRepository.class);
        NotificationService notifier = mock(NotificationService.class);
        TaskManager taskManager = new TaskManager(repository, notifier);

        Task task = new Task("1", "Test Task", "High");
        when(repository.save(task)).thenReturn(true);

        assertTrue(taskManager.addTask(task));
        verify(repository).save(task);
        verify(notifier).notify("New task added: Test Task");
    }
}

public class TaskManager {
    private TaskRepository repository;
    private NotificationService notifier;

    public TaskManager(TaskRepository repository, NotificationService notifier) {
        this.repository = repository;
        this.notifier = notifier;
    }

    public boolean addTask(Task task) {
        if (repository.save(task)) {
            notifier.notify("New task added: " + task.getTitle());
            return true;
        }
        return false;
    }
}

public interface TaskRepository {
    boolean save(Task task);
}

public interface NotificationService {
    void notify(String message);
}

public class Task {
    private String id;
    private String title;
    private String priority;

    public Task(String id, String title, String priority) {
        this.id = id;
        this.title = title;
        this.priority = priority;
    }

    public String getTitle() {
        return title;
    }
}
```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
