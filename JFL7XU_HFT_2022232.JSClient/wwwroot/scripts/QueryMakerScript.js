let queryBox = [];
var outputStringElement;
const xElement = document.getElementById('variableX');
const yElement = document.getElementById('variableY');
const queryCatalogElement = document.getElementById('QueryCatalog');
var x;
var y;

function getX() {
    x = xElement.value;
    if (!isNaN(x)) { return x; }
    else {
        x = null;
        console.log("Wrong input variables!");
    }
    return x;
}
function getY() {
    y = yElement.value;
    if (!isNaN(y)) { return y; }
    else {
        y = null;
        console.log("Wrong input variables!");
    }
    return y;
}

function query(num) {
    if (num == 7) {
        ListReports();
    }
    switch (num) {
        case 1:
            ListShips();
            break;
        case 2:
            ListFullerHangars();
            break;
        case 3:
            ListMoreEmptyHangars();
            break;
        case 4:
            ListOlderOwners();
            break;
        case 5:
            ListYoungerOwners();
            break;
        case 6:
            ListComplexOwners();
            break;
        case 7:
            ListReports();
            break;
        default:
            alert('Not implemented Query!');
            break;
    }
}

async function ListShips() {
    queryBox = null;
    var year = getX();
    if (year != null && year != "") {
        await fetch('http://localhost:40567/NonCrud/ListShips_WhichBuiltAfter/' + year)
            .then(x => x.json())
            .then(y => {
                queryBox = y;
                console.log(y);
                displayShips();
            });
    }
    else {
        alert("X was null!");
    }
}
async function ListFullerHangars() {
    queryBox = null;
    var quantity = getX();
    if (quantity != null && quantity != '') {
        await fetch('http://localhost:40567/NonCrud/ListHangars_WithShipsMoreThan/' + quantity)
            .then(x => x.json())
            .then(y => {
                queryBox = y;
                console.log(y);
                displayHangars();
            });
    }
    else {
        alert("X was null!");
    }
}
async function ListMoreEmptyHangars() {
    queryBox = null;
    var quantity = getX();
    if (quantity != null && quantity != '') {
        await fetch('http://localhost:40567/NonCrud/ListHangars_WithShipsLessThan/' + quantity)
            .then(x => x.json())
            .then(y => {
                queryBox = y;
                console.log(y);
                displayHangars();
            });
    }
    else {
        alert("X was null!");
    }
}
async function ListOlderOwners() {
    queryBox = null;
    var age = getX();
    if (age != null && age != '') {
        await fetch('http://localhost:40567/NonCrud/ListOwners_OlderThan/' + age)
            .then(x => x.json())
            .then(y => {
                queryBox = y;
                console.log(y);
                displayOwners();
            });
    }
    else {
        alert("X was null!");
    }
}
async function ListYoungerOwners() {
    queryBox = null;
    var age = getX();
    if (age != null && age != '') {
        await fetch('http://localhost:40567/NonCrud/ListOwners_YoungerThan/' + age)
            .then(x => x.json())
            .then(y => {
                queryBox = y;
                console.log(y);
                displayOwners();
            });
    }
    else {
        alert("X was null!");
    }
}
async function ListComplexOwners() {
    queryBox = null;
    var age = getX();
    var quantity = getY();
    if (quantity != '' && age != '') {
        var url = 'http://localhost:40567/NonCrud/ListOwners_YoungerAndHasMoreShipsThan/' + age + '/' + quantity;
        await fetch(url)
            .then(x => x.json())
            .then(y => {
                queryBox = y;
                console.log(y);
                displayOwners();
            });
    }
    else {
        if (quantity == '' && age == '') {
            alert("X and Y was null!");
        }
        else if (age == '') {
            alert("X was null!");
        }
        else {
            alert("Y was null!");
        }
    }
}
async function ListReports() {
    queryBox = null;
    await fetch('http://localhost:40567/NonCrud/ListStatistics')
        .then(x => x.json())
        .then(y => {
            queryBox = y;
            console.log(y);
            displayReport();
        });
}
function displayShips() {
    queryCatalogElement.innerHTML = null;
    outputStringElement = "";
    queryBox.forEach(q => {
        outputStringElement +=
            "<div><div class='outputText'>" + q.id + ".</div><div class='outputText'>" + q.name + "</div><div class='outputText'>Year of manufacture: " + q.yearOfManu + "</div><div class='outputText'>Type: " + q.type +"</div><div class='outputText'>Owner: "+q.ownerID+"</div></div>";
    })
    queryCatalogElement.innerHTML = outputStringElement;
}
function displayHangars() {
    queryCatalogElement.innerHTML = null;
    outputStringElement = "";
    queryBox.forEach(q => {
        outputStringElement +=
            "<div><div class='outputText'>" + q.id + ".</div><div class='outputText'>" + q.name + "</div><div class='outputText'>Located at: " + q.location + "</div><div class='outputText'>Owner: " + q.ownerID + "</div></div>";
    })
    queryCatalogElement.innerHTML = outputStringElement;
}
function displayOwners() {
    queryCatalogElement.innerHTML = null;
    outputStringElement = "";
    queryBox.forEach(q => {
        outputStringElement +=
            "<div><div class='outputText'>" + q.id + ".</div><div class='outputText'>" + q.name + "</div><div class='outputText'>" + q.age + " years old</div></div>";
    })
    queryCatalogElement.innerHTML = outputStringElement;
}
function displayReport() {
    queryCatalogElement.innerHTML = null;
    outputStringElement = "";
    queryBox.forEach(q => {
        outputStringElement +=
            "<div><div class='outputTextFirst'>Owner: " + q.owner.id + ". " + q.owner.name + ", " + q.owner.age + " years old, has hangar at: " + q.hangar.location + " named " + q.hangar.name + ".</div>" +
            "<div class='outputShipHeader'>-Ships owned:</div><div class='outputListingContainer'>";
        q.ships.forEach(s => {
            outputStringElement +=
                "<label class='outputListing'>-" + s.id + ". name: " + s.name + ", type: " + s.type + "</label><br/>";
        })
        outputStringElement += "</div></div>";
    })
    queryCatalogElement.innerHTML = outputStringElement;
}
