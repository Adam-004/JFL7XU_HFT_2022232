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

getData();
setupSignalR()
async function getData() {
    await fetch('http://localhost:40567/Hangar')
        .then(x => x.json())
        .then(y => {
            hangars = y;
            console.log(y);
            display();
        });
}
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:40567/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build()

    connection.on("HangarCreated", (user, message) => {
        getData();
    });

    connection.on("HangarDeleted", (user, message) => {
        getData();
    });

    connection.on("HangarUpdated", (user, message) => {
        getData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}
function display() {
    hangarCatalogElement.innerHTML = null;
    hangars.forEach(h => {
        hangarCatalogElement.innerHTML +=
            "<div style='vertical.align: middle;' ><div class='outputText'><span>" + h.id + "</span></div>" +
            "<div class='outputText'>" + h.name + "</div>" +
            "<div class='outputText'>" + h.location + "</div>" +
            "<div class='outputText'>" + h.ownerID + "</div>" +
            "<div><button class='deleteButton' type='button' onclick='remove(" + h.id + ")'>Delete</button><button class='updateButton' type='button' onclick='showUpdate(" + h.id + ")'>Update</button></div></div>"
    })
}
function create() {
    let hId = Number(hIdElement.value);
    let hName = hNameElement.value;
    let hLoc = hLocElement.value;
    let hOId = Number(hOIdElement.value);
    fetch('http://localhost:40567/Hangar', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: hId, name: hName, location: hLoc, ownerID:hOId})
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });
    hIdElement.value = null;
    hNameElement.value = null;
    hLocElement.value = null;
    hOIdElement.value = null;
}
function remove(id) {
    fetch('http://localhost:40567/Hangar/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null,
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });
    cancelUpdate();
}
function showUpdate(id) {
    var toBeUpdated = hangars.find(o => o['id'] == id);
    document.getElementById("Updater").style.display = null;
    updatedIdElement.innerHTML = toBeUpdated.id;
    updatedNameElement.value = toBeUpdated.name;
    updatedLocElement.value = toBeUpdated.location;
    updatedOIdElement.value = toBeUpdated.ownerID;
}
function update() {
    document.getElementById("Updater").style.display = "none";
    let updatedID = Number(updatedIdElement.innerHTML);
    let updatedName = updatedNameElement.value;
    let updatedLoc = updatedLocElement.value;
    let updatedOId = Number(updatedOIdElement.value);
    fetch('http://localhost:40567/Hangar', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: updatedID, name: updatedName, location: updatedLoc, ownerID:updatedOId })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });
}
function cancelUpdate() {
    updatedIdElement.innerHTML = null;
    updatedNameElement.value = null;
    updatedLocElement.value = null;
    updatedOIdElement.value = null;
    document.getElementById("Updater").style.display = "none";
}