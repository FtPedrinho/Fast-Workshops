import {
    getWorkshops,
    createWorkshop,
    updateWorkshop,
    deleteWorkshop
} from "../services/api.js";

import { renderNavbar } from "../components/Navbar.js";
import { renderWorkshopCard } from "../components/WorkshopCard.js";
import { renderModal, closeModal } from "../components/Modal.js";
import { showConfirmModal } from "../components/ConfirmModal.js";


async function renderWorkshops(app) {
    app.innerHTML = `
        ${renderNavbar()}

        <main class="container">

            <header class="page-header page-header-with-action">

                <div>
                    <h1>Workshops</h1>

                    <p>
                        Visualização dos workshops da FAST Soluções.
                    </p>
                </div>

                <button
                    type="button"
                    class="add-button"
                    id="add-workshop"
                    aria-label="Adicionar workshop"
                    title="Adicionar workshop"
                >
                    +
                </button>

            </header>

            <section
                id="workshops-container"
                class="workshops-grid"
            >
                <p>Carregando workshops...</p>
            </section>

        </main>
    `;

    setupAddButton();

    await loadWorkshops();
}


async function loadWorkshops() {
    const container = document.querySelector(
        "#workshops-container"
    );

    try {
        const workshops = await getWorkshops();

        if (workshops.length === 0) {
            renderEmptyState(container);
            return;
        }

        container.innerHTML = workshops
            .map(renderWorkshopCard)
            .join("");

        setupWorkshopEvents(workshops);

    } catch (error) {
        renderError(container, error.message);
    }
}


function setupAddButton() {
    const button = document.querySelector("#add-workshop");

    if (!button) {
        return;
    }

    button.addEventListener("click", () => {
        openWorkshopForm();
    });
}


function setupWorkshopEvents(workshops) {
    const container = document.querySelector(
        "#workshops-container"
    );

    if (!container) {
        return;
    }

    container.addEventListener("click", event => {
        const button = event.target.closest("button");

        if (!button) {
            return;
        }

        const id = Number(button.dataset.id);
        const action = button.dataset.action;

        const workshop = workshops.find(
            item => item.id === id
        );

        if (!workshop) {
            return;
        }

        if (action === "edit") {
            openWorkshopForm(workshop);
        }

        if (action === "delete") {
            confirmWorkshopDeletion(workshop);
        }
    });
}


function openWorkshopForm(workshop = null) {
    const isEditing = workshop !== null;

    const title = isEditing
        ? "Editar workshop"
        : "Adicionar workshop";

    const content = `
        <form id="workshop-form">

            <div class="form-group">

                <label for="workshop-name">
                    Nome
                </label>

                <input
                    id="workshop-name"
                    name="nome"
                    type="text"
                    maxlength="150"
                    required
                    value="${isEditing ? workshop.nome : ""}"
                    placeholder="Digite o nome do workshop"
                >

            </div>

            <div class="form-group">

                <label for="workshop-date">
                    Data de realização
                </label>

                <input
                    id="workshop-date"
                    name="dataRealizacao"
                    type="date"
                    required
                    value="${isEditing ? workshop.dataRealizacao : ""}"
                >

            </div>

            <div class="form-group">

                <label for="workshop-description">
                    Descrição
                </label>

                <textarea
                    id="workshop-description"
                    name="descricao"
                    maxlength="1000"
                    placeholder="Digite a descrição do workshop"
                >${isEditing ? workshop.descricao ?? "" : ""}</textarea>

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

    setupModalEvents();

    const form = document.querySelector("#workshop-form");

    form.addEventListener("submit", async event => {
        event.preventDefault();

        const nome = form.nome.value.trim();
        const dataRealizacao = form.dataRealizacao.value;
        const descricao = form.descricao.value.trim();

        if (!nome || !dataRealizacao) {
            return;
        }

        const workshopData = {
            nome,
            dataRealizacao,
            descricao: descricao || null
        };

        try {
            if (isEditing) {
                await updateWorkshop(
                    workshop.id,
                    workshopData
                );
            } else {
                await createWorkshop(workshopData);
            }

            closeModal();

            await loadWorkshops();

        } catch (error) {
            alert(error.message);
        }
    });
}


function setupModalEvents() {
    const modal = document.querySelector(".modal-overlay");

    if (!modal) {
        return;
    }

    const closeButton = modal.querySelector(
        "[data-modal-close]"
    );

    if (closeButton) {
        closeButton.addEventListener(
            "click",
            closeModal
        );
    }
}


function confirmWorkshopDeletion(workshop) {
    showConfirmModal({
        title: "Excluir workshop",

        message: `
            Tem certeza que deseja excluir
            <strong>${workshop.nome}</strong>?
            <br><br>
            As participações relacionadas também serão removidas.
        `,

        onConfirm: async () => {
            try {
                await deleteWorkshop(workshop.id);

                await loadWorkshops();

            } catch (error) {
                alert(error.message);
            }
        }
    });
}


function renderEmptyState(container) {
    container.innerHTML = `
        <div class="empty-state">

            <h2>Nenhum workshop encontrado.</h2>

            <p>
                Utilize o botão + para adicionar o primeiro workshop.
            </p>

        </div>
    `;
}


function renderError(container, message) {
    container.innerHTML = `
        <div class="error-state">

            <h2>
                Não foi possível carregar os workshops.
            </h2>

            <p>
                ${message}
            </p>

        </div>
    `;
}


export {
    renderWorkshops
};