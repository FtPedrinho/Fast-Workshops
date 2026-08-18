import {
    getColaboradores,
    createColaborador,
    updateColaborador,
    deleteColaborador
} from "../services/api.js";

import { renderNavbar } from "../components/Navbar.js";
import { renderCollaboratorCard } from "../components/CollaboratorCard.js";
import { renderModal, closeModal } from "../components/Modal.js";
import { showConfirmModal } from "../components/ConfirmModal.js";

async function renderColaboradores(app) {
    app.innerHTML = `
        ${renderNavbar()}

        <main class="container">

            <header class="page-header page-header-with-action">

                <div>
                    <h1>Colaboradores</h1>

                    <p>
                        Visualização dos colaboradores da FAST Soluções.
                    </p>
                </div>

                <button
                    type="button"
                    class="add-button"
                    id="add-collaborator"
                    aria-label="Adicionar colaborador"
                    title="Adicionar colaborador"
                >
                    +
                </button>

            </header>

            <section
                id="collaborators-container"
                class="collaborators-list"
            >
                <p>Carregando colaboradores...</p>
            </section>

        </main>
    `;

    await loadColaboradores();
}

async function loadColaboradores() {
    const container = document.querySelector(
        "#collaborators-container"
    );

    try {
        const colaboradores = await getColaboradores();

        if (colaboradores.length === 0) {
            renderEmptyState(container);
        } else {
            container.innerHTML = colaboradores
                .map(renderCollaboratorCard)
                .join("");
        }

        setupPageEvents(colaboradores);

    } catch (error) {
        renderError(container, error.message);
    }
}

function setupPageEvents(colaboradores) {
    setupAddButton();

    document
        .querySelector("#collaborators-container")
        .addEventListener("click", event => {

            const button = event.target.closest("button");

            if (!button) {
                return;
            }

            const id = Number(button.dataset.id);
            const action = button.dataset.action;

            const colaborador = colaboradores.find(
                item => item.id === id
            );

            if (!colaborador) {
                return;
            }

            if (action === "edit") {
                openCollaboratorForm(colaborador);
            }

            if (action === "delete") {
                confirmCollaboratorDeletion(colaborador);
            }
        });
}

function setupAddButton() {
    const button = document.querySelector("#add-collaborator");

    if (!button) {
        return;
    }

    button.addEventListener("click", () => {
        openCollaboratorForm();
    });
}

function openCollaboratorForm(colaborador = null) {
    const isEditing = colaborador !== null;

    const title = isEditing
        ? "Editar colaborador"
        : "Adicionar colaborador";

    const content = `
        <form id="collaborator-form">

            <div class="form-group">
                <label for="collaborator-name">
                    Nome
                </label>

                <input
                    id="collaborator-name"
                    name="nome"
                    type="text"
                    maxlength="150"
                    required
                    value="${isEditing ? colaborador.nome : ""}"
                    placeholder="Digite o nome"
                >
            </div>

            <div class="modal-actions">

                <button
                    type="button"
                    class="button button-secondary"
                    data-modal-close
                >
                    Cancelar
                </button>

                <button
                    type="submit"
                    class="button"
                >
                    Salvar
                </button>

            </div>

        </form>
    `;

    document.body.insertAdjacentHTML(
        "beforeend",
        renderModal({
            title,
            content
        })
    );

    const modal = document.querySelector(".modal-overlay");
    const form = document.querySelector("#collaborator-form");

    modal
        .querySelectorAll("[data-modal-close]")
        .forEach(button => {
            button.addEventListener(
                "click",
                closeModal
            );
        });

    form.addEventListener("submit", async event => {
        event.preventDefault();

        const nome = form.nome.value.trim();

        if (!nome) {
            return;
        }

        try {
            if (isEditing) {
                await updateColaborador(colaborador.id, nome);
            } else {
                await createColaborador(nome);
            }

            closeModal();

            await loadColaboradores();

        } catch (error) {
            alert(error.message);
        }
    });
}

function confirmCollaboratorDeletion(colaborador) {
    showConfirmModal({
        title: "Excluir colaborador",
        message: `
            Tem certeza que deseja excluir
            <strong>${colaborador.nome}</strong>?
        `,
        onConfirm: async () => {
            try {
                await deleteColaborador(colaborador.id);

                await loadColaboradores();

            } catch (error) {
                alert(error.message);
            }
        }
    });
}

function renderEmptyState(container) {
    container.innerHTML = `
        <div class="empty-state">
            <h2>Nenhum colaborador encontrado.</h2>

            <p>
                Utilize o botão + para adicionar o primeiro colaborador.
            </p>
        </div>
    `;
}

function renderError(container, message) {
    container.innerHTML = `
        <div class="error-state">
            <h2>Não foi possível carregar os colaboradores.</h2>

            <p>${message}</p>
        </div>
    `;
}

export {
    renderColaboradores
};