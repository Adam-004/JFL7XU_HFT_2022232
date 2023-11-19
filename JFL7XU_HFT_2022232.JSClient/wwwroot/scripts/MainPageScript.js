let hangars = [];
let hangarCounter;
let owners = [];
let ownerCounter;
let starships = [];
let starshipCounter;

let connection = null;
getAllRecords();

async function getAllRecords() {
    await fetch('http://localhost:40567/Owner')
        .then(x => x.json())
        .then(y => {
            hangars = y;
            console.log(y);
            hangarCounter = hangars.length;
        });
    await fetch('http://localhost:40567/Hangar')
        .then(x => x.json())
        .then(y => {
            owners = y;
            console.log(y);
            ownerCounter = owners.length;
        });
    await fetch('http://localhost:40567/Starship')
        .then(x => x.json())
        .then(y => {
            starships = y;
            console.log(y);
            starshipCounter = starships.length;
        });
    displayRecords();
}
function displayRecords() {
    document.getElementById('OwnerDisplay').innerHTML = ownerCounter + " records.";
    document.getElementById('HangarDisplay').innerHTML = hangarCounter + " records.";
    document.getElementById('StarshipDisplay').innerHTML = starshipCounter + " records.";
}