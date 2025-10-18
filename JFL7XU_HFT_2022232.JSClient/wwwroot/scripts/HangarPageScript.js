let hangars = [];
let connection = null;

const hIdElement = document.getElementById('hangarId');
const hNameElement = document.getElementById('hangarName');
const hLocElement = document.getElementById('hangarLoc');
const hOIdElement = document.getElementById('hangarOId');

const hangarCatalogElement = document.getElementById('HangarCatalog');

const updatedIdElement = document.getElementById("hangarIDUpdate");
const updatedNameElement = document.getElementById("hangarNameUpdate");
const updatedLocElement = document.getElementById("hangarLocUpdate");
const updatedOIdElement = document.getElementById("hangarOIdUpdate");

const token = () => localStorage.getItem('jwtToken');

getData();
setupSignalR();

async function getData() {
    await fetch('http://localhost:40567/Hangar', {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token()
        }
    })
        .then(x => x.json())
        .then(y => {
            hangars = y;
            display();
        })
        .catch(err => console.error('Error fetching hangars:', err));
}

function create() {
    fetch('http://localhost:40567/Hangar', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token()
        },
        body: JSON.stringify({
            id: Number(hIdElement.value),
            name: hNameElement.value,
            location: hLocElement.value,
            ownerID: Number(hOIdElement.value)
        })
    })
        .then(response => response)
        .then(() => {
            getData();
            clearCreateForm();
        })
        .catch(err => console.error('Error creating hangar:', err));
}

function remove(id) {
    fetch(`http://localhost:40567/Hangar/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token()
        }
    })
        .then(() => getData())
        .catch(err => console.error('Error deleting hangar:', err));
}

function update() {
    const updatedID = Number(updatedIdElement.innerHTML);
    fetch('http://localhost:40567/Hangar', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token()
        },
        body: JSON.stringify({
            id: updatedID,
            name: updatedNameElement.value,
            location: updatedLocElement.value,
            ownerID: Number(updatedOIdElement.value)
        })
    })
        .then(() => {
            getData();
            cancelUpdate();
        })
        .catch(err => console.error('Error updating hangar:', err));
}

function display() {
    hangarCatalogElement.innerHTML = '';
    hangars.forEach(h => {
        hangarCatalogElement.innerHTML += `
            <div style='vertical-align: middle;'>
                <div class='outputText'><span>${h.id}</span></div>
                <div class='outputText'>${h.name}</div>
                <div class='outputText'>${h.location}</div>
                <div class='outputText'>${h.ownerID}</div>
                <div>
                    <button class='deleteButton' type='button' onclick='remove(${h.id})'>Delete</button>
                    <button class='updateButton' type='button' onclick='showUpdate(${h.id})'>Update</button>
                </div>
            </div>`;
    });
}

function showUpdate(id) {
    const toBeUpdated = hangars.find(o => o.id === id);
    document.getElementById("Updater").style.display = null;
    updatedIdElement.innerHTML = toBeUpdated.id;
    updatedNameElement.value = toBeUpdated.name;
    updatedLocElement.value = toBeUpdated.location;
    updatedOIdElement.value = toBeUpdated.ownerID;
}

function cancelUpdate() {
    updatedIdElement.innerHTML = null;
    updatedNameElement.value = '';
    updatedLocElement.value = '';
    updatedOIdElement.value = '';
    document.getElementById("Updater").style.display = "none";
}

function clearCreateForm() {
    hIdElement.value = '';
    hNameElement.value = '';
    hLocElement.value = '';
    hOIdElement.value = '';
}

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:40567/hub", {
            accessTokenFactory: () => token()
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("HangarCreated", () => getData());
    connection.on("HangarDeleted", () => getData());
    connection.on("HangarUpdated", () => getData());

    connection.onclose(async () => {
        await startConnection();
    });

    startConnection();
}

async function startConnection() {
    try {
        await connection.start();
        console.log("SignalR Connected");
    } catch (err) {
        console.error(err);
        setTimeout(startConnection, 5000);
    }
}
