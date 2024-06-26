let owners = [];
let connection = null;
const oIdElement = document.getElementById('ownerID');
const oNameElement = document.getElementById('ownerName');
const oAgeElement = document.getElementById('ownerAge');
const ownerCatalogElement = document.getElementById('OwnerCatalog');
const updatedIdElement= document.getElementById("ownerIDUpdate");
const updatedNameElement = document.getElementById("ownerNameUpdate");
const updatedAgeElement = document.getElementById("ownerAgeUpdate");

getData();
setupSignalR()
async function getData() {
    await fetch('http://localhost:40567/Owner')
        .then(x => x.json())
        .then(y => {
            owners = y;
            console.log(y);
            display();
        });
}
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:40567/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build()

    connection.on("OwnerCreated", (user, message) => {
        getData();
    });

    connection.on("OwnerDeleted", (user, message) => {
        getData();
    });

    connection.on("OwnerUpdated", (user, message) => {
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
    ownerCatalogElement.innerHTML = null;
    owners.forEach(o => {
        ownerCatalogElement.innerHTML +=
            "<div style='vertical.align: middle;' ><div class='outputText'><span>" + o.id + "</span></div>" +
            "<div class='outputText'>" + o.name + "</div>" +
            "<div class='outputText'>" + o.age + "</div>" +
            "<div><button class='deleteButton' type='button' onclick='remove(" + o.id + ")'>Delete</button><button class='updateButton' type='button' onclick='showUpdate(" + o.id + ")'>Update</button></div></div>"
    })
}
function create() {
    let oId = Number(oIdElement.value);
    let oName = oNameElement.value;
    let oAge = Number(oAgeElement.value);
    fetch('http://localhost:40567/Owner', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: oId, name: oName, age: oAge })
    })
    .then(response => response)
    .then(data => {
        console.log('Success: ', data);
        getData();
    })
        .catch((error) => { console.error('Error:', error) });
    oIdElement.value = null;
    oNameElement.value = null;
    oAgeElement.value = null;
}
function remove(id) {
    fetch('http://localhost:40567/Owner/' + id, {
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
    var toBeUpdated = owners.find(o => o['id'] == id);
    document.getElementById("Updater").style.display = null;
    updatedIdElement.innerHTML = toBeUpdated.id;
    updatedNameElement.value = toBeUpdated.name;
    updatedAgeElement.value = toBeUpdated.age;
}
function update() {
    document.getElementById("Updater").style.display = "none";
    let updatedID = Number(updatedIdElement.innerHTML);
    let updatedName = updatedNameElement.value;
    let updatedAge = Number(updatedAgeElement.value);
    fetch('http://localhost:40567/Owner', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: updatedID, name: updatedName, age: updatedAge })
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
    updatedAgeElement.value = null;
    document.getElementById("Updater").style.display = "none";
}