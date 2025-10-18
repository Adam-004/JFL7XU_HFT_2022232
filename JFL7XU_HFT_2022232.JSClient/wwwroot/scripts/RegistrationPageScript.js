document.getElementById("registerForm").addEventListener("submit", async e => {
    e.preventDefault();
    const username = document.getElementById("username").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const res = await fetch("http://localhost:40567/UserAuthentication/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, email, password }),
        credentials: "include"
    });

    const msgEl = document.getElementById("message");
    if (res.ok) {
        msgEl.textContent = "✅ Registration successful!";
        msgEl.classList.replace("text-danger", "text-success");
        setTimeout(() => window.location = "LoginPage.html", 1000);
    } else {
        const data = await res.json();
        msgEl.textContent = data.message || "❌ Registration failed";
    }
});