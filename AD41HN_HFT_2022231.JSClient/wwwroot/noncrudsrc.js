let datas = [];

function display() {
    document.getElementById('noncrudresult').innerHTML = "";
    datas.forEach(t => {
        document.getElementById('noncrudresult').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>"
            + "</td></tr>";
    });
}
async function getdata01() {
    await fetch('http://localhost:5218/Non_crud/GetGKs')
        .then(x => x.json())
        .then(y => {
            datas = y;
            console.log(datas)
            display();
            datas = [];
        });
}

async function getdata02() {
    await fetch('http://localhost:5218/Non_crud/GetHun')
        .then(x => x.json())
        .then(y => {
            datas = y;
            //console.log(datas)
            display();
        });
}


async function getdata03() {
    await fetch('http://localhost:5218/Non_crud/GetEng')
        .then(x => x.json())
        .then(y => {
            datas = y;
            //console.log(datas)
            display();
        });
}





async function getdata04() {
    await fetch('http://localhost:5218/Non_crud/GetTeamIds')
        .then(x => x.json())
        .then(y => {
            datas = y;
            //console.log(datas)
            display();
        });
}





async function getdata05() {
    await fetch('http://localhost:5218/Non_crud/GetGermanyTrainers')
        .then(x => x.json())
        .then(y => {
            datas = y;
            //console.log(datas)
            display();
        });
}
