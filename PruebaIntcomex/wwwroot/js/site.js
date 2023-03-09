const _URLSERVICE = "https://localhost:44378/";
const _URLSERVICE_USER = _URLSERVICE + "api/Usuario";
const _URLSERVICE_GETUSER = _URLSERVICE + "api/Usuario/";
const _URLSERVICE_TIPOCONT = _URLSERVICE + "api/TipoContacto";
const _URLSERVICE_GETTIPOCONT = _URLSERVICE + "api/TipoContacto/";
const _URLSERVICE_CLIENTE = _URLSERVICE + "api/Cliente";
const _URLSERVICE_GETCLIENTE = _URLSERVICE + "api/Cliente/";
const _URLSERVICE_CARGO = _URLSERVICE + "api/Cargo";
const _URLSERVICE_GETCARGO = _URLSERVICE + "api/Cargo/";
var allUser = null;
var allCargo = null;
var allTiposCont = null;

async function getallUser(view) {
    await fetch(_URLSERVICE_USER).then(function(response){
        return response.json();
    }).then(function (Data) {

        allUser = Data;
        console.log('getallUser--->' + view);
        if (view =='users')
        {
            const tableData = Data.map(value => {
                return (`<tr>
                <td>${value.nombreUsuario}</td>
                <td>
                <button class="btn btn-danger" value="${value.idUsuario}" onclick="InactiveUser(this.value)">Inactivar</button>
                <button class="btn btn-info" value="${value.idUsuario}" onclick="gestionarUser(Number(this.value))">Actualizar</button>
                </td>
            </tr>`
                );
            }).join('');

            const tableBody = document.querySelector("#tableBodyUsers");
            tableBody.innerHTML = tableData;

        }

        if (view == 'cliente') {
            console.log('getallUser entro al view del cliente--->' + view);
            if (allUser.length > 0)
            {
                var userselect = document.getElementById("userselect");                
                for (var i = 0; i < allUser.length; i++) {
                    var myOption = document.createElement("option");
                    myOption.text = allUser[i].nombreUsuario;
                    myOption.value = allUser[i].idUsuario;
                    userselect.add(myOption);
                }
            }
        }
    })
}

function findAttribute(model, attr, id, value) {
    var user = model.filter(x => x[id] == value[id]);    
    return (user.length > 0) ? user[0][attr] : 'N/A'; 
}

async function getallClients() {   

    await fetch(_URLSERVICE_CLIENTE).then(function (response) {
        return response.json();
    }).then(function (Data) {
        console.log('getallClients--->');
        const tableData = Data.map(value => {

            var nickuser = findAttribute(allUser, 'nombreUsuario', 'idUsuario', value);
            var namecargo = findAttribute(allCargo, 'nombreCargo', 'idCargo', value);
            var nametp = findAttribute(allTiposCont, 'nombreTipoContacto', 'idTipoContacto', value);

            return (  `<tr>
               <td>${value.nombreCompleto}</td>
               <td>${value.telefono}</td>
               <td>${value.correoElectronico}</td>
               <td>${nickuser}</td>
               <td>${namecargo}</td>
               <td>${nametp}</td>
               <td>
                <button class="btn btn-danger" value="${value.codigoCliente}" onclick="InactiveCliente(this.value)">Inactivar</button>
                <button class="btn btn-info" value="${value.codigoCliente}" onclick="gestionarCliente(Number(this.value))">Actualizar</button>
                </td>
            </tr>`
            );
        }).join('');

        const tableBody = document.querySelector("#tableBody");
        tableBody.innerHTML = tableData;
        document.getElementById("btnconsultar").removeAttribute('disabled');
        
    })
}

async function getallCargos(view) {
    await fetch(_URLSERVICE_CARGO).then(function (response) {
        return response.json();
    }).then(function (Data) {

        allCargo = Data;
        console.log('getallCargos--->'+view);
        if (view =='cargos')
        {        
            const tableData = Data.map(value => {
                return (`<tr>
                   <td>${value.nombreCargo}</td>
                   <td>
                   <button class="btn btn-danger" value="${value.idCargo}" onclick="InactiveCargo(this.value)">Inactivar</button>
                   <button class="btn btn-info" value="${value.idCargo}" onclick="gestionarCargo(Number(this.value))">Actualizar</button>
                   </td>
                </tr>`
                );
            }).join('');

            const tableBody = document.querySelector("#tableBodyCargos");
            tableBody.innerHTML = tableData;

        }

        if (view == 'cliente')
        {
            console.log('getallCargos entro al view del cliente--->' + view);
            if (allCargo.length > 0) {
                var cargoselect = document.getElementById("cargoselect");
                for (var i = 0; i < allCargo.length; i++) {
                    var myOption = document.createElement("option");
                    myOption.text = allCargo[i].nombreCargo;
                    myOption.value = allCargo[i].idCargo;
                    cargoselect.add(myOption);
                }
            }
        }

    })
}

async function getallTiposCont(view) {
    await fetch(_URLSERVICE_TIPOCONT).then(function (response) {
        return response.json();
    }).then(function (Data) {
        //console.log(Data);
        allTiposCont = Data;
        console.log('getallTiposCont--->' + view);
        
        if (view == 'tps')
        {
            const tableData = Data.map(value => {
                return (`<tr>
               <td>${value.nombreTipoContacto}</td>
               <td>
                <button class="btn btn-danger" value="${value.idTipoContacto}" onclick="InactiveTP(this.value)">Inactivar</button>
                <button class="btn btn-info" value="${value.idTipoContacto}" onclick="gestionarTP(Number(this.value))">Actualizar</button>
                </td>
            </tr>`
                );
            }).join('');

            const tableBody = document.querySelector("#tableBodyTP");
            tableBody.innerHTML = tableData;

        }

        if (view == 'cliente')
        {
            console.log('getallTiposCont entro al view del cliente--->' + view);
            if (allTiposCont.length > 0) {
                var tpselect = document.getElementById("tpselect");
                for (var i = 0; i < allTiposCont.length; i++) {
                    var myOption = document.createElement("option");
                    myOption.text = allTiposCont[i].nombreTipoContacto;
                    myOption.value = allTiposCont[i].idTipoContacto;
                    tpselect.add(myOption);
                }
            }
        }
    })
}

function saveUsers(user) {
    console.log("el usuario es " + user);
    fetch(_URLSERVICE_USER, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept":"application/json"
        },
        body: JSON.stringify({ nombreUsuario: user}),
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida" + response.json());
            $('#exampleModal').modal('hide');
            getallUser('users');
        } else {
            console.error("Error:", response.json());
        }
    }).catch((error) => {
            console.error("Error:", error);
        });

}

function saveCargos(cargo) {
    console.log("el cargo es " + cargo);
    fetch(_URLSERVICE_CARGO, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        body: JSON.stringify({ nombreCargo: cargo }),
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida" + response.json());
            $('#exampleModal').modal('hide');
            getallCargos('cargos');
        } else {
            console.error("Error:", response.json());
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function saveTP(tp) {
    console.log("el tp es " + tp);
    fetch(_URLSERVICE_TIPOCONT, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        body: JSON.stringify({ nombreTipoContacto: tp }),
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida" + response.json());
            $('#exampleModal').modal('hide');
            getallTiposCont('tps');
        } else {
            console.error("Error:", response.json());
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function saveClient(inputNombre, inputEmail, inputTelefono, inputUser, inputCargo, inputTP) {
    console.log("saveClient");
    fetch(_URLSERVICE_CLIENTE, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        body: JSON.stringify({
            nombreCompleto: inputNombre,
            telefono: Number(inputTelefono),
            correoElectronico: inputEmail,
            idUsuario: Number(inputUser),
            idCargo: Number(inputCargo),
            idTipoContacto: Number(inputTP)
        }),
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida" + response.json());
            $('#exampleModal').modal('hide');
            getallClients();
        } else {
            console.error("Error:", response.json());
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function valideKey(evt) {

    var code = (evt.which) ? evt.which : evt.keyCode;
    if (code == 8) {
        return true;
    } else if (code >= 48 && code <= 57) {
        return true;
    } else {
        return false;
    }
}

function InactiveUser(user) {
    console.log("el InactiveUser es " + user);
    fetch(_URLSERVICE_GETUSER + user, {
        method: "DELETE"
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida->" + response.statusText);
            
            getallUser('users');
        } else {
            console.error("Error:",response.statusText);
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function InactiveCargo(cargo) {
    console.log("el InactiveCargo es " + cargo);
    fetch(_URLSERVICE_GETCARGO + cargo, {
        method: "DELETE"
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida->" + response.statusText);
            getallCargos('cargos');
        } else {
            console.error("Error:", response.statusText);
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function InactiveTP(tp) {
    console.log("el InactiveTP es " + tp);
    fetch(_URLSERVICE_GETTIPOCONT + tp, {
        method: "DELETE"
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida->" + response.statusText);
            getallTiposCont('tps');
        } else {
            console.error("Error:", response.statusText);
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function InactiveCliente(cliente) {
    console.log("el InactiveCliente es " + cliente);
    fetch(_URLSERVICE_GETCLIENTE + cliente, {
        method: "DELETE"
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida->" + response.statusText);
            getallClients();
        } else {
            console.error("Error:", response.statusText);
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function getUser(user) {
    console.log("el getUser es " + user);
    fetch(_URLSERVICE_GETUSER + user, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "*/*"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (Data) {
        console.log('getUser resultado--->' + Data.nombreUsuario);
        document.getElementById('user').value = Data.nombreUsuario;
        document.getElementById('userid').value = user;
    }).catch((error) => {
        console.error("Error:", error);
    });
    
}

function getCargo(cargo) {
    console.log("el getCargo es " + cargo);
    fetch(_URLSERVICE_GETCARGO + cargo, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "*/*"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (Data) {
        console.log('getCargo resultado--->' + Data.nombreCargo);
        document.getElementById('cargo').value = Data.nombreCargo;
        document.getElementById('cargoid').value = cargo;
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function getTP(tp) {
    console.log("el getTP es " + tp);
    fetch(_URLSERVICE_GETTIPOCONT + tp, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "*/*"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (Data) {
        console.log('getTP resultado--->' + Data.nombreTipoContacto);
        document.getElementById('tp').value = Data.nombreTipoContacto;
        document.getElementById('tpid').value = tp;
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function getCliente(id) {
    console.log("el getCliente es " + id);
    fetch(_URLSERVICE_GETCLIENTE + id, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Accept": "*/*"
        }
    }).then(function (response) {
        return response.json();
    }).then(function (Data) {

        console.log('getCliente resultado--->',Data);
        document.getElementById('clienteid').value = id;
        document.getElementById("nombre").value = Data.nombreCompleto;
        document.getElementById("email").value = Data.correoElectronico;
        document.getElementById("telefono").value = Data.telefono;
        document.getElementById("userselect").value = Data.idUsuario;
        document.getElementById("cargoselect").value = Data.idCargo;
        document.getElementById("tpselect").value = Data.idTipoContacto;

    }).catch((error) => {
        console.error("Error:", error);
    });

}

function updateUser() {

    var user = document.getElementById('user').value;
    var iduser = document.getElementById('userid').value;

    console.log("el updateUser user es  " + user + " el iduser es " + iduser);
    fetch(_URLSERVICE_GETUSER + Number(iduser), {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Accept": "*/*"
        },
        body: JSON.stringify({ nombreUsuario: user })
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida->" + response.statusText);            
            document.getElementById('user').value = '';
            document.getElementById('userid').value = '';
            document.getElementById("ModalLabel").innerHTML = '';
            getallUser('users');
            document.getElementById("myDynamicTableUsers").setAttribute("style", "display:block");
            $('#exampleModal').modal('hide');
        } else {
            console.error("Error:", response.statusText);
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function updateCargo() {

    var cargo = document.getElementById('cargo').value;
    var cargoid = document.getElementById('cargoid').value;

    console.log("el updateCargo cargo es  " + cargo + " el cargoid es " + cargoid);
    fetch(_URLSERVICE_GETCARGO + Number(cargoid), {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Accept": "*/*"
        },
        body: JSON.stringify({ nombreCargo: cargo })
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida->" + response.statusText);
            document.getElementById('cargo').value = '';
            document.getElementById('cargoid').value = '';
            document.getElementById("ModalLabel").innerHTML = '';
            getallCargos('cargos');
            document.getElementById("myDynamicTableCargos").setAttribute("style", "display:block");
            $('#exampleModal').modal('hide');
        } else {
            console.error("Error:", response.statusText);
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function updateTP() {

    var tp = document.getElementById('tp').value;
    var tpid = document.getElementById('tpid').value;

    console.log("el updateTP tp es  " + tp + " el tpid es " + tpid);
    fetch(_URLSERVICE_GETTIPOCONT + Number(tpid), {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Accept": "*/*"
        },
        body: JSON.stringify({ nombreTipoContacto: tp })
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida->" + response.statusText);
            document.getElementById('tp').value = '';
            document.getElementById('tpid').value = '';
            document.getElementById("ModalLabel").innerHTML = '';
            getallTiposCont('tps');
            document.getElementById("myDynamicTableTP").setAttribute("style", "display:block");
            $('#exampleModal').modal('hide');
        } else {
            console.error("Error:", response.statusText);
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}

function updateClient(inputNombre, inputEmail, inputTelefono, inputUser, inputCargo, inputTP) {

    var clienteid = document.getElementById('clienteid').value;

    console.log("el updateClient el clienteid es " + clienteid);
    fetch(_URLSERVICE_GETCLIENTE + Number(clienteid), {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            "Accept": "*/*"
        },
        body: JSON.stringify({
            nombreCompleto: inputNombre,
            telefono: Number(inputTelefono),
            correoElectronico: inputEmail,
            idUsuario: Number(inputUser),
            idCargo: Number(inputCargo),
            idTipoContacto: Number(inputTP)
        })
    }).then(function (response) {
        if (response.ok) {
            console.log("Respuesta valida->" + response.statusText);
            document.getElementById("nombre").value = '';
            document.getElementById("email").value = '';
            document.getElementById("telefono").value = '';
            document.getElementById("userselect").value = '';
            document.getElementById("cargoselect").value = '';
            document.getElementById("tpselect").value = '';
            document.getElementById("clienteid").value = '';
            document.getElementById("ModalLabel").innerHTML = '';
            getallClients();
            document.getElementById("myDynamicTable").setAttribute("style", "display:block");
            $('#exampleModal').modal('hide');
        } else {
            console.error("Error:", response.statusText);
        }
    }).catch((error) => {
        console.error("Error:", error);
    });

}