let ships = [];
let connection = null;
let toBeUpdated = null;

const sIdElement = document.getElementById('shipID');
const sNameElement = document.getElementById('shipName');
const sSizeElement = document.getElementById('shipSize');
const sYOMElement = document.getElementById('shipYOM');
const sTypeElement = document.getElementById('shipType');
const sOIdElement = document.getElementById('shipOId');

const shipCatalogElement = document.getElementById('ShipCatalog');

const updatedIdElement = document.getElementById("shipIdUpdate");
const updatedNameElement = document.getElementById("shipNameUpdate");
const updatedSizeElement = document.getElementById("shipSizeUpdate");
const updatedYOMElement = document.getElementById("shipYOMUpdate");
const updatedTypeElement = document.getElementById("shipTypeUpdate");
const updatedOIdElement = document.getElementById("shipOIdUpdate");

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
        const res = await fetch('http://localhost:40567/Starship', {
            headers: { 'Authorization': `Bearer ${token}` }
        });
        ships = res.ok ? await res.json() : [];
        display();
    } catch (err) {
        console.error("Error fetching starships:", err);
    }
}

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:40567/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("StarshipCreated", () => getData());
    connection.on("StarshipDeleted", () => getData());
    connection.on("StarshipUpdated", () => getData());

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
    shipCatalogElement.innerHTML = '';
    ships.forEach(s => {
        shipCatalogElement.innerHTML += `
            <div>
                <div class="outputText"><span>${s.id}</span></div>
                <div class="outputText">${s.name}</div>
                <div class="outputText">${s.size}</div>
                <div class="outputText">${s.yearOfManu}</div>
                <div class="outputText">${s.type}</div>
                <div class="outputText">${s.ownerID}</div>
                <div>
                    <button class="deleteButton" type="button" onclick="remove(${s.id})">Delete</button>
                    <button class="updateButton" type="button" onclick="showUpdate(${s.id})">Update</button>
                </div>
            </div>
        `;
    });
}

async function create() {
    const token = localStorage.getItem("jwtToken");
    if (!token) return;

    const sId = Number(sIdElement.value);
    const sName = sNameElement.value;
    const sSize = Number(sSizeElement.value);
    const sYOM = Number(sYOMElement.value);
    const sType = Number(sTypeElement.value);
    const sOId = Number(sOIdElement.value);

    try {
        await fetch('http://localhost:40567/Starship', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ id: sId, name: sName, size: sSize, yearOfManu: sYOM, type: sType, ownerID: sOId })
        });

        sIdElement.value = '';
        sNameElement.value = '';
        sSizeElement.value = '';
        sYOMElement.value = '';
        sTypeElement.value = '';
        sOIdElement.value = '';
        getData();
    } catch (err) {
        console.error("Error creating starship:", err);
    }
}

async function remove(id) {
    const token = localStorage.getItem("jwtToken");
    if (!token) return;

    try {
        await fetch(`http://localhost:40567/Starship/${id}`, {
            method: 'DELETE',
            headers: { 'Authorization': `Bearer ${token}` }
        });
        cancelUpdate();
        getData();
    } catch (err) {
        console.error("Error deleting starship:", err);
    }
}

function showUpdate(id) {
    toBeUpdated = ships.find(s => s.id === id);
    if (!toBeUpdated) return;

    document.getElementById("Updater").style.display = '';
    updatedIdElement.innerHTML = toBeUpdated.id;
    updatedNameElement.value = toBeUpdated.name;
    updatedSizeElement.value = toBeUpdated.size;
    updatedYOMElement.value = toBeUpdated.yearOfManu;
    updatedTypeElement.value = toBeUpdated.type;
    updatedOIdElement.value = toBeUpdated.ownerID;
}

async function update() {
    const token = localStorage.getItem("jwtToken");
    if (!token || !toBeUpdated) return;

    const updatedData = {
        id: Number(updatedIdElement.innerHTML),
        name: updatedNameElement.value,
        size: Number(updatedSizeElement.value),
        yearOfManu: Number(updatedYOMElement.value),
        type: Number(updatedTypeElement.value),
        ownerID: Number(updatedOIdElement.value)
    };

    try {
        await fetch('http://localhost:40567/Starship', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(updatedData)
        });

        cancelUpdate();
        getData();
    } catch (err) {
        console.error("Error updating starship:", err);
    }
}

function cancelUpdate() {
    updatedIdElement.innerHTML = '';
    updatedNameElement.value = '';
    updatedSizeElement.value = '';
    updatedYOMElement.value = '';
    updatedTypeElement.value = '';
    updatedOIdElement.value = '';
    document.getElementById("Updater").style.display = 'none';
    toBeUpdated = null;
}
