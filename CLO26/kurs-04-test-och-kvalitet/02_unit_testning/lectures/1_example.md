# 1. Introduktion till Testdriven utveckling (TDD)

🟢


Complete example:

```csharp
// Example 1: Implement a simple calculator using TDD
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class SimpleCalculatorTest {
    @Test
    void shouldAddTwoNumbers() {
        SimpleCalculator calculator = new SimpleCalculator();
        assertEquals(5, calculator.add(2, 3));
    }
}

public class SimpleCalculator {
    public int add(int a, int b) {
        return a + b;
    }
}
```

```csharp
// Example 2: Implement a user authentication system using TDD
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class UserAuthenticatorTest {
    @Test
    void shouldAuthenticateValidUser() {
        UserAuthenticator authenticator = new UserAuthenticator();
        assertTrue(authenticator.authenticate("validUser", "correctPassword"));
    }

    @Test
    void shouldRejectInvalidUser() {
        UserAuthenticator authenticator = new UserAuthenticator();
        assertFalse(authenticator.authenticate("invalidUser", "anyPassword"));
    }
}

public class UserAuthenticator {
    public boolean authenticate(String username, String password) {
        // Simplified authentication logic
        return "validUser".equals(username) && "correctPassword".equals(password);
    }
}
```

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
