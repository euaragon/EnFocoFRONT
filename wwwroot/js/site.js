window.SwalConfirm = async function (email) {
    const result = await Swal.fire({
        title: '¿Confirmás la suscripción?',
        text: 'Email: ' + email,
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Sí, suscribirme',
        cancelButtonText: 'Cancelar'
    });

    return result.isConfirmed;
};

window.getInputValue = function (element) {
    return element.value;
};

window.initTinyMCE = function () {
    const textElement = document.querySelector("#Text");
    if (textElement) {
        tinymce.init({
            selector: '#Text',
            license_key: 'gpl'
        });
    }
};

window.initTinyMCEdit = function () {
    const textEditElement = document.querySelector("#TextEdit");
    if (textEditElement) {
        tinymce.init({
            selector: '#TextEdit',
            license_key: 'gpl'
        });
    }
};

window.getTinyMceContent = function () {
    const editor = tinymce.get("Text");
    return editor ? editor.getContent() : "";
};

window.getTinyMceContentEdit = function () {
    const editor = tinymce.get("TextEdit");
    return editor ? editor.getContent() : "";
};

const imageFileInput = document.querySelector('#ImageFile');
if (imageFileInput) {
    imageFileInput.addEventListener('change', function (e) {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const previewImage = document.querySelector('#previewImage');
                if (previewImage) {
                    previewImage.src = e.target.result;
                    previewImage.style.display = 'block';
                }
            };
            reader.readAsDataURL(file);
        } else {
            const previewImage = document.querySelector('#previewImage');
            if (previewImage) {
                previewImage.src = '#';
                previewImage.style.display = 'none';
            }
        }
    });
}

const createForm = document.querySelector('#createForm');
if (createForm) {
    createForm.addEventListener('submit', function (e) {
        e.preventDefault();

        if (document.querySelector('button[type="submit"]').disabled) {
            console.log("Intento de envío bloqueado porque la imagen es vertical.");
            return;
        }

        const fileInput = document.getElementById('ImageFile');
        const file = fileInput ? fileInput.files[0] : null;
        if (file) {
            const imgField = document.getElementById('Img');
            if (imgField) {
                imgField.value = "img/" + file.name; // Asigna la ruta en el campo Img
            }
        }

        Swal.fire({
            title: '¿Estás seguro?',
            text: '¿Quieres guardar esta noticia?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Guardar'
        }).then((result) => {
            if (result.isConfirmed) {
                createForm.submit();
            }
        });
    });
}







