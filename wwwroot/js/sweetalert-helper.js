window.SwalHelper = {
    confirmDelete: function () {
        return Swal.fire({
            title: "¿Estás seguro?",
            text: "Esta acción no se puede deshacer.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar"
        });
    },

    showSuccess: function (msg) {
        return Swal.fire("Éxito", msg, "success");
    },

    showError: function (msg) {
        return Swal.fire("Error", msg, "error");
    }
};
