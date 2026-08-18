function renderModal({
    title,
    content,
    modalId = "modal"
}) {
    return `
        <div
            id="${modalId}"
            class="modal-overlay"
            role="dialog"
            aria-modal="true"
            aria-labelledby="${modalId}-title"
        >
            <div class="modal">

                <div class="modal-header">
                    <h2 id="${modalId}-title">
                        ${title}
                    </h2>

                    <button
                        type="button"
                        class="modal-close"
                        data-modal-close
                        aria-label="Fechar"
                    >
                        ×
                    </button>
                </div>

                <div class="modal-content">
                    ${content}
                </div>

            </div>
        </div>
    `;
}

function closeModal() {
    const modal = document.querySelector(".modal-overlay");

    if (modal) {
        modal.remove();
    }
}

export {
    renderModal,
    closeModal
};