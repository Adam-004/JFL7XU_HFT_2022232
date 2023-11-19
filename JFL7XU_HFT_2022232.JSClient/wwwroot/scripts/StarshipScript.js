let ships = [];
let connection = null;
var toBeUpdated;
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

getData();
setupSignalR()
async function getData() {
    await fetch('http://localhost:40567/Starship')
        .then(x => x.json())
        .then(y => {
            ships = y;
            console.log(y);
            display();
        });
}
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:40567/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build()

    connection.on("StarshipCreated", (user, message) => {
        getData();
    });

    connection.on("StarshipDeleted", (user, message) => {
        getData();
    });

    connection.on("StarshipUpdated", (user, message) => {
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
    shipCatalogElement.innerHTML = null;
    ships.forEach(h => {
        shipCatalogElement.innerHTML +=
        "<div><div class='outputText'><span>" + h.id + "</span></div>" +
        "<div class='outputText'>" + h.name + "</div>" +
        "<div class='outputText'>" + h.size + "</div>" +
        "<div class='outputText'>" + h.yearOfManu + "</div>" +
        "<div class='outputText'>" + h.type + "</div>" +
        "<div class='outputText'>" + h.ownerID + "</div>" +
        "<div><button class='deleteButton' type='button' onclick='remove(" + h.id + ")'>Delete</button><button class='updateButton' type='button' onclick='showUpdate(" + h.id + ")'>Update</button></div></div>"
    })
}
function create() {
    let sId = Number(sIdElement.value);
    let sName = sNameElement.value;
    let sSize = Number(sSizeElement.value);
    let sYOM = Number(sYOMElement.value);
    let sType = Number(sTypeElement.value);
    let sOId = Number(sOIdElement.value);
    fetch('http://localhost:40567/Starship', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: sId, name: sName, size: sSize, yearOfManu: sYOM, type: sType, ownerID: sOId })
    })
    .then(response => response)
    .then(data => {
        console.log('Success: ', data);
        getData();
    })
    .catch((error) => { console.error('Error:', error) });
    sIdElement.value = null;
    sNameElement.value = null;
    sSizeElement.value = null;
    sYOMElement.value = null;
    sTypeElement.value = null;
    sOIdElement.value = null;
}
function remove(id) {
    fetch('http://localhost:40567/Starship/' + id, {
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
    toBeUpdated = ships.find(o => o['id'] == id);
    document.getElementById("Updater").style.display = null;
    updatedIdElement.innerHTML = toBeUpdated.id;
    updatedNameElement.value = toBeUpdated.name;
    updatedSizeElement.value = toBeUpdated.size;
    updatedYOMElement.value = toBeUpdated.yearOfManu;
    updatedTypeElement.value = toBeUpdated.type;
    updatedOIdElement.value = toBeUpdated.ownerID;
}
function update() {
    document.getElementById("Updater").style.display = "none";
    fetch('http://localhost:40567/Starship', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: Number(updatedIdElement.innerHTML), name: updatedNameElement.value, size: Number(updatedSizeElement.value), yearOfManu: Number(updatedYOMElement.value), type: Number(updatedTypeElement.value), ownerID: updatedOIdElement.value })
    })
        .then(response => response)
        .then(data => {
            console.log('Success: ', data);
            getData();
        })
        .catch((error) => { console.error('Error:', error) });
    toBeUpdated = null;
}
function cancelUpdate() {
    updatedIdElement.innerHTML = null;
    updatedNameElement.value = null;
    updatedSizeElement.value = null;
    updatedYOMElement.value = null;
    updatedTypeElement.value = null;
    updatedOIdElement.value = null;
    document.getElementById("Updater").style.display = "none";
    toBeUpdated = null;
}
