import {
    getWorkshopById,
    getColaboradores,
    getParticipacoes,
    createParticipacao,
    deleteParticipacao
} from "../services/api.js";

import { renderNavbar } from "../components/Navbar.js";
import { renderModal, closeModal } from "../components/Modal.js";
import { showConfirmModal } from "../components/ConfirmModal.js";


async function renderWorkshopDetalhes(app, workshopId) {
    app.innerHTML = `
        ${renderNavbar()}

        <main class="container">

            <div id="workshop-details">
                <p>Carregando workshop...</p>
            </div>

        </main>
    `;

    await loadWorkshopDetails(workshopId);
}


async function loadWorkshopDetails(workshopId) {
    const container = document.querySelector(
        "#workshop-details"
    );

    try {
        const [workshop, participacoes] = await Promise.all([
            getWorkshopById(workshopId),
            getParticipacoes(workshopId)
        ]);

        renderWorkshopDetails(
            container,
            workshop,
            participacoes
        );

        setupParticipantEvents(
            workshop,
            participacoes
        );

    } catch (error) {
        renderError(container, error.message);
    }
}


function renderWorkshopDetails(
    container,
    workshop,
    participacoes
) {
    const participants = workshop.participantes ?? [];

    const participantCount = participants.length;

    container.innerHTML = `
        <section class="workshop-details">

            <header class="details-header">

                <a
                    href="#/workshops"
                    class="back-link"
                >
                    ← Voltar para workshops
                </a>

                <div class="details-title-row">

                    <div>
                        <span class="workshop-date">
                            ${formatDate(workshop.dataRealizacao)}
                        </span>

                        <h1>
                            ${workshop.nome}
                        </h1>
                    </div>

                </div>

            </header>


            <div class="details-content">

                <section class="workshop-description">

                    <h2>Sobre o workshop</h2>

                    <p>
                        ${workshop.descricao || "Sem descrição."}
                    </p>

                </section>


                <section class="participants-section">

                    <header class="section-header">

                        <div>
                            <h2>
                                Participantes
                            </h2>

                            <span class="participant-count">
                                ${participantCount}
                                participante${participantCount !== 1 ? "s" : ""}
                            </span>
                        </div>

                        <button
                            type="button"
                            class="add-button"
                            id="add-participant"
                            aria-label="Adicionar participante"
                            title="Adicionar participante"
                        >
                            +
                        </button>

                    </header>


                    <div
                        id="participants-container"
                        class="participants-list"
                    >
                        ${renderParticipants(participants)}
                    </div>

                </section>

            </div>

        </section>
    `;
}


function renderParticipants(participants) {
    if (participants.length === 0) {
        return `
            <div class="empty-state">

                <h3>
                    Nenhum participante registrado.
                </h3>

                <p>
                    Utilize o botão + para registrar um colaborador.
                </p>

            </div>
        `;
    }

    return participants
        .map(renderParticipant)
        .join("");
}


function renderParticipant(participant) {
    return `
        <article class="participant-card">

            <div class="participant-info">

                <span class="participant-id">
                    #${participant.id}
                </span>

                <strong>
                    ${participant.nome}
                </strong>

            </div>

            <button
                type="button"
                class="icon-button delete-button"
                data-action="remove-participant"
                data-id="${participant.id}"
                aria-label="Remover ${participant.nome} do workshop"
                title="Remover participante"
            >
                🗑
            </button>

        </article>
    `;
}


function setupParticipantEvents(
    workshop,
    participacoes
) {
    const addButton = document.querySelector(
        "#add-participant"
    );

    if (addButton) {
        addButton.addEventListener("click", () => {
            openParticipantForm(
                workshop,
                participacoes
            );
        });
    }


    const container = document.querySelector(
        "#participants-container"
    );

    container.addEventListener("click", event => {
        const button = event.target.closest("button");

        if (!button) {
            return;
        }

        const action = button.dataset.action;
        const collaboratorId = Number(button.dataset.id);

        if (action === "remove-participant") {
            confirmParticipantRemoval(
                workshop,
                collaboratorId
            );
        }
    });
}


async function openParticipantForm(
    workshop,
    participacoes
) {
    try {
        const colaboradores = await getColaboradores();

        const participantIds = new Set(
            participacoes.map(
                participacao => participacao.colaboradorId
            )
        );

        const availableCollaborators =
            colaboradores.filter(
                colaborador =>
                    !participantIds.has(colaborador.id)
            );


        if (availableCollaborators.length === 0) {
            alert(
                "Todos os colaboradores já estão registrados neste workshop."
            );

            return;
        }


        const options = availableCollaborators
            .map(colaborador => `
                <option value="${colaborador.id}">
                    ${colaborador.nome}
                </option>
            `)
            .join("");


        const content = `
            <form id="participant-form">

                <div class="form-group">

                    <label for="participant">
                        Colaborador
                    </label>

                    <select
                        id="participant"
                        name="colaboradorId"
                        required
                    >
                        <option value="">
                            Selecione um colaborador
                        </option>

                        ${options}
                    </select>

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
                        Adicionar
                    </button>

                </div>

            </form>
        `;


        document.body.insertAdjacentHTML(
            "beforeend",
            renderModal({
                title: "Adicionar participante",
                content
            })
        );


        const modal = document.querySelector(
            ".modal-overlay"
        );

        const form = document.querySelector(
            "#participant-form"
        );


        modal
            .querySelectorAll("[data-modal-close]")
            .forEach(button => {
                button.addEventListener(
                    "click",
                    closeModal
                );
            });


        form.addEventListener(
            "submit",
            async event => {
                event.preventDefault();

                const colaboradorId = Number(
                    form.colaboradorId.value
                );

                if (!colaboradorId) {
                    return;
                }

                try {
                    await createParticipacao(
                        workshop.id,
                        colaboradorId
                    );

                    closeModal();

                    await loadWorkshopDetails(
                        workshop.id
                    );

                } catch (error) {
                    alert(error.message);
                }
            }
        );

    } catch (error) {
        alert(error.message);
    }
}


function confirmParticipantRemoval(
    workshop,
    colaboradorId
) {
    const participant = workshop.participantes?.find(
        colaborador =>
            colaborador.id === colaboradorId
    );

    if (!participant) {
        return;
    }


    showConfirmModal({
        title: "Remover participante",

        message: `
            Tem certeza que deseja remover
            <strong>${participant.nome}</strong>
            deste workshop?
        `,

        onConfirm: async () => {
            try {
                await deleteParticipacao(
                    workshop.id,
                    colaboradorId
                );

                await loadWorkshopDetails(
                    workshop.id
                );

            } catch (error) {
                alert(error.message);
            }
        }
    });
}


function formatDate(date) {
    return new Date(`${date}T00:00:00`)
        .toLocaleDateString("pt-BR");
}


function renderError(container, message) {
    container.innerHTML = `
        <div class="error-state">

            <h2>
                Não foi possível carregar o workshop.
            </h2>

            <p>
                ${message}
            </p>

            <a
                href="#/workshops"
                class="button"
            >
                Voltar para workshops
            </a>

        </div>
    `;
}


export {
    renderWorkshopDetalhes
};