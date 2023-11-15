let owners = [];
let connection = null;
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
    document.getElementById('OwnerCatalog').innerHTML = null;
    owners.forEach(o => {
        document.getElementById('OwnerCatalog').innerHTML +=
            "<tr><td>" + o.id + "</td><td>" + o.name + "</td><td>" + o.age + '</td>' +
            '<td><button type="button" onclick="remove(' + o.id + ')">Delete</button><button type="button" onclick="showUpdate(' + o.id + ')">Update</button></td></tr>';
        console.log(o.name);
    })
}
function create() {
    let oId = Number(document.getElementById('ownerID').value);
    let oName = document.getElementById('ownerName').value;
    let oAge = Number(document.getElementById('ownerAge').value);
    fetch('http://localhost:40567/Owner', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({ id: oId, name: oName, age: oAge })
    })
    .then(response => response)
        .then(data =>
        {
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
    document.getElementById("ownerIDUpdate").value = toBeUpdated.id;
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
        body: JSON.stringify({ id:updatedID, name: updatedName, age: updatedAge })
    })
    .then(response => response)
    .then(data => {
        console.log('Success: ', data);
        getData();
    })
    .catch((error) => { console.error('Error:', error) });
    
}