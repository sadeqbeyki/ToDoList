$(document).ready(function() {
    const token = $('input[name="__RequestVerificationToken"]').val();

    $(document).on('click', '.btn-delete', function(e) {
        e.preventDefault();

        const btn = $(this);
        const id = btn.data('id');
        const url = btn.data('url');

        Swal.fire({
            title: 'Are you sure?',
            text: "This operation is irreversible!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: { id: id },
                    headers: {
                        'RequestVerificationToken': token
                    },
                    success: function(result) {
                        if (result.isSucceeded) {
                            Swal.fire(
                                'Deleted!',
                                result.message || 'The record was successfully deleted.',
                                'success'
                            );
                            btn.closest("tr").fadeOut(500, function() {
                                $(this).remove();
                            });
                        } else {
                            Swal.fire(
                                'Error!',
                                result.message || 'There was a problem deleting.',
                                'error'
                            );
                        }
                    },
                    error: function() {
                        Swal.fire(
                            'Error!',
                            'An error occurred during deletion.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});