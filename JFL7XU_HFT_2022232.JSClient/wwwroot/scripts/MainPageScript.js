let hangars = [];
let hangarCounter = 0;
let owners = [];
let ownerCounter = 0;
let starships = [];
let starshipCounter = 0;

checkAuth();
getAllRecords();

async function getAllRecords() {
    const token = localStorage.getItem("jwtToken");
    const loggedInControls = document.getElementById("LoggedInControls");

    if (!token) {
        loggedInControls.style.display = "none";
        return;
    } else {
        loggedInControls.style.display = "block";
    }

    try {
        const ownersRes = await fetch('http://localhost:40567/Owner', {
            headers: { "Authorization": `Bearer ${token}` }
        });
        owners = ownersRes.ok ? await ownersRes.json() : [];
        ownerCounter = owners.length;

        const hangarsRes = await fetch('http://localhost:40567/Hangar', {
            headers: { "Authorization": `Bearer ${token}` }
        });
        hangars = hangarsRes.ok ? await hangarsRes.json() : [];
        hangarCounter = hangars.length;

        const starshipsRes = await fetch('http://localhost:40567/Starship', {
            headers: { "Authorization": `Bearer ${token}` }
        });
        starships = starshipsRes.ok ? await starshipsRes.json() : [];
        starshipCounter = starships.length;

        displayRecords();
    } catch (err) {
        console.error("Error fetching records:", err);
    }
}

function displayRecords() {
    document.getElementById('OwnerDisplay').textContent = ownerCounter + " records.";
    document.getElementById('HangarDisplay').textContent = hangarCounter + " records.";
    document.getElementById('StarshipDisplay').textContent = starshipCounter + " records.";
}

async function checkAuth() {
    const authSection = document.getElementById("AuthSection");
    const loggedInControls = document.getElementById("LoggedInControls");
    const token = localStorage.getItem("jwtToken");

    if (!token) {
        loggedInControls.style.display = "none";
        authSection.innerHTML = `
            <button class="AuthButton" onclick="window.location.href='LoginPage.html'">Login</button>
            <button class="AuthButton" onclick="window.location.href='RegistrationPage.html'">Register</button>
        `;
        return;
    }

    try {
        const res = await fetch("http://localhost:40567/UserAuthentication/me", {
            headers: { "Authorization": `Bearer ${token}` }
        });

        if (!res.ok) throw new Error("Token invalid or expired");

        const user = await res.json();
        loggedInControls.style.display = "block";
        authSection.innerHTML = `
            <span>Welcome, <b>${user.userName}</b></span>
            <button class="AuthButton" onclick="logout()">Logout</button>
        `;

    } catch (err) {
        console.error("Auth check failed:", err);
        localStorage.removeItem("jwtToken");
        loggedInControls.style.display = "none";
        authSection.innerHTML = `
            <button class="AuthButton" onclick="window.location.href='LoginPage.html'">Login</button>
            <button class="AuthButton" onclick="window.location.href='RegistrationPage.html'">Register</button>
        `;
    }
}

function logout() {
    localStorage.removeItem("jwtToken");
    window.location.reload();
}