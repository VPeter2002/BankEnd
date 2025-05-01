let trainers = [];
let connection = null;
let trainerIdtoupdate
getdata();
setupSignalR();



function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5218/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TrainerCreated", (user, message) => {
        getdata();
    });

    connection.on("TrainerDeleted", (user, message) => {
        getdata();
    });

    connection.on("TrainerUpdated", (user, message) => {
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
    await fetch('http://localhost:5218/trainer')
        .then(x => x.json())
        .then(y => {
            trainers = y;
            display();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    trainers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" +
        `<button type="button" onclick="remove(${t.id})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}


function remove(id) {
    fetch('http://localhost:5218/trainer/' + id, {
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
   
    document.getElementById('nametoupdate').value = trainers.find(t => t.id == id).name;
    trainerIdtoupdate = id;
   // documet.getElementById('updateformdiv').style.display ='flex';
    
}


function update() {

    
    let name = document.getElementById('nametoupdate').value;

    fetch('http://localhost:5218/trainer', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { Name: name/*, Id: trainerIdtoupdate*/  })
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
    fetch('http://localhost:5218/trainer', {
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

