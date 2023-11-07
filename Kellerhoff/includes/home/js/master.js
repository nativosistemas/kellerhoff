
var isIngresarPageMethods = false;
var name = null;
var pass = null;
jQuery(document).ready(function () {
    //        localStorage['name'] = name;
    //localStorage['pass'] = pass;



    var myName = localStorage['name'] || '';
    var myPass = localStorage['pass'] || '';
    $('#login_password').val(myPass);
    $('#login_name').val(myName);

    $('#password_footer').val(myPass);
    $('#name_footer').val(myName);
});

function ajaxLogin(name, pass, token) {
    if (isNotNullEmpty(name) && isNotNullEmpty(pass)) {
        $.ajax({
            type: "POST",
            url: "../config/login",
            data: { pName: name, pPass: pass, pToken: token  },
            success:
                function (response) {
                    OnCallBackLogin(response);
                },
            failure: function (response) {
                OnFail(response);
            },
            error: function (response) {
                OnFail(response);
            }
        });
    }
}
function ajaxLoginCarrito(name, pass, pIdOferta,pToken) {
    if (isNotNullEmpty(name) && isNotNullEmpty(pass)) {
        $.ajax({
            type: "POST",
            url: "../config/loginCarrito",
            data: { pName: name, pPass: pass, pIdOferta: pIdOferta, pToken: pToken  },
            success:
                function (response) {
                    OnCallBackLoginCarrito(response);
                },
            failure: function (response) {
                OnFail(response);
            },
            error: function (response) {
                OnFail(response);
            }
        });
    }
}
function onkeypressIngresar(e) {
    if (!e) e = window.event;
    var keyCode = e.keyCode || e.which;
    if (keyCode == '13') {
        // Enter pressed
        onclickIngresar();
        return false;
    }
}
function onkeypressIngresarAbajo(e) {
    if (!e) e = window.event;
    var keyCode = e.keyCode || e.which;
    if (keyCode == '13') {
        // Enter pressed
        onclickIngresarAbajo();
        return false;
    }
}
function onkeypressIngresarDesdeAgregarCarrito(e) {
    if (!e) e = window.event;
    var keyCode = e.keyCode || e.which;
    if (keyCode == '13') {
        // Enter pressed
        onclickIngresarDesdeAgregarCarrito();
        return false;
    }
}

function onclickIngresar() {
    if (!isIngresarPageMethods) {
        isIngresarPageMethods = true;
        name = $('#login_name').val();
        pass = $('#login_password').val();

        //ajaxLogin(name, pass);
        var token = "";

        grecaptcha.execute("6LeVNeQoAAAAAB8BX4-pJCSwCfdS7iWes-JQWhJe", {
            action: "iniciar_sesion",
        })
            .then(function (tokenResponse) {
                token = tokenResponse;

                ajaxLogin(name, pass, token);
            });
    }
    return false;
}
function onclickIngresarAbajo() {
    if (!isIngresarPageMethods) {
        isIngresarPageMethods = true;
        name = $('#name_footer').val();
        pass = $('#password_footer').val();
        // ajaxLogin(name, pass);

        var token = "";

        grecaptcha.execute("6LeVNeQoAAAAAB8BX4-pJCSwCfdS7iWes-JQWhJe", {
                action: "iniciar_sesion",
            })
            .then(function (tokenResponse) {
                token = tokenResponse;

                ajaxLogin(name, pass, token);
            });
    }
    return false;
}
function OnFailLogin(ex) {
    isIngresarPageMethods = false;
    OnFail(ex);
}
var idOferta = null;

function IngresarDsdMobil() {
    idOferta = -1;
    var myName = localStorage['name'] || '';
    var myPass = localStorage['pass'] || '';
    $('#name_carrito').val(myName);
    $('#password_carrito').val(myPass);
    $('#myModal').modal();
}
function onclickIngresarDesdeAgregarCarrito() {
    name = $('#name_carrito').val();
    pass = $('#password_carrito').val();
   // ajaxLoginCarrito(name, pass, idOferta);
    var token = "";

    grecaptcha.execute("6LeVNeQoAAAAAB8BX4-pJCSwCfdS7iWes-JQWhJe", {
        action: "iniciar_sesion",
    })
        .then(function (tokenResponse) {
            token = tokenResponse;

            ajaxLoginCarrito(name, pass, idOferta, token);
        });
    return false;
}
function OnCallBackLoginCarrito(args) {
    if (args == 'Ok') {
        localStorage['name'] = name;
        localStorage['pass'] = pass;
        funIrIntranet();
    }
    else if (args == 'OkPromotor') {
        localStorage['name'] = name;
        localStorage['pass'] = pass;
        funIrIntranetPromotor();
    }
    else {
        $.alert({
            title: 'Información',
            content: args,
        });
    }
}
function funIrIntranet() {
    location.href = '../mvc/Buscador';
}

function funIrIntranetPromotor() {
    location.href = '../ctacte/composicionsaldo';
}

function OnCallBackLogin(args) {
    isIngresarPageMethods = false;
    console.log(args);
    if (args == 'Ok') {
        localStorage['name'] = name;
        localStorage['pass'] = pass;
        funIrIntranet();
    }
    else if (args == 'OkPromotor') {
        localStorage['name'] = name;
        localStorage['pass'] = pass;
        funIrIntranetPromotor();
    }
    else {
        $.alert({
            title: 'Información',
            content: args,
        });
    }
}