function confirmAlert(event, element) {
    event.preventDefault();
    
    let type = element.getAttribute('data-type');
    let mjs = element.getAttribute('data-mjs');
    let title = element.getAttribute('data-title');


    return Swal.fire({
        title: title,
        text: mjs,
        icon: type,
        showCancelButton: true,
        confirmButtonText: 'Aceptar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed)
        window.location.href = element.getAttribute('href'); 
    });
}
