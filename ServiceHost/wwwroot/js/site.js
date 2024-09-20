// SinglePage object to manage modal operations
const SinglePage = {
    loadModal() {
        const hash = window.location.hash.toLowerCase();
        if (!hash.startsWith("#showmodal")) return;

        const urlParam = hash.split("showmodal=")[1];
        if (!urlParam) return;

        $.get(urlParam)
            .done(html => {
                const $modalContent = $("#ModalContent").html(html);
                const $form = $modalContent.find("form").last();
                $.validator.unobtrusive.parse($form);
                this.showModal();
            })
            .fail(() => alert("An error has occurred, please contact the system administrator."));
    },

    showModal() {
        const $modal = $("#MainModal");
        $modal.modal("show");

        // Handle close button
        $modal.find(".modal-close").off("click").on("click", () => this.hideModal());

        // Clean up URL
        window.history.replaceState({}, document.title, window.location.pathname);
    },

    hideModal() {
        $("#MainModal").modal("hide");
    },

    handleAjaxFormSubmit($form) {
        const method = $form.attr("method").toLowerCase();
        const url = $form.attr("action");
        const action = $form.data("action");

        if (method === "get") {
            const data = $form.serialize();
            $.get(url, data, result => this.handleCallback(result, action, $form));
        } else {
            const formData = new FormData($form[0]);
            $.ajax({
                url,
                type: "post",
                data: formData,
                enctype: "multipart/form-data",
                dataType: "json",
                processData: false,
                contentType: false,
                success: data => this.handleCallback(data, action, $form),
                error: () => alert("An error has occurred, please contact the system administrator.")
            });
        }
    },

    handleCallback(data, action, $form) {
        switch (action) {
            case "Message":
                alert(data.message);
                break;
            case "Refresh":
                if (data.isSucceeded) window.location.reload();
                else alert(data.message);
                break;
            case "RefreshList":
                this.hideModal();
                const refreshUrl = $form.data("refreshurl");
                const refreshDiv = $form.data("refreshdiv");
                if (refreshUrl && refreshDiv) this.refreshContent(refreshUrl, refreshDiv);
                break;
            case "SetValue":
                const element = $form.data("element");
                if (element) $(`#${element}`).html(data);
                break;
            default:
                this.hideModal();
        }
    },

    refreshContent(url, targetId) {
        const params = window.location.search;
        $.get(url, params, result => $(`#${targetId}`).html(result));
    }
};

// Document ready setup
$(function () {
    window.onhashchange = () => SinglePage.loadModal();

    // Submit AJAX forms
    $(document).on("submit", 'form[data-ajax="true"]', function (e) {
        e.preventDefault();
        SinglePage.handleAjaxFormSubmit($(this));
        return false;
    });

    // Ajax-enabled buttons
    $(document).on("click", 'button[data-ajax="true"]', function () {
        const $btn = $(this);
        const method = $btn.data("request-method")?.toLowerCase();
        let url = $btn.data("request-url");
        const formId = $btn.data("request-form");
        const fieldId = $btn.data("request-field-id");
        const data = formId ? $(`#${formId}`).serialize() : null;

        if (fieldId) url += `/${$(`#${fieldId}`).val()}`;

        if ($btn.data("request-confirm")) {
            if (!confirm("Are you sure?")) return;
        }

        if (method === "post") {
            $.post(url, data)
                .done(() => window.location.reload())
                .fail(() => alert("An error has occurred, please contact the system administrator."));
        } else {
            $.get(url, data)
                .done(response => console.log(response))
                .fail(() => alert("An error has occurred, please contact the system administrator."));
        }
    });
});



