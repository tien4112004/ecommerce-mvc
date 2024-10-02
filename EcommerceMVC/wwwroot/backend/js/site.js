document.addEventListener("DOMContentLoaded", function() {
    const confirmDeletionLinks = document.querySelectorAll("a.confirmDeletion");
    if (confirmDeletionLinks.length) {
        confirmDeletionLinks.forEach(link => {
            link.addEventListener("click", function(event) {
                if (!confirm("Are you sure you want to delete this record?")) {
                    event.preventDefault();
                }
            });
        });
    }

    const alertNotifications = document.querySelectorAll("div.alert.notification");
    if (alertNotifications.length) {
        setTimeout(() => {
            alertNotifications.forEach(notification => {
                notification.style.display = "none";
            });
        }, 2000);
    }
});

function readURL(input) {
    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function(e) {
            const imgPreview = document.querySelector("img#imgpreview");
            imgPreview.src = e.target.result;
            imgPreview.style.width = "auto";
            imgPreview.style.height = "auto";
            imgPreview.style.maxWidth = "100%";
            imgPreview.style.maxHeight = "100%";
        };

        reader.readAsDataURL(input.files[0]);
    }
}