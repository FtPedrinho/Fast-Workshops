import { renderModal, closeModal } from "./Modal.js";

function showConfirmModal({
    title,
    message,
    onConfirm
}) {
    const modalContent = `
        <div class="confirm-content">

            <p>
                ${message}
            </p>

            <div class="modal-actions">

                <button
                    type="button"
                    class="button button-secondary"
                    data-modal-close
                >
                    Cancelar
                </button>

                <button
                    type="button"
                    class="button button-danger"
                    id="confirm-delete"
                >
                    Excluir
                </button>

            </div>

        </div>
    `;

    document.body.insertAdjacentHTML(
        "beforeend",
        renderModal({
            title,
            content: modalContent,
            modalId: "confirm-modal"
        })
    );

    const modal = document.querySelector("#confirm-modal");

    modal
        .querySelector("[data-modal-close]")
        .addEventListener("click", closeModal);

    modal
        .querySelector("#confirm-delete")
        .addEventListener("click", async () => {
            await onConfirm();
            closeModal();
        });
}

export {
    showConfirmModal
};