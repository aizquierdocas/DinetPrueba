﻿function alertaConfirmacion(C_PARM_SAL) {
    Swal.fire({
        title: 'Correcto',
        icon: 'success',
        text: C_PARM_SAL,
        confirmButtonText: 'Aceptar'
    });
}

function alertaError(C_PARM_SAL) {
    Swal.fire({
        title: 'Error',
        icon: 'error',
        text: C_PARM_SAL,
        confirmButtonText: 'Aceptar'
    })
}