
function initializeModalAnimation(modalId) {
    debugger;
        const modal = document.querySelector(modalId);
    if (!modal) return;

    const modalDialog = modal.querySelector('.modal-dialog');
    if (!modalDialog) return;

    modalDialog.classList.add('position-absolute', 'top-50', 'start-50', 'transform-out');

        // Prevent duplicate listeners
        modal.removeEventListener('show.bs.modal', modal._customShowHandler || (() => { }));
        modal.removeEventListener('hide.bs.modal', modal._customHideHandler || (() => { }));

    modal._customShowHandler = function () {
        modalDialog.classList.remove('transform-out');
    modalDialog.classList.add('transform-in');
        };

    modal._customHideHandler = function () {
        modalDialog.classList.remove('transform-in');
    modalDialog.classList.add('transform-out');
        };

    modal.addEventListener('show.bs.modal', modal._customShowHandler);
    modal.addEventListener('hide.bs.modal', modal._customHideHandler);
    }

    // Automatically initialize when any modal trigger is clicked
    document.addEventListener('click', function (e) {
        const btn = e.target.closest('[data-bs-toggle="modal"]');
    if (btn) {
            const modalSelector = btn.getAttribute('data-bs-target');
            setTimeout(() => {
        initializeModalAnimation(modalSelector);
            }, 100);
        }
    });

// format date time to dd/mm/yyyy
function formatDate(date) {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Month is 0-indexed
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
}

