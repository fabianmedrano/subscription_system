function confirmAlert(event, element) {
    event.preventDefault();

    let type = element.getAttribute('data-type');
    let mjs = element.getAttribute('data-mjs');
    let title = element.getAttribute('data-title');
    let method = element.getAttribute('data-method'); // add this
    let url = element.getAttribute('href');
    let data = element.getAttribute('data-data'); // add this

    return Swal.fire({
        title: title,
        text: mjs,
        icon: type,
        showCancelButton: true,
        confirmButtonText: 'Aceptar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            if (method === 'POST') {
                // send a POST request with the ID
                postRequest(url, method, data);
            } else {
                // send a GET request (or other method)
                getRequest();
            }
        }
    });
}

function getRequest(url) {
    window.location.href = url;
}
function postRequest(url,method,data) {
    $.ajax({
        type: method,
        url: url,
        data: { ...JSON.parse(data) },
        success: function (data) {
            console.log('Request sent successfully');
        },
        error: function (error) {
            console.error('Error sending request:', error);
        }
    });
}
function postRequest(url, method, data) {
    $.ajax({
        type: method,
        url: url,
        data: { ...JSON.parse(data) },
        success: function (response) {
            if (response.success) {
                Swal.fire({
                    title: response.title ??'Éxito',
                    text: response.message,
                    icon: response.messageType ??'success'
                });
                // Actualiza el contenido de la página
                location.reload();
            } else {
                Swal.fire({
                    title: response.title?? 'Error',
                    text: response.message,
                    icon: response.messageType ?? 'error'
                });
            }
        },
        error: function (error) {
            console.error('Error sending request:', error);
        }
    });
}

