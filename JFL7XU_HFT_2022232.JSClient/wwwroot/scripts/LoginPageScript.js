document.getElementById("loginForm").addEventListener("submit", async e => {
    e.preventDefault();

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    try {
        const res = await fetch("http://localhost:40567/UserAuthentication/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        });

        const msgEl = document.getElementById("message");

        if (res.ok) {
            const data = await res.json();

            if (data.token) {
                localStorage.setItem("jwtToken", data.token);
            }

            msgEl.textContent = "✅ Login successful!";
            msgEl.classList.replace("text-danger", "text-success");

            setTimeout(() => window.location = "MainMenuPage.html", 1000);
        } else {
            const data = await res.json();
            msgEl.textContent = data.message || "❌ Login failed";
            msgEl.classList.replace("text-success", "text-danger");
        }
    } catch (err) {
        console.error(err);
        const msgEl = document.getElementById("message");
        msgEl.textContent = "❌ Login failed (network error)";
    }
});