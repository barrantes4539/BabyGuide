<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

$(document).ready(function () {
    // Asociar el botón al evento click y llamar a la función obtenerDetalleBebe con el valor del campo de bebé
    $('#btnCargarDatos').on('click', function () {
        var idBebe = $('#idBebe').val();
        obtenerDetalleBebe(idBebe);
    });
});

function obtenerDetalleBebe(idBebe) {
    $.ajax({
        type: 'GET',
        url: "/Home/DetalleBebe",
            data: { idBebe: idBebe },
        success: function (data) {
            debugger;
            console.log(data);
            mostrarDetalleBebe(data);
        },
        error: function () {
            alert('Datos del bebe no encontrados.');
        }
    });
}

function mostrarDetalleBebe(data) {
    if (data && data.length > 0) {
        var usuario = data[0];
        $('#idUsuarioInput').val(usuario.IdUsuario);
        $('#nombreBebe').val(usuario.Nombre);
        $('#apellido1Bebe').val(usuario.Apellido1);
        $('#apellido2Bebe').val(usuario.Apellido2);
        $('#genero').val(usuario.Genero);
        $('#FechaNacBebe').val(usuario.FechaNacimiento);
    } else {
        limpiarCampos();
    }
}

function limpiarCampos() {
    $('#idBebe').val('');
    $('#nombreBebe').val('');
    $('#apellido1Bebe').val('');
    $('#apellido2Bebe').val('');
    $('#generoInput').val('');
    $('#FechaNacBebe').val('');
}
});
