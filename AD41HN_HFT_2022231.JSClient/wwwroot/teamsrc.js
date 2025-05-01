let teams = [];
let connection = null;
let teamIdtoupdate
getdata();
setupSignalR();



function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5218/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TeamCreated", (user, message) => {
        getdata();
    });

    connection.on("TeamDeleted", (user, message) => {
        getdata();
    });

    connection.on("TeamUpdated", (user, message) => {
        getdata();
    });
    
   

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


async function getdata() {
    await fetch('http://localhost:5218/team')
        .then(x => x.json())
        .then(y => {
            teams = y;
            display();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    teams.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" +
        `<button type="button" onclick="remove(${t.id})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}


function remove(id) {
    fetch('http://localhost:5218/team/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}



function showupdate(id) {
   // alert("aasdsd");
   
    document.getElementById('nametoupdate').value = teams.find(t => t.id == id).name;
    teamIdtoupdate = id;
   // documet.getElementById('updateformdiv').style.display ='flex';
    
}


function update() {

    
    let name = document.getElementById('nametoupdate').value;

    fetch('http://localhost:5218/team', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Name: name, Id:teamIdtoupdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('name').value;
    fetch('http://localhost:5218/team', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Name: name })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

