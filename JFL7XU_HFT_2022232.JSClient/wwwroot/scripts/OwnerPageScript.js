let owners = [];
let connection = null;

const oIdElement = document.getElementById('ownerID');
const oNameElement = document.getElementById('ownerName');
const oAgeElement = document.getElementById('ownerAge');

const ownerCatalogElement = document.getElementById('OwnerCatalog');

const updatedIdElement = document.getElementById("ownerIDUpdate");
const updatedNameElement = document.getElementById("ownerNameUpdate");
const updatedAgeElement = document.getElementById("ownerAgeUpdate");

checkAuthAndFetch();
setupSignalR();

async function checkAuthAndFetch() {
    const token = localStorage.getItem("jwtToken");
    if (!token) return;

    await getData();
}

async function getData() {
    const token = localStorage.getItem("jwtToken");
    if (!token) return;

    try {
        const res = await fetch('http://localhost:40567/Owner', {
            headers: { 'Authorization': `Bearer ${token}` }
        });

        owners = res.ok ? await res.json() : [];
        display();
    } catch (err) {
        console.error("Error fetching owners:", err);
    }
}

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:40567/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("OwnerCreated", () => getData());
    connection.on("OwnerDeleted", () => getData());
    connection.on("OwnerUpdated", () => getData());

    connection.onclose(async () => {
        await startSignalR();
    });

    startSignalR();
}

async function startSignalR() {
    try {
        await connection.start();
        console.log("SignalR Connected");
    } catch (err) {
        console.log(err);
        setTimeout(startSignalR, 5000);
    }
}

function display() {
    ownerCatalogElement.innerHTML = '';
    owners.forEach(o => {
        ownerCatalogElement.innerHTML += `
            <div style="vertical-align: middle;">
                <div class="outputText"><span>${o.id}</span></div>
                <div class="outputText">${o.name}</div>
                <div class="outputText">${o.age}</div>
                <div>
                    <button class="deleteButton" type="button" onclick="remove(${o.id})">Delete</button>
                    <button class="updateButton" type="button" onclick="showUpdate(${o.id})">Update</button>
                </div>
            </div>
        `;
    });
}

async function create() {
    const token = localStorage.getItem("jwtToken");
    if (!token) return;

    const oId = Number(oIdElement.value);
    const oName = oNameElement.value;
    const oAge = Number(oAgeElement.value);

    try {
        await fetch('http://localhost:40567/Owner', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ id: oId, name: oName, age: oAge })
        });

        oIdElement.value = '';
        oNameElement.value = '';
        oAgeElement.value = '';
        getData();
    } catch (err) {
        console.error('Error creating owner:', err);
    }
}

async function remove(id) {
    const token = localStorage.getItem("jwtToken");
    if (!token) return;

    try {
        await fetch(`http://localhost:40567/Owner/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        cancelUpdate();
        getData();
    } catch (err) {
        console.error('Error deleting owner:', err);
    }
}

function showUpdate(id) {
    const toBeUpdated = owners.find(o => o.id === id);
    if (!toBeUpdated) return;

    document.getElementById("Updater").style.display = '';
    updatedIdElement.innerHTML = toBeUpdated.id;
    updatedNameElement.value = toBeUpdated.name;
    updatedAgeElement.value = toBeUpdated.age;
}

async function update() {
    const token = localStorage.getItem("jwtToken");
    if (!token) return;

    const updatedID = Number(updatedIdElement.innerHTML);
    const updatedName = updatedNameElement.value;
    const updatedAge = Number(updatedAgeElement.value);

    try {
        await fetch('http://localhost:40567/Owner', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ id: updatedID, name: updatedName, age: updatedAge })
        });
        cancelUpdate();
        getData();
    } catch (err) {
        console.error('Error updating owner:', err);
    }
}

function cancelUpdate() {
    updatedIdElement.innerHTML = '';
    updatedNameElement.value = '';
    updatedAgeElement.value = '';
    document.getElementById("Updater").style.display = 'none';
}
