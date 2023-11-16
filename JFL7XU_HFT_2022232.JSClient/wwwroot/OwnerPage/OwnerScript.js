let owners = [];
let connection = null;
const oIdElement = document.getElementById('ownerID');
const oNameElement = document.getElementById('ownerName');
const oAgeElement = document.getElementById('ownerAge');
const ownerCatalogElement = document.getElementById('OwnerCatalog');

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
    //http://localhost:40567/
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
            "<div><div class='outputText'><span>" + o.id + "</span></div>" +
            "<div class='outputText'>" + o.name + "</div>" +
            "<div class='outputText'>" + o.age + "</div>" +
            "<div class='outputText'><button class='deleteButton' type='button' onclick='remove(" + o.id + ")'>Delete</button><button class='updateButton' type='button' onclick='showUpdate(" + o.id + ")'>Update</button></div></div>"
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
}
function showUpdate(id) {
    var toBeUpdated = owners.find(o => o['id'] == id);
    document.getElementById("Updater").style.display = null;
    document.getElementById("ownerIDUpdate").innerHTML = toBeUpdated.id;
    document.getElementById("ownerNameUpdate").value = toBeUpdated.name;
    document.getElementById("ownerAgeUpdate").value = toBeUpdated.age;
}
function update() {
    document.getElementById("Updater").style.display = "none";
    let updatedID = Number(document.getElementById("ownerIDUpdate").value);
    let updatedName = document.getElementById("ownerNameUpdate").value;
    let updatedAge = Number(document.getElementById("ownerAgeUpdate").value);
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
    document.getElementById("ownerIDUpdate").value = null;
    document.getElementById("ownerNameUpdate").value = null;
    document.getElementById("ownerAgeUpdate").value = null;
    document.getElementById("Updater").style.display = "none";
}